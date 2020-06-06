namespace FluentIL.Emitters
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// <see cref="ILGenerator"/> implementation of the <see cref="IEmitter"/> interface.
    /// </summary>
    internal class ILGeneratorEmitter
        : EmitterBase
    {
        /// <summary>
        /// The <see cref="ILGenerator"/> to use.
        /// </summary>
        private ILGenerator generator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ILGeneratorEmitter"/> class.
        /// </summary>
        /// <param name="generator">The <see cref="ILGenerator"/> to use.</param>
        public ILGeneratorEmitter(ILGenerator generator)
        {
            this.generator = generator;
        }

        /// <inheritdoc/>
        public override int ILOffset => this.generator.ILOffset;

        /// <inheritdoc/>
        public override IEmitter BeginCatchBlock(Type exceptionType)
        {
            this.generator.BeginCatchBlock(exceptionType);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginExceptFilterBlock()
        {
            this.generator.BeginExceptFilterBlock();
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginExceptionBlock(out ILabel label)
        {
            var actualLabel = this.generator.BeginExceptionBlock();
            label = new LabelAdapter(Guid.NewGuid().ToString(), actualLabel);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginExceptionBlock(ILabel label)
        {
            var actualLabel = this.generator.BeginExceptionBlock();
            ((IAdaptedLabel)label).Label = actualLabel;
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginFaultBlock()
        {
            this.generator.BeginFaultBlock();
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginFinallyBlock()
        {
            this.generator.BeginFinallyBlock();
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginScope()
        {
            this.generator.BeginScope();
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter DeclareLocal(Type localType, out ILocal local)
        {
            return this.DeclareLocal(localType, Guid.NewGuid().ToString(), false, out local);
        }

        /// <inheritdoc/>
        public override IEmitter DeclareLocal(Type localType, string localName, out ILocal local)
        {
            return this.DeclareLocal(localType, localName, false, out local);
        }

        /// <inheritdoc/>
        public override IEmitter DeclareLocal(Type localType, bool pinned, out ILocal local)
        {
            return this.DeclareLocal(localType, Guid.NewGuid().ToString(), pinned, out local);
        }

        /// <inheritdoc/>
        public override IEmitter DeclareLocal(Type localType, string localName, bool pinned, out ILocal local)
        {
            var actualLocal = this.generator.DeclareLocal(localType, pinned);
            local = new LocalAdapter(localName, localType, actualLocal.LocalIndex, pinned, actualLocal);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter DeclareLocal(IGenericParameterBuilder genericParameter, out ILocal local)
        {
            // var actualLocal = this.generator.DeclareLocal(localType, pinned);
            local = new LocalAdapter(Guid.NewGuid().ToString(), genericParameter);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter DeclareLocal(Type localGenericTypeDefinition, IGenericParameterBuilder[] genericTypeArgs, out ILocal local)
        {
            // var actualLocal = this.generator.DeclareLocal(localType, pinned);
            local = new LocalAdapter(Guid.NewGuid().ToString(), localGenericTypeDefinition, genericTypeArgs);
            return this;
        }

        /// <inheritdoc />
        public override IEmitter DeclareLocal(ITypeBuilder typeBuilder, out ILocal local)
        {
            var actualLocal = this.generator.DeclareLocal(typeBuilder.CreateType(), false);
            local = new LocalAdapter(Guid.NewGuid().ToString(), actualLocal.LocalType, actualLocal.LocalIndex, false, actualLocal);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter DeclareLocal(ILocal local)
        {
            var actualLocal = this.generator.DeclareLocal(local.LocalType, local.IsPinned);
            ((IAdaptedLocal)local).Local = actualLocal;
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter DefineLabel(out ILabel label)
        {
            return this.DefineLabel(Guid.NewGuid().ToString(), out label);
        }

        /// <inheritdoc/>
        public override IEmitter DefineLabel(string labelName, out ILabel label)
        {
            var actualLabel = this.generator.DefineLabel();
            label = new LabelAdapter(labelName, actualLabel);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter DefineLabel(ILabel label)
        {
            ((IAdaptedLabel)label).Label = this.generator.DefineLabel();
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, Type type)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, type);

            if (type == null)
            {
                WriteLineError("\t!Null Type!");
                return this;
            }

            WriteLine("\t[{0}]", type.Name);

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, string str)
        {
            this.generator.Emit(opcode, str);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, float arg)
        {
            this.generator.Emit(opcode, arg);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, sbyte arg)
        {
            this.generator.Emit(opcode, arg);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, MethodInfo methodInfo)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, methodInfo);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, FieldInfo field)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, field);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, IFieldBuilder field)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, field.Define());
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, ILabel[] labels)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, labels?.Select(l => ((Label)((IAdaptedLabel)l).Label)).ToArray());
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, SignatureHelper signature)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, signature);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, ILocal local)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, ((IAdaptedLocal)local)?.Local as LocalBuilder);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, ConstructorInfo con)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, con);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, long arg)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, arg);
            WriteLine("{0}", arg);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, int arg)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, arg);
            WriteLine("{0}", arg);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, short arg)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, arg);
            WriteLine("{0}", arg);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, double arg)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, arg);
            WriteLine("{0}", arg);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, byte arg)
        {
            OutputOpCode(this.generator, opcode);
            this.generator.Emit(opcode, arg);
            WriteLine("{0}", arg);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode)
        {
            this.generator.Emit(opcode);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, ILabel label)
        {
            this.generator.Emit(opcode, (Label)((IAdaptedLabel)label).Label);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitCall(OpCode opcode, Func<MethodInfo> action)
        {
            this.generator.Emit(opcode, action());
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
        {
            this.generator.EmitCall(opcode, methodInfo, optionalParameterTypes);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
        {
            this.generator.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitWriteLine(FieldInfo fld)
        {
            this.generator.EmitWriteLine(fld);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitWriteLine(string value)
        {
            this.generator.EmitWriteLine(value);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitWriteLine(ILocal local)
        {
            this.generator.EmitWriteLine((LocalBuilder)((IAdaptedLocal)local).Local);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EndExceptionBlock()
        {
            this.generator.EndExceptionBlock();
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EndScope()
        {
            this.generator.EndScope();
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter MarkLabel(ILabel label)
        {
            this.generator.MarkLabel((Label)((IAdaptedLabel)label).Label);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter ThrowException(Type excType)
        {
            this.generator.ThrowException(excType);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter UsingNamespace(string usingNamespace)
        {
            this.generator.UsingNamespace(usingNamespace);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Comment(string comment)
        {
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Defer(Action<IEmitter> action)
        {
            action(this);
            return this;
        }

        /// <inheritdoc/>
        public override void EmitIL(ILGenerator generator)
        {
        }
    }
}