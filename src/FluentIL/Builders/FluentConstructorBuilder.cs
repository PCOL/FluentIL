namespace FluentIL.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL.Emitters;

    /// <summary>
    /// An implementation of the <see cref="IConstructorBuilder"/> interface.
    /// </summary>
    internal class FluentConstructorBuilder
        : IConstructorBuilder
    {
        /// <summary>
        /// A function to define the constructor.
        /// </summary>
        private readonly Func<MethodAttributes, CallingConventions, Type[], Type[][], Type[][], ConstructorBuilder> define;

        private readonly Func<MethodAttributes, ConstructorBuilder> defineDefault;

        /// <summary>
        /// The constructors calling conversion.
        /// </summary>
        private CallingConventions callingConvention;

        /// <summary>
        /// A list of constructor parameters.
        /// </summary>
        private List<FluentParameterBuilder> parameters = new List<FluentParameterBuilder>();

        /// <summary>
        /// The constuctor builder.
        /// </summary>
        private ConstructorBuilder ctor;

        /// <summary>
        /// The method implementation attributes.
        /// </summary>
        private MethodImplAttributes methodImplAttributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentConstructorBuilder"/> class.
        /// </summary>
        /// <param name="define">A constructor definition function.</param>
        public FluentConstructorBuilder(
            Func<MethodAttributes, CallingConventions, Type[], Type[][], Type[][], ConstructorBuilder> define)
        {
            this.define = define;
            this.callingConvention = CallingConventions.HasThis;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentConstructorBuilder"/> class.
        /// </summary>
        /// <param name="define">A default constructor definition function.</param>
        public FluentConstructorBuilder(
            Func<MethodAttributes,
            ConstructorBuilder> define)
        {
            this.defineDefault = define;
        }

        /// <inheritdoc />
        public MethodAttributes MethodAttributes { get; set; }

        /// <inheritdoc />
        public IEmitter Body()
        {
            return this.Define().Body();
        }

        /// <inheritdoc />
        public IConstructorBuilder CallingConvention(CallingConventions callingConvention)
        {
            this.callingConvention = callingConvention;
            return this;
        }

        /// <inheritdoc />
        public IConstructorBuilder Param(Type parameterType, string parameterName, ParameterAttributes attrs = ParameterAttributes.None)
        {
            this.parameters.Add(
                new FluentParameterBuilder(
                    parameterType,
                    parameterName,
                    attrs));

            return this;
        }

        /// <inheritdoc />
        public IConstructorBuilder Param(Action<IParameterBuilder> action)
        {
            var builder = new FluentParameterBuilder();
            this.parameters.Add(builder);
            action(builder);
            return this;
        }

        /// <inheritdoc />
        public IConstructorBuilder Params(params Type[] parameterTypes)
        {
            this.parameters = parameterTypes.Select(
                t => new FluentParameterBuilder(t, null, ParameterAttributes.None))
                .ToList();
            return this;
        }

        /// <inheritdoc />
        public IConstructorBuilder SetMethodAttributes(MethodAttributes attributes)
        {
            this.MethodAttributes = attributes;
            return this;
        }

        /// <inheritdoc />
        public IConstructorBuilder SetImplementationFlags(MethodImplAttributes attributes)
        {
            this.methodImplAttributes = attributes;
            return this;
        }

        /// <inheritdoc />
        public ConstructorBuilder Define()
        {
            if (this.ctor != null)
            {
                return this.ctor;
            }

            if (this.define != null)
            {
                this.ctor = this.define(
                    this.MethodAttributes,
                    this.callingConvention,
                    this.parameters.Select(p => p.ParameterType).ToArray(),
                    null,
                    null);

                int i = 0;
                foreach (var parm in this.parameters)
                {
                    this.ctor.DefineParameter(++i, parm.Attributes, parm.ParameterName);
                }

                this.ctor.SetImplementationFlags(this.methodImplAttributes);
            }
            else if (this.defineDefault != null)
            {
                this.ctor = this.defineDefault(this.MethodAttributes);
            }

            DebugOutput.WriteLine("=======================================");
            DebugOutput.WriteLine("New Constructor ({0})", string.Join(", ", this.parameters.Select(p => $"{p.ParameterType} {p.ParameterName}")));
            DebugOutput.WriteLine("Calling Convention: {0}", this.callingConvention);
            DebugOutput.WriteLine(string.Empty);

            return this.ctor;
        }
    }
}