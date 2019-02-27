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
        : IEmitter
    {
        /// <summary>
        /// The <see cref="ILGenerator"/> to use.
        /// </summary>
        private ILGenerator ilGen;

        /// <summary>
        /// Initialises a new instance of the <see cref="ILGeneratorEmitter"/> class.
        /// </summary>
        /// <param name="ilGen">The <see cref="ILGenerator"/> to use.</param>
        public ILGeneratorEmitter(ILGenerator ilGen)
        {
            this.ilGen = ilGen;
        }

        /// <inheritdoc/>
        public int ILOffset => this.ilGen.ILOffset;

        /// <inheritdoc/>
        public IEmitter BeginCatchBlock(Type exceptionType)
        {
            this.ilGen.BeginCatchBlock(exceptionType);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter BeginExceptFilterBlock()
        {
            this.ilGen.BeginExceptFilterBlock();
            return this;
        }

        /// <inheritdoc/>
        public IEmitter BeginExceptionBlock(out ILabel label)
        {
            var actualLabel = this.ilGen.BeginExceptionBlock();
            label = new LabelAdapter(Guid.NewGuid().ToString(), actualLabel);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter BeginExceptionBlock(ILabel label)
        {
            var actualLabel = this.ilGen.BeginExceptionBlock();
            ((IAdaptedLabel)label).Label = actualLabel;
            return this;
        }

        /// <inheritdoc/>
        public IEmitter BeginFaultBlock()
        {
            this.ilGen.BeginFaultBlock();
            return this;
        }

        /// <inheritdoc/>
        public IEmitter BeginFinallyBlock()
        {
            this.ilGen.BeginFinallyBlock();
            return this;
        }

        /// <inheritdoc/>
        public IEmitter BeginScope()
        {
            this.ilGen.BeginScope();
            return this;
        }

        /// <inheritdoc/>
        public IEmitter DeclareLocal(Type localType, out ILocal local)
        {
            return this.DeclareLocal(localType, Guid.NewGuid().ToString(), false, out local);
        }

        /// <inheritdoc/>
        public IEmitter DeclareLocal(Type localType, string localName, out ILocal local)
        {
            return this.DeclareLocal(localType, localName, false, out local);
        }

        /// <inheritdoc/>
        public IEmitter DeclareLocal(Type localType, bool pinned, out ILocal local)
        {
            return this.DeclareLocal(localType, Guid.NewGuid().ToString(), pinned, out local);
        }

        /// <inheritdoc/>
        public IEmitter DeclareLocal(Type localType, string localName, bool pinned, out ILocal local)
        {
            var actualLocal = this.ilGen.DeclareLocal(localType, pinned);
            local = new LocalAdapter(localName, localType, actualLocal.LocalIndex, pinned, actualLocal);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter DeclareLocal(ILocal local)
        {
            var actualLocal = this.ilGen.DeclareLocal(local.LocalType, local.IsPinned);
            ((IAdaptedLocal)local).Local = actualLocal;
            return this;
        }

        /// <inheritdoc/>
        public IEmitter DefineLabel(out ILabel label)
        {
            return this.DefineLabel(Guid.NewGuid().ToString(), out label);
        }

        /// <inheritdoc/>
        public IEmitter DefineLabel(string labelName, out ILabel label)
        {
            var actualLabel = this.ilGen.DefineLabel();
            label = new LabelAdapter(labelName, actualLabel);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter DefineLabel(ILabel label)
        {
            ((IAdaptedLabel)label).Label = this.ilGen.DefineLabel();
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, Type type)
        {
            this.ilGen.Emit(opcode, type);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, string str)
        {
            this.ilGen.Emit(opcode, str);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, float arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, sbyte arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, MethodInfo methodInfo)
        {
            this.ilGen.Emit(opcode, methodInfo);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, FieldInfo field)
        {
            this.ilGen.Emit(opcode, field);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, IFieldBuilder field)
        {
            this.ilGen.Emit(opcode, field.Define());
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, ILabel[] labels)
        {
            this.ilGen.Emit(opcode, labels?.Select(l => ((Label)((IAdaptedLabel)l).Label)).ToArray());
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, SignatureHelper signature)
        {
            this.ilGen.Emit(opcode, signature);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, ILocal local)
        {
            this.ilGen.Emit(opcode, ((IAdaptedLocal)local)?.Local as LocalBuilder);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, ConstructorInfo con)
        {
            this.ilGen.Emit(opcode, con);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, long arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, int arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, short arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, double arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, byte arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode)
        {
            this.ilGen.Emit(opcode);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Emit(OpCode opcode, ILabel label)
        {
            this.ilGen.Emit(opcode, (Label)((IAdaptedLabel)label).Label);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
        {
            this.ilGen.EmitCall(opcode, methodInfo, optionalParameterTypes);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
        {
            this.ilGen.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter EmitWriteLine(FieldInfo fld)
        {
            this.ilGen.EmitWriteLine(fld);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter EmitWriteLine(string value)
        {
            this.ilGen.EmitWriteLine(value);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter EmitWriteLine(ILocal local)
        {
            this.ilGen.EmitWriteLine((LocalBuilder)((IAdaptedLocal)local).Local);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter EndExceptionBlock()
        {
            this.ilGen.EndExceptionBlock();
            return this;
        }

        /// <inheritdoc/>
        public IEmitter EndScope()
        {
            this.ilGen.EndScope();
            return this;
        }

        /// <inheritdoc/>
        public IEmitter MarkLabel(ILabel label)
        {
            this.ilGen.MarkLabel((Label)((IAdaptedLabel)label).Label);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter ThrowException(Type excType)
        {
            this.ilGen.ThrowException(excType);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter UsingNamespace(string usingNamespace)
        {
            this.ilGen.UsingNamespace(usingNamespace);
            return this;
        }

        /// <inheritdoc/>
        public IEmitter Comment(string comment)
        {
            return this;
        }
    }
}