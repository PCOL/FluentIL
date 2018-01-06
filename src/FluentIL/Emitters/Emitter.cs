namespace FluentIL.Emitters
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    public class Emitter
        : IEmitter
    {
        private List<Action<IEmitter>> actions = new List<Action<IEmitter>>();

        private int localIndex;

        public Emitter()
        {
        }
        
        public int ILOffset
        {
            get
            {
                return 0;
            }
        }

        public IEmitter BeginCatchBlock(Type exceptionType)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.BeginCatchBlock(exceptionType);
                });

            return this;
        }

        public IEmitter BeginExceptFilterBlock()
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.BeginExceptFilterBlock();
                });

            return this;
        }

        public IEmitter BeginExceptionBlock(out ILabel label)
        {
            var lab = new LabelAdapter(Guid.NewGuid().ToString());
            this.actions.Add(
                emitter =>
                {
                    emitter.BeginExceptionBlock(lab);
                });

            label = lab;
            return this;
        }

        public IEmitter BeginExceptionBlock(ILabel label)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.BeginExceptionBlock(label);
                });

            return this;
        }

        public IEmitter BeginFaultBlock()
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.BeginFaultBlock();
                });

            return this;
        }

        public IEmitter BeginFinallyBlock()
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.BeginFinallyBlock();
                });

            return this;
        }

        public IEmitter BeginScope()
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.BeginScope();
                });

            return this;
        }

        public IEmitter DeclareLocal(Type localType, out ILocal local)
        {
            this.DeclareLocal(localType, Guid.NewGuid().ToString(), out local);
            return this;
        }

        public IEmitter DeclareLocal(Type localType, string localName, out ILocal local)
        {
            this.DeclareLocal(localType, localName, false, out local);
            return this;
        }

        public IEmitter DeclareLocal(Type localType, bool pinned, out ILocal local)
        {
            this.DeclareLocal(localType, Guid.NewGuid().ToString(), pinned, out local);
            return this;
        }

        public IEmitter DeclareLocal(Type localType, string localName, bool pinned, out ILocal local)
        {
            var loc = new LocalAdapter(localName, localType, localIndex++, pinned);
            this.actions.Add(
                emitter =>
                {
                    emitter.DeclareLocal(loc);
                });

            local = loc;
            return this;
        }

        public IEmitter DeclareLocal(ILocal local)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.DeclareLocal(local);
                });

            return this;
        }

        public IEmitter DefineLabel(out ILabel label)
        {
            var lab = new LabelAdapter(Guid.NewGuid().ToString());
            this.actions.Add(
                emitter =>
                {
                    emitter.DefineLabel(lab);
                });

            label = lab;
            return this;
        }

        /// <summary>
        /// Defines a label.
        /// </summary>
        /// <param name="labelName"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public IEmitter DefineLabel(string labelName, out ILabel label)
        {
            var lab = new LabelAdapter(labelName);
            this.actions.Add(
                emitter =>
                {
                    emitter.DefineLabel(lab);
                });

            label = lab;
            return this;
        }

        public IEmitter DefineLabel(ILabel label)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.DefineLabel(label);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, Type type)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, type);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, string str)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, str);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, float arg)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, arg);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, sbyte arg)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, arg);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, MethodInfo meth)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, meth);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, FieldInfo field)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, field);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, IFieldBuilder field)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, field);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, ILabel[] labels)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, labels);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, SignatureHelper signature)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, signature);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, ILocal local)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, local);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, ConstructorInfo con)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, con);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, long arg)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, arg);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, int arg)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, arg);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, short arg)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, arg);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, double arg)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, arg);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, byte arg)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, arg);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode);
                });

            return this;
        }

        public IEmitter Emit(OpCode opcode, ILabel label)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.Emit(opcode, label);
                });

            return this;
        }

        public IEmitter EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.EmitCall(opcode, methodInfo, optionalParameterTypes);
                });

            return this;
        }

        public IEmitter EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes);
                });

            return this;
        }

        public IEmitter EmitWriteLine(FieldInfo fld)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.EmitWriteLine(fld);
                });

            return this;
        }

        public IEmitter EmitWriteLine(string value)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.EmitWriteLine(value);
                });

            return this;
        }

        public IEmitter EmitWriteLine(ILocal local)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.EmitWriteLine(local);
                });

            return this;
        }

        public IEmitter EndExceptionBlock()
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.EndExceptionBlock();
                });

            return this;
        }

        public IEmitter EndScope()
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.EndScope();
                });

            return this;
        }

        public IEmitter MarkLabel(ILabel loc)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.MarkLabel(loc);
                });

            return this;
        }

        public IEmitter ThrowException(Type excType)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.ThrowException(excType);
                });

            return this;
        }

        public IEmitter UsingNamespace(string usingNamespace)
        {
            this.actions.Add(
                emitter =>
                {
                    emitter.UsingNamespace(usingNamespace);
                });

            return this;
        }

        internal void EmitMethod(IEmitter emitter)
        {
            foreach (var action in this.actions)
            {
                action(emitter);
            }
        }
    }
}