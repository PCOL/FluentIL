namespace FluentIL.Emitters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// <see cref="ILGenerator"/> implementation of the <see cref="IEmitter"/> interface.
    /// </summary>
    internal class DeferredILGeneratorEmitter
        : EmitterBase
    {
        /// <summary>
        /// The current IL offset.
        /// </summary>
        private int offset = 0;

        /// <summary>
        /// A list of deferred actions.
        /// </summary>
        private List<Action<ILGenerator>> actions = new List<Action<ILGenerator>>();

        /// <inheritdoc/>
        public override int ILOffset => this.offset;

        /// <inheritdoc/>
        public override IEmitter BeginCatchBlock(Type exceptionType)
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.BeginCatchBlock(exceptionType);
                    WriteLineColor(ConsoleColor.Green, "BeginCatchBlock({0})", exceptionType.Name);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginExceptFilterBlock()
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.BeginExceptFilterBlock();
                    WriteLineColor(ConsoleColor.Green, "BeginExceptFilterBlock");
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginExceptionBlock(out ILabel label)
        {
            var lbl = new LabelAdapter(Guid.NewGuid().ToString());

            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    var actualLabel = generator.BeginExceptionBlock();
                    lbl.Label = actualLabel;

                    WriteLineColor(ConsoleColor.Green, "BeginExceptionBlock [{0}]", lbl.Name);
                });

            label = lbl;
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginExceptionBlock(ILabel label)
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    var actualLabel = generator.BeginExceptionBlock();
                    ((IAdaptedLabel)label).Label = actualLabel;

                    WriteLineColor(ConsoleColor.Green, "BeginExceptionBlock [{0}]", label.Name);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginFaultBlock()
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.BeginFaultBlock();
                    WriteLineColor(ConsoleColor.Green, "BeginFaultBlock");
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginFinallyBlock()
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.BeginFinallyBlock();
                    WriteLineColor(ConsoleColor.Green, "BeginFinallyBlock");
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter BeginScope()
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.BeginScope();
                    WriteLineColor(ConsoleColor.Green, "BeginScope");
                });

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
            var loc = new LocalAdapter(localName, localType, 0, pinned, null);

            this.actions.Add(
                generator =>
                {
                    var actualLocal = generator.DeclareLocal(localType, pinned);
                    loc.LocalIndex = actualLocal.LocalIndex;
                    loc.Local = actualLocal;

                    WriteLineColor(ConsoleColor.Yellow, "Local - [{0}] {1} ({2})", loc.LocalIndex, localType.Name, localName);
                });

            local = loc;
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter DeclareLocal(IGenericParameterBuilder genericParameter, out ILocal local)
        {
            var loc = new LocalAdapter(Guid.NewGuid().ToString(), genericParameter);
            this.actions.Add(
                generator =>
                {
                    var type = genericParameter.AsType();
                    var actualLocal = generator.DeclareLocal(type, false);
                    loc.LocalIndex = actualLocal.LocalIndex;
                    loc.LocalType = type;
                    loc.Local = actualLocal;

                    WriteLineColor(ConsoleColor.Yellow, "Local - [{0}] Generic Type ({1})", loc.LocalIndex, genericParameter.ParameterName);
                });

            local = loc;
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter DeclareLocal(Type localGenericTypeDefinition, IGenericParameterBuilder[] genericTypeArgs, out ILocal local)
        {
            var loc = new LocalAdapter(Guid.NewGuid().ToString(), localGenericTypeDefinition, genericTypeArgs);
            this.actions.Add(
                generator =>
                {
                    var type = localGenericTypeDefinition.MakeGenericType(genericTypeArgs.Select(t => t.AsType()).ToArray());
                    var actualLocal = generator.DeclareLocal(type, false);
                    loc.LocalIndex = actualLocal.LocalIndex;
                    loc.LocalType = type;
                    loc.Local = actualLocal;

                    WriteLineColor(ConsoleColor.Yellow, "Local - [{0}] {1} ({2})", loc.LocalIndex, localGenericTypeDefinition.Name, loc.Name);
                });

            local = loc;
            return this;
        }

        /// <inheritdoc />
        public override IEmitter DeclareLocal(ITypeBuilder typeBuilder, out ILocal local)
        {
            var loc = new LocalAdapter(Guid.NewGuid().ToString());

            this.actions.Add(
                generator =>
                {
#if NETSTANDARD_20
                    var typeBuilderType = typeBuilder.Define();
#else
                    var typeBuilderType = typeBuilder.Define().CreateTypeInfo().AsType();
#endif
                    var actualLocal = generator.DeclareLocal(typeBuilderType, false);
                    loc.LocalType = typeBuilderType;
                    loc.LocalIndex = actualLocal.LocalIndex;
                    loc.Local = actualLocal;

                    WriteLineColor(ConsoleColor.Yellow, "Local - [{0}] {1} ({2})", loc.LocalIndex, typeBuilder.TypeName, loc.Name);
                });

            local = loc;
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter DeclareLocal(ILocal local)
        {
            this.actions.Add(
                generator =>
                {
                    var actualLocal = generator.DeclareLocal(local.LocalType, local.IsPinned);
                    ((IAdaptedLocal)local).Local = actualLocal;
                    ((LocalAdapter)local).LocalIndex = actualLocal.LocalIndex;

                    WriteLineColor(ConsoleColor.Yellow, "Local - [{0}] {1}", actualLocal.LocalIndex, local.Name);
                });

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
            var lbl = new LabelAdapter(labelName, null);

            this.actions.Add(
                generator =>
                {
                    var actualLabel = generator.DefineLabel();
                    lbl.Label = actualLabel;
                });

            label = lbl;
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter DefineLabel(ILabel label)
        {
            this.actions.Add(
                generator =>
                {
                    ((IAdaptedLabel)label).Label = generator.DefineLabel();
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, Type type)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, type);

                    if (type == null)
                    {
                        WriteLineError("!Null Type!");
                        return;
                    }

                    WriteLine("[{0}]", type.Name);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, string str)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, str);
                    WriteLine("\"{0}\"", str ?? "<NULL>");
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, float arg)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, arg);
                    WriteLine("    {0}{1}", opcode, arg);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, sbyte arg)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, arg);
                    WriteLine("{0}", arg);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, MethodInfo methodInfo)
        {
            methodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));

            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, methodInfo);

                    if (methodInfo == null)
                    {
                        WriteLineError("!Null MethodInfo!");
                        return;
                    }

                    WriteLine("[{0}] {1}.{2}()", methodInfo.ReturnType, methodInfo.DeclaringType, methodInfo.Name);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, FieldInfo field)
        {
            field = field ?? throw new ArgumentNullException(nameof(field));

            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, field);
                    WriteType(field.FieldType, field.Name);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, IFieldBuilder field)
        {
            return this.Emit(opcode, field.Define());
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, ILabel[] labels)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, labels?.Select(l => ((Label)((IAdaptedLabel)l).Label)).ToArray());

                    if (labels == null)
                    {
                        WriteLineError("!Null ILabel[]!");
                        return;
                    }

                    WriteLine("[{0}]", string.Join(", ", labels.Select(l => l.Name)));
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, SignatureHelper signature)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, signature);

                    if (signature == null)
                    {
                        WriteLineError("!Null SignatureHelper!");
                        return;
                    }

                    WriteLine("[{0}]", opcode, signature);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, ILocal local)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, ((IAdaptedLocal)local)?.Local as LocalBuilder);

                    WriteLine("[{0}] {1}", local.LocalType?.Name, local.Name);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, ConstructorInfo con)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, con);

                    if (con == null)
                    {
                        WriteLineError("!Null ConstructorInfo!");
                        return;
                    }

                    WriteLine("{0}({1})", con.Name, string.Join(", ", con.GetParameters().Select(p => $"{p.ParameterType} {p.Name}")));
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, long arg)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, arg);
                    WriteLine(arg.ToString());
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, int arg)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, arg);
                    WriteLine(arg.ToString());
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, short arg)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, arg);
                    WriteLine(arg.ToString());
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, double arg)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, arg);
                    WriteLine(arg.ToString());
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, byte arg)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, arg);
                    WriteLine(arg.ToString());
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode);
                    WriteLine(string.Empty);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Emit(OpCode opcode, ILabel label)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.Emit(opcode, (Label)((IAdaptedLabel)label).Label);

                    WriteLineColor(ConsoleColor.Cyan, "[{0}]", label.Name);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitCall(OpCode opcode, Func<MethodInfo> action)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);

                    var methodInfo = action();
                    generator.Emit(opcode, methodInfo);

                    if (methodInfo == null)
                    {
                        WriteLine("Null Pointer");
                    }
                    else
                    {
                        WriteLine("{0})", methodInfo.Name);
                    }
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.EmitCall(opcode, methodInfo, optionalParameterTypes);

                    if (methodInfo == null)
                    {
                        WriteLine("    {0}Null Pointer", opcode);
                        return;
                    }

                    WriteLine("{0}({1})", methodInfo.Name, string.Join(", ", optionalParameterTypes.Select(t => t.Name)));
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
        {
            this.actions.Add(
                generator =>
                {
                    OutputOpCode(generator, opcode);
                    generator.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes);
                    WriteLine("{0} {1}({2}) {3}", callingConvention, returnType.Name, string.Join(", ", parameterTypes.Select(t => t.Name)), string.Join(", ", optionalParameterTypes.Select(t => t.Name)));
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitWriteLine(FieldInfo fld)
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.EmitWriteLine(fld);
                    WriteLine("WriteLine(fld)");
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitWriteLine(string value)
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.EmitWriteLine(value);
                    WriteLine("WriteLine({0})", value);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EmitWriteLine(ILocal local)
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.EmitWriteLine((LocalBuilder)((IAdaptedLocal)local).Local);
                    WriteLine("WriteLine(local)");
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EndExceptionBlock()
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.EndExceptionBlock();
                    WriteLineColor(ConsoleColor.Green, "EndExceptionBlock");
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter EndScope()
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.EndScope();
                    WriteLineColor(ConsoleColor.Green, "EndScope");
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter MarkLabel(ILabel label)
        {
            this.actions.Add(
                generator =>
                {
                    generator.MarkLabel((Label)((IAdaptedLabel)label).Label);
                    WriteLineColor(ConsoleColor.Cyan, "[{0}]", label.Name);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter ThrowException(Type excType)
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.ThrowException(excType);
                    WriteLine("Throw Excpetion ({0})", excType.Name);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter UsingNamespace(string usingNamespace)
        {
            this.actions.Add(
                generator =>
                {
                    OutputILOffset(generator);
                    generator.UsingNamespace(usingNamespace);
                    WriteLine("using {0}", usingNamespace);
                });

            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Comment(string comment)
        {
            WriteLineColor(ConsoleColor.Green, "// {0}", comment);
            return this;
        }

        /// <inheritdoc/>
        public override IEmitter Defer(Action<IEmitter> action)
        {
            this.actions.Add(
                generator =>
                {
                    var emitter = new ILGeneratorEmitter(generator);
                    action(emitter);
                });

            return this;
        }

        /// <summary>
        /// Defines the constructor.
        /// </summary>
        /// <param name="generator">An IL generator.</param>
        public override void EmitIL(ILGenerator generator)
        {
            foreach (var action in this.actions)
            {
                action(generator);
            }
        }
    }
}