namespace FluentIL.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL.Emitters;

    public class FluentMethodBuilder
        : IMethodBuilder
    {
        //private readonly Emitter body;

        private readonly Func<string, MethodAttributes, CallingConventions, Type, Type[], Type[], Type[], Type[][], Type[][], MethodBuilder> defineMethod;

        private string methodName;

        private CallingConventions callingConvention;

        private Type returnType;

        private List<FluentParameterBuilder> parms = new List<FluentParameterBuilder>();

        private List<FluentGenericParameterBuilder> genericParameterBuilders;

        private GenericTypeParameterBuilder[] genericParameters;

        private MethodBuilder methodBuilder;

        //private Func<MethodInfo> postAction;

        /// <summary>
        /// Initialises a new instance of the <see cref="FluentMethodBuilder"/> class.
        /// </summary>
        /// <param name="methodName">The name of the method</param>
        /// <param name="defineMethod">A function to define the <see cref="MethodBuilder"/>.</param>
        /// <param name="postAction">A function perform any post build actions.</param>
        internal FluentMethodBuilder(
            string methodName,
            Func<string, MethodAttributes, CallingConventions, Type, Type[], Type[], Type[], Type[][], Type[][], MethodBuilder> defineMethod,
            Func<MethodInfo> postAction = null)
        {
            this.methodName = methodName;
            this.returnType = typeof(void);
            this.defineMethod = defineMethod;
            // this.postAction = postAction;
            // this.body = new Emitter();
        }

        /// <inheritdoc/>
        public MethodAttributes Attributes { get; set; }

        /// <inheritdoc />
        public IEmitter Body()
        {
            return this.Define().Body();
        }

        /// <inheritdoc />
        public IMethodBuilder Body(Action<IEmitter> action)
        {
            action(this.Define().Body());
            return this;
        }

        /// <inheritdoc/>
        public IMethodBuilder Param<TParam>(string parameterName, ParameterAttributes attrs = ParameterAttributes.None)
        {
            return this.Param(typeof(TParam), parameterName, attrs);
        }

        /// <inheritdoc/>
        public IMethodBuilder Param(Type parameterType, string parameterName, ParameterAttributes attrs = ParameterAttributes.None)
        {
            this.ThrowIfDefined();
            this.parms.Add(
                new FluentParameterBuilder(
                    parameterType,
                    parameterName,
                    attrs));

            return this;
        }

        /// <inheritdoc/>
        public IMethodBuilder Param(Action<IParameterBuilder> action)
        {
            this.ThrowIfDefined();
            var builder = new FluentParameterBuilder();
            this.parms.Add(builder);
            action(builder);
            return this;
        }

        /// <summary>
        /// Defines the constructor parameters.
        /// </summary>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The <see cref="IConstructorBuilder"/> instance.</returns>
        public IMethodBuilder Params(params Type[] parameterTypes)
        {
            this.ThrowIfDefined();
            this.parms = parameterTypes.Select(
                t => new FluentParameterBuilder(t, null, ParameterAttributes.None))
                .ToList();
            return this;
        }

        /// <inheritdoc/>
        public bool HasParameter(string parameterName)
        {
            return this.parms.Any(p => p.ParameterName == parameterName);
        }

        /// <summary>
        /// Sets the methods attributes.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns>The method builder.</returns>
        public IMethodBuilder MethodAttributes(MethodAttributes attributes)
        {
            this.ThrowIfDefined();
            this.Attributes = attributes;
            return this;
        }

        /// <inheritdoc/>
        public IMethodBuilder CallingConvention(CallingConventions callingConventions)
        {
            this.ThrowIfDefined();
            this.callingConvention = callingConventions;
            return this;
        }

        /// <inheritdoc/>
        public IMethodBuilder Returns<TReturn>()
        {
            return this.Returns(typeof(TReturn));
        }

        /// <inheritdoc/>
        public IMethodBuilder Returns(Type returnType)
        {
            this.ThrowIfDefined();
            this.returnType = returnType;
            return this;
        }

        /// <inheritdoc/>
        public IGenericParameterBuilder NewGenericParameter(string parameterName)
        {
            this.genericParameterBuilders = this.genericParameterBuilders ?? new List<FluentGenericParameterBuilder>();
            var builder = new FluentGenericParameterBuilder(
                parameterName,
                (name) =>
                {
                    this.Define();
                    return this.GetGenericParameter(name);
                });

            this.genericParameterBuilders.Add(builder);
            return builder;
        }

        /// <inheritdoc/>
        public IMethodBuilder NewGenericParameter(string parameterName, Action<IGenericParameterBuilder> action)
        {
            this.genericParameterBuilders = this.genericParameterBuilders ?? new List<FluentGenericParameterBuilder>();
            var builder = new FluentGenericParameterBuilder(
                parameterName,
                (name) =>
                {
                    this.Define();
                    return this.GetGenericParameter(name);
                });

            action(builder);
            this.genericParameterBuilders.Add(builder);
            return this;
        }

        /// <inheritdoc/>
        public IMethodBuilder NewGenericParameters(params string[] parameterNames)
        {
            return this.NewGenericParameters(parameterNames, (Action<IGenericParameterBuilder[]>) null);
        }

        /// <summary>
        /// Defines generic parameters.
        /// </summary>
        /// <param name="parameterNames">The names of the parameters.</param>
        /// <param name="action">The action to update the parameters</param>
        /// <returns>The <see cref="IMethodBuilder"/> instance.</returns>
        public IMethodBuilder NewGenericParameters(string[] parameterNames, Action<IGenericParameterBuilder[]> action)
        {
            this.genericParameterBuilders = this.genericParameterBuilders ?? new List<FluentGenericParameterBuilder>();
            foreach (var parameterName in parameterNames)
            {
                var builder = new FluentGenericParameterBuilder(
                    parameterName,
                    (name) =>
                    {
                        this.Define();
                        return this.GetGenericParameter(name);
                    });
                this.genericParameterBuilders.Add(builder);
            }

            action?.Invoke(this.genericParameterBuilders.ToArray());

            return this;
        }

        /// <summary>
        /// Gets a generic parameter builder.
        /// </summary>
        /// <param name="parameterName">The generic parameter name.</param>
        /// <returns>A <see cref="GenericTypeParameterBuilder"/>.</returns>
        public GenericTypeParameterBuilder GetGenericParameter(string parameterName)
        {
            return this.genericParameters
                .FirstOrDefault(g => g.Name == parameterName);
        }

        /// <summary>
        /// Defines the method.
        /// </summary>
        /// <returns>A <see cref="MethodInfo"/> instance.</returns>
        public MethodBuilder Define()
        {
            if (this.methodBuilder != null)
            {
                return this.methodBuilder;
            }

            this.methodBuilder = this.defineMethod(
                    this.methodName,
                    this.Attributes,
                    this.callingConvention,
                    this.returnType,
                    null,
                    null,
                    this.parms.Select(p => p.ParameterType).ToArray(),
                    null,
                    null);

            if (this.genericParameterBuilders != null)
            {
                this.genericParameters = this.methodBuilder.DefineGenericParameters(
                    this.genericParameterBuilders.Select(g => g.ParameterName).ToArray());

                for (int i = 0; i < this.genericParameterBuilders.Count; i++)
                {
                    this.genericParameterBuilders[i].Build(this.genericParameters[i]);
                }
            }

            int parmIndex = 0;
            foreach (var parm in this.parms)
            {
                this.methodBuilder.DefineParameter(++parmIndex, parm.Attributes, parm.ParameterName);
            }

            DebugOutput.WriteLine("");
            DebugOutput.WriteLine("=======================================");
            DebugOutput.WriteLine("New Method {0}({1})",
                this.methodBuilder.Name,
                string.Join(", ", this.parms.Select(p => $"{p.ParameterType} {p.ParameterName}")));
            DebugOutput.WriteLine("Calling Convention: {0}", this.methodBuilder.CallingConvention);
            DebugOutput.WriteLine("Attributes: {0}", this.Attributes);
            DebugOutput.WriteLine("");

            return this.methodBuilder;
        }

        /// <summary>
        /// Throws an exception if the method has been defined.
        /// </summary>
        private void ThrowIfDefined()
        {
            if (this.methodBuilder != null)
            {
                throw new InvalidOperationException("Method already defined");
            }
        }
    }
}