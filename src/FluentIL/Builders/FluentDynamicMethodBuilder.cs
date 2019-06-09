namespace FluentIL.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL.Emitters;

    /// <summary>
    /// Implementation of the <see cref="IDynamicMethodBuilder"/> interface.
    /// </summary>
    internal class FluentDynamicMethodBuilder
        : IDynamicMethodBuilder
    {
        /// <summary>
        /// The methods name.
        /// </summary>
        private string methodName;

        /// <summary>
        /// The methods owning type.
        /// </summary>
        private Type methodOwner;

        /// <summary>
        /// The methods return type.
        /// </summary>
        private Type returnType;

        /// <summary>
        /// The methods parameters.
        /// </summary>
        private List<FluentParameterBuilder> parms = new List<FluentParameterBuilder>();

        /// <summary>
        /// The dynamic method.
        /// </summary>
        private DynamicMethod dynamicMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentDynamicMethodBuilder"/> class.
        /// </summary>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="methodOwner">The methods owning type.</param>
        internal FluentDynamicMethodBuilder(string methodName, Type methodOwner)
        {
            this.methodName = methodName;
            this.methodOwner = methodOwner;
            this.returnType = typeof(void);
        }

        /// <summary>
        /// Throws an exception if the method has been defined.
        /// </summary>
        private void ThrowIfDefined()
        {
            if (this.dynamicMethod != null)
            {
                throw new InvalidOperationException("Method already defined");
            }
        }

        /// <inheritdoc />
        public IEmitter Body()
        {
            this.Define();

            var il = this.dynamicMethod.GetILGenerator();
            var emitter = new ILGeneratorEmitter(il);
            if (DebugOutput.Output == null)
            {
                return emitter;
            }

            return new DebugEmitter(emitter, DebugOutput.Output);
        }

        /// <inheritdoc />
        public IDynamicMethodBuilder Body(Action<IEmitter> action)
        {
            action(this.Body());
            return this;
        }

        /// <inheritdoc/>
        public IDynamicMethodBuilder Returns<TReturn>()
        {
            return this.Returns(typeof(TReturn));
        }

        /// <inheritdoc/>
        public IDynamicMethodBuilder Returns(Type returnType)
        {
            this.ThrowIfDefined();
            this.returnType = returnType;
            return this;
        }

        /// <inheritdoc/>
        public IDynamicMethodBuilder Param<TParam>(string parameterName, ParameterAttributes attrs = ParameterAttributes.None)
        {
            return this.Param(typeof(TParam), parameterName, attrs);
        }

        /// <inheritdoc/>
        public IDynamicMethodBuilder Param(Type parameterType, string parameterName, ParameterAttributes attrs = ParameterAttributes.None)
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
        public IDynamicMethodBuilder Param(Action<IParameterBuilder> action)
        {
            this.ThrowIfDefined();
            var builder = new FluentParameterBuilder();
            this.parms.Add(builder);
            action(builder);
            return this;
        }

        /// <inheritdoc/>
        public IDynamicMethodBuilder Param(IParameterBuilder parameter)
        {
            this.ThrowIfDefined();
            this.parms.Add((FluentParameterBuilder)parameter);
            return this;
        }

        /// <inheritdoc/>
        public IDynamicMethodBuilder Params(params Type[] parameterTypes)
        {
            this.ThrowIfDefined();
            this.parms = parameterTypes.Select(
                t => new FluentParameterBuilder(t, null, ParameterAttributes.None))
                .ToList();
            return this;
        }

        /// <inheritdoc/>
        public IDynamicMethodBuilder Params(params IParameterBuilder[] parameters)
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

        /// <summary>
        /// Defines the method.
        /// </summary>
        /// <returns>A <see cref="DynamicMethod"/> instance.</returns>
        public DynamicMethod Define()
        {
            if (this.dynamicMethod == null)
            {
                int parmCount = this.parms.Count;
                Type[] parameterTypes = new Type[parmCount];
                for (int i = 0; i < parmCount; i++)
                {
                    parameterTypes[i] = this.parms[i].ParameterType;
                }

                this.dynamicMethod = new DynamicMethod(this.methodName, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, this.returnType, parameterTypes, this.methodOwner, false);

                /*
                int parmIndex = 0;
                foreach (var parm in this.parms)
                {
                    var paramBuilder = dynamicMethod
                        .DefineParameter(++parmIndex, parm.Attributes, parm.ParameterName);

                    parm.CustomAttributes.SetCustomAttributes(a => paramBuilder.SetCustomAttribute(a));
                }
                */
            }

            return this.dynamicMethod;
        }
    }
}