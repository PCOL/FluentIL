namespace FluentIL.Emitters
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Emitter base class.
    /// </summary>
    internal abstract class EmitterBase
        : IEmitter
    {
        /// <inheritdoc />
        public abstract int ILOffset { get; }

        /// <inheritdoc />
        public abstract IEmitter BeginCatchBlock(Type exceptionType);

        /// <inheritdoc />
        public abstract IEmitter BeginExceptFilterBlock();

        /// <inheritdoc />
        public abstract IEmitter BeginExceptionBlock(out ILabel label);

        /// <inheritdoc />
        public abstract IEmitter BeginExceptionBlock(ILabel label);

        /// <inheritdoc />
        public abstract IEmitter BeginFaultBlock();

        /// <inheritdoc />
        public abstract IEmitter BeginFinallyBlock();

        /// <inheritdoc />
        public abstract IEmitter BeginScope();

        /// <inheritdoc />
        public abstract IEmitter Comment(string comment);

        /// <inheritdoc />
        public abstract IEmitter DeclareLocal(Type localType, out ILocal local);

        /// <inheritdoc />
        public abstract IEmitter DeclareLocal(Type localType, string localName, out ILocal local);

        /// <inheritdoc />
        public abstract IEmitter DeclareLocal(Type localType, bool pinned, out ILocal local);

        /// <inheritdoc />
        public abstract IEmitter DeclareLocal(Type localType, string localName, bool pinned, out ILocal local);

        /// <inheritdoc />
        public abstract IEmitter DeclareLocal(IGenericParameterBuilder genericType, out ILocal local);

        /// <inheritdoc />
        public abstract IEmitter DeclareLocal(Type localGenericTypeDefinition, IGenericParameterBuilder[] genericTypeArgs, out ILocal local);

        /// <inheritdoc />
        public abstract IEmitter DeclareLocal(ITypeBuilder typeBuilder, out ILocal local);

        /// <inheritdoc />
        public abstract IEmitter DeclareLocal(ILocal local);

        /// <inheritdoc />
        public abstract IEmitter DefineLabel(out ILabel label);

        /// <inheritdoc />
        public abstract IEmitter DefineLabel(string labelName, out ILabel label);

        /// <inheritdoc />
        public abstract IEmitter DefineLabel(ILabel label);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, Type type);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, string str);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, float arg);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, sbyte arg);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, MethodInfo meth);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, FieldInfo field);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, IFieldBuilder field);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, ILabel[] labels);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, SignatureHelper signature);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, ILocal local);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, ConstructorInfo con);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, long arg);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, int arg);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, short arg);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, double arg);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, byte arg);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode);

        /// <inheritdoc />
        public abstract IEmitter Emit(OpCode opcode, ILabel label);

        /// <inheritdoc />
        public abstract IEmitter EmitCall(OpCode opcode, Func<MethodInfo> action);

        /// <inheritdoc />
        public abstract IEmitter EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes);

        /// <inheritdoc />
        public abstract IEmitter EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes);

        /// <inheritdoc />
        public abstract IEmitter EmitWriteLine(FieldInfo fld);

        /// <inheritdoc />
        public abstract IEmitter EmitWriteLine(string value);

        /// <inheritdoc />
        public abstract IEmitter EmitWriteLine(ILocal local);

        /// <inheritdoc />
        public abstract IEmitter EndExceptionBlock();

        /// <inheritdoc />
        public abstract IEmitter EndScope();

        /// <inheritdoc />
        public abstract IEmitter MarkLabel(ILabel label);

        /// <inheritdoc />
        public abstract IEmitter ThrowException(Type excType);

        /// <inheritdoc />
        public abstract IEmitter UsingNamespace(string usingNamespace);

        /// <inheritdoc />
        public abstract IEmitter Defer(Action<IEmitter> action);

        /// <summary>
        /// Emits the IL.
        /// </summary>
        /// <param name="generator">An IL generator.</param>
        public abstract void EmitIL(ILGenerator generator);

        /// <summary>
        /// Outputs the <see cref="OpCode"/>.
        /// </summary>
        /// <param name="generator">The IL Generator.</param>
        /// <param name="opcode">An OpCode.</param>
        protected static void OutputOpCode(ILGenerator generator, OpCode opcode)
        {
            OutputILOffset(generator);
            DebugOutput.Output?.Write(" {0}", opcode.ToString().PadRight(12, ' '));
        }

        /// <summary>
        /// Output the IL offset.
        /// </summary>
        /// <param name="generator">The IL Generator.</param>
        protected static void OutputILOffset(ILGenerator generator)
        {
            Write($"IL_{generator.ILOffset.ToString("x4")}:");
        }

        protected static void WriteType(Type type, string name)
        {
            WriteTypeAttributes(type);
            WriteColor(ConsoleColor.DarkCyan, " {0}", type.Name);

#if NETSTANDARD_20
            if (type.IsGenericType == true)
#else
            if (type.GetTypeInfo().IsGenericType == true)
#endif
            {
                foreach (var genType in type.GetGenericArguments())
                {
                    Write(" ");
                    WriteTypeAttributes(genType);
                    WriteColor(ConsoleColor.DarkCyan, " {0}", genType.Name);
                }
            }

            WriteLineColor(ConsoleColor.Cyan, "::{0}", name);
        }

        private static void WriteTypeAttributes(Type type)
        {
#if NETSTANDARD_20
            if (type.IsValueType == true)
#else
            if (type.GetTypeInfo().IsValueType == true)
#endif
            {
                WriteColor(ConsoleColor.Blue, "valuetype");
            }
            else
            {
                WriteColor(ConsoleColor.Blue, "class");
            }
        }

        /// <summary>
        /// Writes to debug output.
        /// </summary>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        protected static void Write(string format, params object[] args)
        {
            DebugOutput.Output?.Write(format, args);
        }

        /// <summary>
        /// Writes a line to debug output.
        /// </summary>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        protected static void WriteLine(string format, params object[] args)
        {
            DebugOutput.Output?.WriteLine(format, args);
        }

        /// <summary>
        /// Writes to debug output in a specified colour.
        /// </summary>
        /// <param name="color">The colour of the string.</param>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        protected static void WriteColor(ConsoleColor color, string format, params object[] args)
        {
            DebugOutput.Output?.WriteColor(color, format, args);
        }

        /// <summary>
        /// Writes a line to debug output in a specified colour.
        /// </summary>
        /// <param name="color">The colour of the string.</param>
        /// <param name="format">A string format.</param>
        /// <param name="args">The format arguments.</param>
        protected static void WriteLineColor(ConsoleColor color, string format, params object[] args)
        {
            DebugOutput.Output?.WriteLineColor(color, format, args);
        }

        /// <summary>
        /// Outputs an error.
        /// </summary>
        /// <param name="format">A format string.</param>
        /// <param name="args">A list of arguments.</param>
        protected static void WriteError(string format, params object[] args)
        {
            DebugOutput.Output?.WriteColor(ConsoleColor.Red, format, args);
        }

        /// <summary>
        /// Outputs an error.
        /// </summary>
        /// <param name="format">A format string.</param>
        /// <param name="args">A list of arguments.</param>
        protected static void WriteLineError(string format, params object[] args)
        {
            DebugOutput.Output?.WriteLineColor(ConsoleColor.Red, format, args);
        }
    }
}