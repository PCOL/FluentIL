namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    public interface IEmitter
    {
        int ILOffset { get; }

        IEmitter BeginCatchBlock(Type exceptionType);

        IEmitter BeginExceptFilterBlock();

        IEmitter BeginExceptionBlock(out ILabel label);

        IEmitter BeginExceptionBlock(ILabel label);
        
        IEmitter BeginFaultBlock();

        IEmitter BeginFinallyBlock();

        IEmitter BeginScope();

        IEmitter DeclareLocal(Type localType, out ILocal local);

        IEmitter DeclareLocal(Type localType, string localName, out ILocal local);

        IEmitter DeclareLocal(Type localType, bool pinned, out ILocal local);

        IEmitter DeclareLocal(Type localType, string localName, bool pinned, out ILocal local);

        IEmitter DeclareLocal(ILocal local);

        IEmitter DefineLabel(out ILabel label);

        IEmitter DefineLabel(string labelName, out ILabel label);

        IEmitter DefineLabel(ILabel label);

        IEmitter Emit(OpCode opcode, Type type);

        IEmitter Emit(OpCode opcode, string str);

        IEmitter Emit(OpCode opcode, float arg);

        IEmitter Emit(OpCode opcode, sbyte arg);

        IEmitter Emit(OpCode opcode, MethodInfo meth);

        IEmitter Emit(OpCode opcode, FieldInfo field);

        IEmitter Emit(OpCode opcode, IFieldBuilder field);

        IEmitter Emit(OpCode opcode, ILabel[] labels);

        IEmitter Emit(OpCode opcode, SignatureHelper signature);

        IEmitter Emit(OpCode opcode, ILocal local);

        IEmitter Emit(OpCode opcode, ConstructorInfo con);

        IEmitter Emit(OpCode opcode, long arg);

        IEmitter Emit(OpCode opcode, int arg);

        IEmitter Emit(OpCode opcode, short arg);

        IEmitter Emit(OpCode opcode, double arg);

        IEmitter Emit(OpCode opcode, byte arg);

        IEmitter Emit(OpCode opcode);

        IEmitter Emit(OpCode opcode, ILabel label);

        IEmitter EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes);

        IEmitter EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes);

        IEmitter EmitWriteLine(FieldInfo fld);

        IEmitter EmitWriteLine(string value);

        IEmitter EmitWriteLine(ILocal local);

        IEmitter EndExceptionBlock();

        IEmitter EndScope();

        IEmitter MarkLabel(ILabel loc);

        IEmitter ThrowException(Type excType);

        IEmitter UsingNamespace(string usingNamespace);
    }
}