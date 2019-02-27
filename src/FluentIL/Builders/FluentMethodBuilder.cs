namespace FluentIL.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL.Emitters;

    /// <summary>
    /// Implementation of the <see cref="IMethodBuilder"/> interface.
    /// </summary>
    public class FluentMethodBuilder
        : IMethodBuilder
    {
        /// <summary>
        /// A reference to the function that creates the <see cref="MethodBuilder"/>.
        /// </summary>
        private readonly Func<string, MethodAttributes, CallingConventions, Type, Type[], Type[], Type[], Type[][], Type[][], MethodBuilder> defineMethod;

        /// <summary>
        /// The methods name.
        /// </summary>
        private string methodName;

        /// <summary>
        /// The methods calling convention.
        /// </summary>
        private CallingConventions callingConvention;

        /// <summary>
        /// The methods return type.
        /// </summary>
        private Type returnType;

        /// <summary>
        /// The methods parameters. 
        /// </summary>
        private List<FluentParameterBuilder> parms = new List<FluentParameterBuilder>();

        /// <summary>
        /// The methods generic arguments.
        /// </summary>
        private List<FluentGenericParameterBuilder> genericParameterBuilders;

        /// <summary>
        /// A generic type parameter builder.
        /// </summary>
        private GenericTypeParameterBuilder[] genericParameters;

        /// <summary>
        /// The methods method builder.
        /// </summary>
        private MethodBuilder methodBuilder;

        /// <summary>
        /// The methods custom attributes.
        /// </summary>
        private List<CustomAttributeBuilder> customAttributes;

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

        /// <inheritdoc/>
        public IMethodBuilder Param(IParameterBuilder parameter)
        {
            this.ThrowIfDefined();
            this.parms.Add((FluentParameterBuilder)parameter);
            return this;
        }

        /// <inheritdoc/>
        public IMethodBuilder Params(params Type[] parameterTypes)
        {
            this.ThrowIfDefined();
            this.parms = parameterTypes.Select(
                t => new FluentParameterBuilder(t, null, ParameterAttributes.None))
                .ToList();
            return this;
        }

        /// <inheritdoc/>
        public IMethodBuilder Params(params IParameterBuilder[] parameters)
        {
            this.ThrowIfDefined();
            this.parms = parameters
                .Cast<FluentParameterBuilder>()
                .ToList();
            return this;
        }

        /// <inheritdoc/>
        public IParameterBuilder CreateParam<TParam>(string parameterName, ParameterAttributes attrs = ParameterAttributes.None)
        {
            return this.CreateParam(typeof(TParam), parameterName, attrs);
        }

        /// <inheritdoc/>
        public IParameterBuilder CreateParam(Type parameterType, string parameterName, ParameterAttributes attrs = ParameterAttributes.None)
        {
            this.ThrowIfDefined();
            return new FluentParameterBuilder(
                    parameterType,
                    parameterName,
                    attrs);
        }

        /// <inheritdoc/>
        public bool HasParameter(string parameterName)
        {
            return this.parms.Any(p => p.ParameterName == parameterName);
        }

        /// <inheritdoc/>
        public IParameterBuilder GetParameter(string parameterName)
        {
            return this.parms.FirstOrDefault(p => p.ParameterName == parameterName);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public GenericTypeParameterBuilder GetGenericParameter(string parameterName)
        {
            return this.genericParameters
                .FirstOrDefault(g => g.Name == parameterName);
        }

        /// <inheritdoc/>
        public IMethodBuilder SetCustomAttribute(CustomAttributeBuilder customAttribute)
        {
            this.customAttributes = this.customAttributes ?? new List<CustomAttributeBuilder>();
            this.customAttributes.Add(customAttribute);
            return this;
        }

        /// <inheritdoc/>
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
                var paramBuilder = this.methodBuilder
                    .DefineParameter(++parmIndex, parm.Attributes, parm.ParameterName);

                parm.CustomAttributes.SetCustomAttributes(a => paramBuilder.SetCustomAttribute(a));
            }

            this.customAttributes.SetCustomAttributes(a => this.methodBuilder.SetCustomAttribute(a));

            DebugOutput.WriteLine("");
            DebugOutput.WriteLine("=======================================");
            DebugOutput.Write($"New Method {this.methodBuilder.Name}");
            if (this.methodBuilder.IsGenericMethodDefinition == true)
            {
                DebugOutput.Write($"<{string.Join(", ", this.methodBuilder.GetGenericArguments().Select(t => t.Name))}>");
            }

            DebugOutput.WriteLine($"({string.Join(", ", this.parms.Select(p => $"{p.ParameterType} {p.ParameterName}"))})");
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