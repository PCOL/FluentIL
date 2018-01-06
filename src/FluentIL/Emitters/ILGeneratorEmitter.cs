namespace FluentIL.Emitters
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// I
    /// </summary>
    public class ILGeneratorEmitter
        : IEmitter
    {
        private ILGenerator ilGen;

        public ILGeneratorEmitter(ILGenerator ilGen)
        {
            this.ilGen = ilGen;
        }

        public int ILOffset => this.ilGen.ILOffset;

        public IEmitter BeginCatchBlock(Type exceptionType)
        {
            this.ilGen.BeginCatchBlock(exceptionType);
            return this;
        }

        public IEmitter BeginExceptFilterBlock()
        {
            this.ilGen.BeginExceptFilterBlock();
            return this;
        }

        public IEmitter BeginExceptionBlock(out ILabel label)
        {
            var actualLabel = this.ilGen.BeginExceptionBlock();
            label = new LabelAdapter(Guid.NewGuid().ToString(), actualLabel);
            return this;
        }

        public IEmitter BeginExceptionBlock(ILabel label)
        {
            var actualLabel = this.ilGen.BeginExceptionBlock();
            ((IAdaptedLabel)label).Label = actualLabel;
            return this;
        }

        public IEmitter BeginFaultBlock()
        {
            this.ilGen.BeginFaultBlock();
            return this;
        }

        public IEmitter BeginFinallyBlock()
        {
            this.ilGen.BeginFinallyBlock();
            return this;
        }

        public IEmitter BeginScope()
        {
            this.ilGen.BeginScope();
            return this;
        }

        public IEmitter DeclareLocal(Type localType, out ILocal local)
        {
            return this.DeclareLocal(localType, Guid.NewGuid().ToString(), false, out local);
        }

        public IEmitter DeclareLocal(Type localType, string localName, out ILocal local)
        {
            return this.DeclareLocal(localType, localName, false, out local);
        }

        public IEmitter DeclareLocal(Type localType, bool pinned, out ILocal local)
        {
            return this.DeclareLocal(localType, Guid.NewGuid().ToString(), pinned, out local);
        }

        public IEmitter DeclareLocal(Type localType, string localName, bool pinned, out ILocal local)
        {
            var actualLocal = this.ilGen.DeclareLocal(localType, pinned);
            local = new LocalAdapter(localName, localType, actualLocal.LocalIndex, pinned, actualLocal);
            return this;
        }

        public IEmitter DeclareLocal(ILocal local)
        {
            var actualLocal = this.ilGen.DeclareLocal(local.LocalType, local.IsPinned);
            ((IAdaptedLocal)local).Local = actualLocal;
            return this;
        }

        public IEmitter DefineLabel(out ILabel label)
        {
            return this.DefineLabel(Guid.NewGuid().ToString(), out label);
        }

        public IEmitter DefineLabel(string labelName, out ILabel label)
        {
            var actualLabel = this.ilGen.DefineLabel();
            label = new LabelAdapter(labelName, actualLabel);
            return this;
        }

        public IEmitter DefineLabel(ILabel label)
        {
            ((IAdaptedLabel)label).Label = this.ilGen.DefineLabel();
            return this;
        }

        public IEmitter Emit(OpCode opcode, Type type)
        {
            this.ilGen.Emit(opcode, type);
            return this;
        }

        public IEmitter Emit(OpCode opcode, string str)
        {
            this.ilGen.Emit(opcode, str);
            return this;
        }

        public IEmitter Emit(OpCode opcode, float arg) 
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        public IEmitter Emit(OpCode opcode, sbyte arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        public IEmitter Emit(OpCode opcode, MethodInfo methodInfo) 
        {
            this.ilGen.Emit(opcode, methodInfo);
            return this;
        }

        public IEmitter Emit(OpCode opcode, FieldInfo field)
        {
            this.ilGen.Emit(opcode, field);
            return this;
        }

        public IEmitter Emit(OpCode opcode, IFieldBuilder field)
        {
            this.ilGen.Emit(opcode, field.Define());
            return this;
        }

        public IEmitter Emit(OpCode opcode, ILabel[] labels)
        {
            this.ilGen.Emit(opcode, labels?.Select(l => ((Label)((IAdaptedLabel)l).Label)).ToArray());
            return this;
        }

        public IEmitter Emit(OpCode opcode, SignatureHelper signature)
        {
            this.ilGen.Emit(opcode, signature);
            return this;
        }

        public IEmitter Emit(OpCode opcode, ILocal local)
        {
            this.ilGen.Emit(opcode, ((IAdaptedLocal)local)?.Local as LocalBuilder);
            return this;
        }

        public IEmitter Emit(OpCode opcode, ConstructorInfo con)
        {
            this.ilGen.Emit(opcode, con);
            return this;
        }

        public IEmitter Emit(OpCode opcode, long arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        public IEmitter Emit(OpCode opcode, int arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        public IEmitter Emit(OpCode opcode, short arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        public IEmitter Emit(OpCode opcode, double arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        public IEmitter Emit(OpCode opcode, byte arg)
        {
            this.ilGen.Emit(opcode, arg);
            return this;
        }

        public IEmitter Emit(OpCode opcode)
        {
            this.ilGen.Emit(opcode);
            return this;
        }

        public IEmitter Emit(OpCode opcode, ILabel label)
        {
            this.ilGen.Emit(opcode, (Label)((IAdaptedLabel)label).Label);
            return this;
        }

        public IEmitter EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
        {
            this.ilGen.EmitCall(opcode, methodInfo, optionalParameterTypes);
            return this;
        }

        public IEmitter EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
        {
            this.ilGen.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes);
            return this;
        }

        public IEmitter EmitWriteLine(FieldInfo fld)
        {
            this.ilGen.EmitWriteLine(fld);
            return this;
        }

        public IEmitter EmitWriteLine(string value)
        {
            this.ilGen.EmitWriteLine(value);
            return this;    
        }

        public IEmitter EmitWriteLine(ILocal local)
        {
            this.ilGen.EmitWriteLine((LocalBuilder)((IAdaptedLocal)local).Local);
            return this;
        }

        public IEmitter EndExceptionBlock()
        {
            this.ilGen.EndExceptionBlock();
            return this;
        }

        public IEmitter EndScope()
        {
            this.ilGen.EndScope();
            return this;
        }

        public IEmitter MarkLabel(ILabel label)
        {
            this.ilGen.MarkLabel((Label)((IAdaptedLabel)label).Label);
            return this;
        }

        public IEmitter ThrowException(Type excType)
        {
            this.ilGen.ThrowException(excType);
            return this;
        }

        public IEmitter UsingNamespace(string usingNamespace)
        {
            this.ilGen.UsingNamespace(usingNamespace);
            return this;
        }
    }
}