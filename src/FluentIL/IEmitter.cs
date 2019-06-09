namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Defines the emitter interface.
    /// </summary>
    public interface IEmitter
    {
        /// <summary>
        /// Gets the IL offset.
        /// </summary>
        /// <returns>The offset.</returns>
        int ILOffset { get; }

        /// <summary>
        /// Begins a catch block.
        /// </summary>
        /// <param name="exceptionType">The exception type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter BeginCatchBlock(Type exceptionType);

        /// <summary>
        /// Begins an except filter block.
        /// </summary>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter BeginExceptFilterBlock();

        /// <summary>
        /// Begins an exception block.
        /// </summary>
        /// <param name="label">A variable to receive a label.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter BeginExceptionBlock(out ILabel label);

        /// <summary>
        /// Begins an exception block.
        /// </summary>
        /// <param name="label">A label.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter BeginExceptionBlock(ILabel label);

        /// <summary>
        /// Begins a fault block.
        /// </summary>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter BeginFaultBlock();

        /// <summary>
        /// Begins a finally block.
        /// </summary>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter BeginFinallyBlock();

        /// <summary>
        /// Begins a scope.
        /// </summary>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter BeginScope();

        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <param name="localType">The local type.</param>
        /// <param name="local">A variable to receive the local.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter DeclareLocal(Type localType, out ILocal local);

        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <param name="localType">The local type.</param>
        /// <param name="localName">The name of the local.</param>
        /// <param name="local">A variable to receive the local.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter DeclareLocal(Type localType, string localName, out ILocal local);

        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <param name="localType">The local type.</param>
        /// <param name="pinned">A value indicating whether or not the local is pinned.</param>
        /// <param name="local">A variable to receive the local.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter DeclareLocal(Type localType, bool pinned, out ILocal local);

        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <param name="localType">The local type.</param>
        /// <param name="localName">The name of the local.</param>
        /// <param name="pinned">A value indicating whether or not the local is pinned.</param>
        /// <param name="local">A variable to receive the local.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter DeclareLocal(Type localType, string localName, bool pinned, out ILocal local);

        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <param name="local">A local.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter DeclareLocal(ILocal local);

        /// <summary>
        /// Defines a label.
        /// </summary>
        /// <param name="label">A variable to receive the label.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter DefineLabel(out ILabel label);

        /// <summary>
        /// Defines a label.
        /// </summary>
        /// <param name="labelName">The label name.</param>
        /// <param name="label">A variable to receive the label.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter DefineLabel(string labelName, out ILabel label);

        /// <summary>
        /// Defines a label.
        /// </summary>
        /// <param name="label">A label.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter DefineLabel(ILabel label);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="Type"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="type">The <see cref="Type"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, Type type);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="string"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="str">The <see cref="string"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, string str);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="float"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="arg">The <see cref="float"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, float arg);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="sbyte"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="arg">The <see cref="sbyte"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, sbyte arg);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="MethodInfo"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="meth">The <see cref="MethodInfo"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, MethodInfo meth);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="FieldInfo"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="field">The <see cref="FieldInfo"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, FieldInfo field);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="IFieldBuilder"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="field">The <see cref="IFieldBuilder"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, IFieldBuilder field);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="ILabel"/> array.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="labels">The <see cref="ILabel"/> array.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, ILabel[] labels);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="SignatureHelper"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="signature">The <see cref="SignatureHelper"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, SignatureHelper signature);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="ILocal"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="local">The <see cref="ILocal"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, ILocal local);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="ConstructorInfo"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="con">The <see cref="ConstructorInfo"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, ConstructorInfo con);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="long"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="arg">The <see cref="long"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, long arg);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="int"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="arg">The <see cref="int"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, int arg);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="short"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="arg">The <see cref="short"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, short arg);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="double"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="arg">The <see cref="double"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, double arg);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="byte"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="arg">The <see cref="byte"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, byte arg);

        /// <summary>
        /// Emits an <see cref="OpCode"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode);

        /// <summary>
        /// Emits an <see cref="OpCode"/> that accepts a <see cref="ILabel"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCode"/>.</param>
        /// <param name="label">The <see cref="ILabel"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Emit(OpCode opcode, ILabel label);

        /// <summary>
        /// Emits IL to perform a <see cref="OpCodes.Call"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCodes.Call"/> opcode.</param>
        /// <param name="methodInfo">The method to call.</param>
        /// <param name="optionalParameterTypes">Optional parameter types.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes);

        /// <summary>
        /// Emits IL to perform a <see cref="OpCodes.Calli"/>.
        /// </summary>
        /// <param name="opcode">The <see cref="OpCodes.Calli"/> opcode.</param>
        /// <param name="callingConvention">The calling convention.</param>
        /// <param name="returnType">The return type.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="optionalParameterTypes">Any optinal parameter types.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes);

        /// <summary>
        /// Emits IL to write a <see cref="FieldInfo"/> to stdout.
        /// </summary>
        /// <param name="fld">The field toe write.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter EmitWriteLine(FieldInfo fld);

        /// <summary>
        /// Emits IL to write a <see cref="string"/> to stdout.
        /// </summary>
        /// <param name="value">The string value to write.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter EmitWriteLine(string value);

        /// <summary>
        /// Emits IL to write a <see cref="ILocal"/> to stdout.
        /// </summary>
        /// <param name="local">The local variable to write out.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter EmitWriteLine(ILocal local);

        /// <summary>
        /// Emits an end exception block.
        /// </summary>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter EndExceptionBlock();

        /// <summary>
        /// Emits an end scope.
        /// </summary>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter EndScope();

        /// <summary>
        /// Marks a label.
        /// </summary>
        /// <param name="label">The labal to mark.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter MarkLabel(ILabel label);

        /// <summary>
        /// Emits a throw exception.
        /// </summary>
        /// <param name="excType">The type of exception to throw.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter ThrowException(Type excType);

        /// <summary>
        /// Emits a using namespace.
        /// </summary>
        /// <param name="usingNamespace">The namespace to emit.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter UsingNamespace(string usingNamespace);

        /// <summary>
        /// Emits nothing. Used to allow comments to be added to a debug output class.
        /// </summary>
        /// <param name="comment">A comment.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        IEmitter Comment(string comment);
    }
}