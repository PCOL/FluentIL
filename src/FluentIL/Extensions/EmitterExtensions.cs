namespace FluentIL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Various <see cref="IEmitter"/> extension methods.
    /// </summary>
    public static class EmitterExtensions
    {
        /// <summary>
        /// The <see cref="MethodInfo"/> for the <see cref="IDisposable.Dispose()"/> method.
        /// </summary>
        private static readonly MethodInfo DisposeMethodInfo = typeof(IDisposable).GetMethod("Dispose");

        /// <summary>
        /// The <see cref="MethodInfo"/> for the <see cref="Type.GetTypeFromHandle(RuntimeTypeHandle)"/> method.
        /// </summary>
        private static readonly MethodInfo TypeGetTypeFromHandle = typeof(Type).GetMethod("GetTypeFromHandle");

        /// <summary>
        /// The <see cref="MethodInfo"/> for the <see cref="TypeFactory.GetType(string, bool)"/> method.
        /// </summary>
        private static readonly MethodInfo TypeFactoryGetType = typeof(TypeFactory).GetMethod("GetType", new Type[] { typeof(string), typeof(bool) });

        /// <summary>
        /// The <see cref="MethodInfo"/> for the <see cref="MethodBase.GetMethodFromHandle(RuntimeMethodHandle)"/> method.
        /// </summary>
        private static readonly MethodInfo MethodBaseGetMethodFromHandle = typeof(MethodBase).GetMethod("GetMethodFromHandle", new[] { typeof(RuntimeMethodHandle) });

        /// <summary>
        /// The <see cref="MethodInfo"/> for the <see cref="MethodBase.GetMethodFromHandle(RuntimeMethodHandle, RuntimeTypeHandle)"/> method.
        /// </summary>
        private static readonly MethodInfo MethodBaseGetMethodFromHandleGeneric = typeof(MethodBase).GetMethod("GetMethodFromHandle", new[] { typeof(RuntimeMethodHandle), typeof(RuntimeTypeHandle) });

        /// <summary>
        /// A <see cref="MethodInfo"/> for the <see cref="object.GetType()"/> method.
        /// </summary>
        private static readonly MethodInfo ObjectGetType = typeof(object).GetMethod("GetType");

        /// <summary>
        /// A <see cref="MethodInfo"/> for the <see cref="Type.GetType(string, bool)"/> method.
        /// </summary>
        private static readonly MethodInfo TypeGetType = typeof(Type).GetMethod("GetType", new[] { typeof(string), typeof(bool) });

        /// <summary>
        /// A <see cref="MethodInfo"/> for the <see cref="Type.IsAssignableFrom(Type)"/> method.
        /// </summary>
        private static readonly MethodInfo TypeIsAssignableFrom = typeof(Type).GetMethod("IsAssignableFrom");

        /// <summary>
        /// Writes a local to standard output.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="local">The local to write out.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter WriteLineLoc(this IEmitter emitter, ILocal local)
        {
            return emitter.EmitWriteLine(local);
        }

        /// <summary>
        /// Emits a <see cref="OpCodes.Newobj"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="ctor">A constructor.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Newobj(this IEmitter emitter, ConstructorInfo ctor)
        {
            return emitter.Emit(OpCodes.Newobj, ctor);
        }

        /// <summary>
        /// Emits a <see cref="OpCodes.Nop"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Nop(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Nop);
        }

        /// <summary>
        /// Copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Dup(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Dup);
        }

        /// <summary>
        /// Computes the bitwise complement of the integer value on top of the stack and pushes the result onto the evaluation stack as the same type.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Not(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Not);
        }

        /// <summary>
        /// Computes the bitwise AND of two values and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter And(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.And);
        }

        /// <summary>
        /// Compute the bitwise complement of the two integer values on top of the stack and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Or(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Or);
        }

        /// <summary>
        /// Computes the bitwise XOR of the top two values on the evaluation stack, pushing the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Xor(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Xor);
        }

        /// <summary>
        /// Removes the value currently on top of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Pop(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Pop);
        }

        /// <summary>
        /// Emits a <see cref="OpCodes.Ret"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Ret(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Pushes the size, in bytes, of a supplied value type onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="valueType">The values type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter SizeOf(this IEmitter emitter, Type valueType)
        {
            if (valueType.IsValueType == false)
            {
                throw new InvalidProgramException("SizeOf instruction must take a value type");
            }

            return emitter.Emit(OpCodes.Sizeof);
        }

        /// <summary>
        /// Signals the Common Language Infrastructure (CLI) to inform the debugger that a break point has been tripped.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Break(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Break);
        }

        /// <summary>
        /// Attempts to cast an object passed by reference to the specified class.
        /// </summary>
        /// <typeparam name="T">The type to cast to.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter CastClass<T>(this IEmitter emitter)
        {
            return emitter.CastClass(typeof(T));
        }

        /// <summary>
        /// Attempts to cast an object passed by reference to the specified class.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="castToType">The <see cref="Type"/> to cast to.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter CastClass(this IEmitter emitter, Type castToType)
        {
            return emitter.Emit(OpCodes.Castclass, castToType);
        }

        /// <summary>
        /// Constrains the type on which a virtual method call is made.
        /// </summary>
        /// <typeparam name="T">The type to constrain to.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Constrained<T>(this IEmitter emitter)
        {
            return emitter.Constrained(typeof(T));
        }

        /// <summary>
        /// Constrains the type on which a virtual method call is made.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="constrainedType">The <see cref="Type"/> to cast to.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Constrained(this IEmitter emitter, Type constrainedType)
        {
            return emitter.Emit(OpCodes.Constrained, constrainedType);
        }

        /// <summary>
        /// Implements a jump table.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="labels">An arrsay of labels.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Switch(this IEmitter emitter, params ILabel[] labels)
        {
            return emitter.Emit(OpCodes.Switch, labels);
        }

        /// <summary>
        /// Performs a postfixed method call instruction such that the current method's stack frame is removed before the actual call instruction is executed.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter TailCall(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Tailcall);
        }

        /// <summary>
        /// Indicates that an address currently atop the evaluation stack might not be aligned to the natural size of the immediately following ldind, stind, ldfld,
        /// stfld, ldobj, stobj, initblk, or cpblk instruction.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="label">A label.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Unaligned(this IEmitter emitter, ILabel label)
        {
            return emitter.Emit(OpCodes.Unaligned, label);
        }

        /// <summary>
        /// Indicates that an address currently atop the evaluation stack might not be aligned to the natural size of the immediately following ldind, stind, ldfld,
        /// stfld, ldobj, stobj, initblk, or cpblk instruction.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="alignment">The byte alignment.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Unaligned(this IEmitter emitter, byte alignment)
        {
            return emitter.Emit(OpCodes.Unaligned, alignment);
        }

        /// <summary>
        /// Emits IL to allocate a block of memory on the stack and push the pointer onto the evaluation stack.
        /// The size of memory block to be allocated is determined by the value on the top of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LocalAlloc(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Localloc);
        }

        /// <summary>
        /// Converts a value type to an object reference (type O).
        /// </summary>
        /// <typeparam name="T">The type to box.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Box<T>(this IEmitter emitter)
            where T : struct
        {
            return emitter.Box(typeof(T));
        }

        /// <summary>
        /// Converts a value type to an object reference (type O).
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="valueType">A value type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Box(this IEmitter emitter, Type valueType)
        {
            if (valueType.IsValueType == false)
            {
                throw new InvalidProgramException("Box instruction must take a value type");
            }

            return emitter.Emit(OpCodes.Box, valueType);
        }

        /// <summary>
        /// Converts a value type to an object reference (type O).
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="refType">A reference type.</param>
        /// <param name="localValue">A local containing a value type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Box(this IEmitter emitter, Type refType, ILocal localValue)
        {
            return emitter
                .LdLoc(localValue)
                .Box(refType);
        }

        /// <summary>
        /// Converts the boxed representation of a value type to its unboxed form.
        /// </summary>
        /// <typeparam name="T">The type to unbox.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Unbox<T>(this IEmitter emitter)
            where T : struct
        {
            return emitter.Unbox(typeof(T));
        }

        /// <summary>
        /// Converts the boxed representation of a value type to its unboxed form.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="valueType">A reference type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Unbox(this IEmitter emitter, Type valueType)
        {
            if (valueType.IsValueType == false)
            {
                throw new InvalidProgramException("Unbox instruction must take a value type");
            }

            return emitter.Emit(OpCodes.Unbox, valueType);
        }

        /// <summary>
        /// Converts the boxed representation of a type specified in the instruction to its unboxed form.
        /// </summary>
        /// <typeparam name="T">The type to unbox.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter UnboxAny<T>(this IEmitter emitter)
            where T : struct
        {
            return emitter.UnboxAny(typeof(T));
        }

        /// <summary>
        /// Converts the boxed representation of a type specified in the instruction to its unboxed form.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="valueType">A value type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter UnboxAny(this IEmitter emitter, Type valueType)
        {
            if (valueType.IsValueType == false)
            {
                throw new InvalidProgramException("Unbox instruction must take a value type");
            }

            return emitter.Emit(OpCodes.Unbox_Any, valueType);
        }

        /// <summary>
        /// Pushes a typed reference to an instance of a specific type onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="refType">A reference type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter MkRefAny(this IEmitter emitter, Type refType)
        {
            return emitter.Emit(OpCodes.Mkrefany, refType);
        }

        /// <summary>
        /// Emits IL to allocate a block of memory on the stack and push the pointer onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="length">A value indicating the size of the memory block.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LocalAlloc(this IEmitter emitter, int length)
        {
            return emitter
                .LdcI4(length)
                .LocalAlloc();
        }

        /// <summary>
        /// Initializes a specified block of memory at a specific address to a given size and initial value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter InitBlk(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Initblk);
        }

        /// <summary>
        /// Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter InitObj<T>(this IEmitter emitter)
            where T : struct
        {
            return emitter.InitObj(typeof(T));
        }

        /// <summary>
        /// Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="valueType">A value type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter InitObj(this IEmitter emitter, Type valueType)
        {
            if (valueType.IsValueType == false)
            {
                throw new InvalidProgramException("InitObj instruction must take a value type");
            }

            return emitter.Emit(OpCodes.Initobj, valueType);
        }

        /// <summary>
        /// Tests whether an object reference (type O) is an instance of a particular class.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">A type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter IsInst(this IEmitter emitter, Type type)
        {
            return emitter.Emit(OpCodes.Isinst, type);
        }

        /// <summary>
        /// Returns an unmanaged pointer to the argmument list of the current method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ArgList(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Arglist);
        }

        /// <summary>
        /// Exits a protected region of code, unconditionally transferring control to a specific target instruction.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The label to jump to.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Leave(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Leave, target);
        }

        /// <summary>
        /// Exits a protected region of code, unconditionally transferring control to a target instruction (short form).
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The label to jump to.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LeaveS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Leave_S, target);
        }

        /// <summary>
        /// Emits IL to load the address of an objects virtual method onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="methodInfo">The method to load the address of.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdVirtFunc(this IEmitter emitter, MethodInfo methodInfo)
        {
            return emitter.Emit(OpCodes.Ldvirtftn, methodInfo);
        }

        /// <summary>
        /// Exits current method and jumps to specified method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="methodInfo">The method to jump to.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Jmp(this IEmitter emitter, MethodInfo methodInfo)
        {
            return emitter.Emit(OpCodes.Jmp, methodInfo);
        }

        /// <summary>
        /// Emits the IL to call a method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="method">The method to call.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Call(this IEmitter emitter, MethodInfo method)
        {
            return emitter.Emit(OpCodes.Call, method);
        }

        /// <summary>
        /// Emits to call a method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="method">The method to call.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Call(this IEmitter emitter, IMethodBuilder method)
        {
            return emitter.Call(method.Define());
        }

        /// <summary>
        /// Emits a <see cref="OpCodes.Call"/> to a method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="method">The method to call.</param>
        /// <param name="locals">Method parameters as local variables.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Call(this IEmitter emitter, IMethodBuilder method, params ILocal[] locals)
        {
            return emitter.Call(
                method.Define(),
                locals);
        }

        /// <summary>
        /// Emits a <see cref="OpCodes.Call"/> to a method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="method">The method to call.</param>
        /// <param name="locals">Method parameters as local variables.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Call(this IEmitter emitter, MethodInfo method, params ILocal[] locals)
        {
            foreach (var local in locals)
            {
                emitter.LdLoc(local);
            }

            return emitter.Call(method);
        }

        /// <summary>
        /// Emits the IL to call a constructor.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="ctor">The constructor to call.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Call(this IEmitter emitter, IConstructorBuilder ctor)
        {
            return emitter
                .Call(ctor.Define());
        }

        /// <summary>
        /// Emits the IL to call a constructor.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="ctor">The constructor to call.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Call(this IEmitter emitter, ConstructorInfo ctor)
        {
            return emitter.Emit(OpCodes.Call, ctor);
        }

        /// <summary>
        /// Emits the IL to call a constructor.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="ctor">The constructor to call.</param>
        /// <param name="locals">The constructors parameters as local variables.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Call(this IEmitter emitter, IConstructorBuilder ctor, params ILocal[] locals)
        {
            return emitter
                .Call(ctor.Define(), locals);
        }

        /// <summary>
        /// Emits the IL to call a constructor.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="ctor">The constructor to call.</param>
        /// <param name="locals">The constructors parameters as local variables.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Call(this IEmitter emitter, ConstructorInfo ctor, params ILocal[] locals)
        {
            foreach (var local in locals)
            {
                emitter.LdLoc(local);
            }

            return emitter.Call(ctor);
        }

        /// <summary>
        /// Emits a <see cref="OpCodes.Calli"/> to a method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="method">The method to call.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Calli(this IEmitter emitter, IMethodBuilder method)
        {
            return emitter.Calli(method);
        }

        /// <summary>
        /// Emits a <see cref="OpCodes.Calli"/> to a method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="method">The method to call.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Calli(this IEmitter emitter, MethodInfo method)
        {
            return emitter.Emit(OpCodes.Calli, method);
        }

        /// <summary>
        /// Emits a <see cref="OpCodes.Callvirt"/> to a method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="method">The method to call.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter CallVirt(this IEmitter emitter, IMethodBuilder method)
        {
            return emitter.CallVirt(method.Define());
        }

        /// <summary>
        /// Emits a <see cref="OpCodes.Callvirt"/> to a method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="method">The <see cref="MethodInfo"/> of the method to call.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter CallVirt(this IEmitter emitter, MethodInfo method)
        {
            return emitter.Emit(OpCodes.Callvirt, method);
        }

        /// <summary>
        /// Emits the IL to increment the contents of a local by one.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="local">The local to increment.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Inc(this IEmitter emitter, ILocal local)
        {
            return emitter
                .LdLoc(local)
                .LdcI4_1()
                .Add()
                .StLoc(local);
        }

        /// <summary>
        /// Emits the IL to decrement the contents of a local by one.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="local">The local to increment.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Dec(this IEmitter emitter, ILocal local)
        {
            return emitter
                .LdLoc(local)
                .LdcI4_1()
                .Sub()
                .StLoc(local);
        }

        /// <summary>
        /// Perform a 'typeof()' style operation.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> to emit the 'typeof()' for.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter EmitTypeOf<T>(this IEmitter emitter)
        {
            return emitter.EmitTypeOf(typeof(T));
        }

        /// <summary>
        /// Perform a 'typeof()' style operation.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The <see cref="Type"/> to emit the 'typeof()' for.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter EmitTypeOf(this IEmitter emitter, Type type)
        {
            return emitter
                .LdToken(type)
                .Call(TypeGetTypeFromHandle);
        }

        /// <summary>
        /// Emits the IL to perform an 'IsAssignableFrom' operation.
        /// </summary>
        /// <typeparam name="T">The type..</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="local">A <see cref="LocalBuilder"/> to check.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter EmitIsAssignableFrom<T>(this IEmitter emitter, ILocal local)
        {
            return emitter.EmitIsAssignableFrom(typeof(T), local);
        }

        /// <summary>
        /// Emits the IL to perform an 'IsAssignableFrom' operation.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="from">The <see cref="Type"/> to check is assignable from.</param>
        /// <param name="local">A <see cref="LocalBuilder"/> to check.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter EmitIsAssignableFrom(this IEmitter emitter, Type from, ILocal local)
        {
            return emitter
                .EmitTypeOf(from)
                .LdLocA(local)
                .Constrained(local.LocalType)
                .CallVirt(ObjectGetType)
                .CallVirt(TypeIsAssignableFrom);
        }

        /// <summary>
        /// Emits the IL to perform an 'IsAssignableFrom' operation.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="from">The <see cref="Type"/> to check is assignable from.</param>
        /// <param name="to">A <see cref="Type"/> to check.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter EmitIsAssignableFrom(this IEmitter emitter, Type from, Type to)
        {
            return emitter
                .EmitTypeOf(from)
                .EmitTypeOf(to)
                .CallVirt(TypeIsAssignableFrom);
        }

        /// <summary>
        /// Emit IL to get method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="methodInfo">A <see cref="MethodInfo"/>.</param>
        /// <returns>The <see cref="ILGenerator"/> instance.</returns>
        public static IEmitter EmitMethod(this IEmitter emitter, MethodInfo methodInfo)
        {
            return emitter
                .LdToken(methodInfo)
                .Call(MethodBaseGetMethodFromHandle);
        }

        /// <summary>
        /// Emit IL to get method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="methodInfo">A <see cref="MethodInfo"/>.</param>
        /// <param name="declaringType">The methods decalring type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter EmitMethod(this IEmitter emitter, MethodInfo methodInfo, Type declaringType)
        {
            return emitter
                .LdToken(methodInfo)
                .LdToken(declaringType)
                .Call(MethodBaseGetMethodFromHandleGeneric);
        }

        /// <summary>
        /// Emits IL for 'using' pattern.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="disposableObj">The disposable object.</param>
        /// <param name="generateBlock">The code block inside the using block.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Using(this IEmitter emitter, ILocal disposableObj, Action generateBlock)
        {
            // Try
            emitter.BeginExceptionBlock(out ILabel beginBlock);

            generateBlock();

            // Finally
            return emitter
                .BeginFinallyBlock()
                .DefineLabel(out ILabel endFinally)

                .LdLoc(disposableObj)
                .BrFalseS(endFinally)
                .LdLoc(disposableObj)
                .CallVirt(DisposeMethodInfo)
                .Nop()
                .MarkLabel(endFinally)
                .EndExceptionBlock();
        }

        /// <summary>
        /// Emits IL to call the static Format method on the <see cref="string"/> object.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="locals">An array of <see cref="LocalBuilder"/> to use.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StringFormat(this IEmitter emitter, string format, params ILocal[] locals)
        {
            return emitter
                .DeclareLocal(typeof(object), out ILocal localArray)
                .Array(
                    localArray,
                    locals.Length,
                    (index) =>
                    {
                        emitter.LdLoc(locals[index]);
                        if (locals[index].LocalType.IsValueType == true)
                        {
                            emitter.Emit(OpCodes.Box, locals[index].LocalType);
                        }
                    })
                .LdStr(format)
                .LdLoc(localArray)
                .Call(typeof(string)
                    .GetMethod("Format", new Type[] { typeof(string), typeof(object[]) }));
        }

        /// <summary>
        /// Emits IL to perform a for loop over an array without element loading.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="localLength">The local variable holding the length.</param>
        /// <param name="action">An action to allow the injecting of the loop code.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter For(
            this IEmitter emitter,
            ILocal localLength,
            Action<ILocal> action)
        {
            return emitter
                .For(
                    localLength,
                    (IEmitter il, ILocal index) => action(index));
        }

        /// <summary>
        /// Emits IL to perform a for loop over an array without element loading.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="localLength">The local variable holding the length.</param>
        /// <param name="action">An action to allow the injecting of the loop code.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter For(
            this IEmitter emitter,
            ILocal localLength,
            Action<IEmitter, ILocal> action)
        {
            emitter
                .DefineLabel(out ILabel beginLoop)
                .DefineLabel(out ILabel loopCheck)
                .DeclareLocal<int>("index", out ILocal index)

                .LdcI4_0()
                .StLoc(index)
                .Br(loopCheck)
                .MarkLabel(beginLoop);

            action(emitter, index);

            return emitter
                .Nop()
                .LdLoc(index)
                .LdcI4_1()
                .Add()
                .StLoc(index)

                .MarkLabel(loopCheck)
                .LdLoc(index)
                .LdLoc(localLength)
                .Blt(beginLoop);
        }

        /// <summary>
        /// Emits IL to perform a for loop over an array with element loading.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="localArray">The local variable holding the array.</param>
        /// <param name="action">An action to allow the injecting of the loop code.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter For(
            this IEmitter emitter,
            ILocal localArray,
            Action<ILocal, ILocal> action)
        {
            return emitter
                .For(
                    localArray,
                    (il, index, item) => action(index, item));
        }

        /// <summary>
        /// Emits IL to perform a for loop over an array with element loading.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="localArray">The local variable holding the array.</param>
        /// <param name="action">An action to allow the injecting of the loop code.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter For(
            this IEmitter emitter,
            ILocal localArray,
            Action<IEmitter, ILocal, ILocal> action)
        {
            return emitter
                .DeclareLocal(localArray.LocalType.GetElementType(), "item", out ILocal itemLocal)
                .DeclareLocal<int>("length", out ILocal lengthLocal)

                .LdLoc(localArray)
                .LdLen()
                .ConvI4()
                .StLocS(lengthLocal)

                .For(
                    lengthLocal,
                    (index) =>
                    {
                        emitter
                            .LdLoc(localArray)
                            .LdLoc(index)
                            .LdElemRef()
                            .StLoc(itemLocal)
                            .Nop();

                        action(emitter, index, itemLocal);
                    });
        }

        /// <summary>
        /// Emits IL to perform a for loop over an array.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localEnumerable">The local variable holding the enumerable object.</param>
        /// <param name="action">An action to allow the injecting of the loop code.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ForEach(
            this IEmitter emitter,
            ILocal localEnumerable,
            Action<ILocal> action)
        {
            return emitter
                .ForEach(
                    localEnumerable,
                    (item, breakAction) => action(item));
        }

        /// <summary>
        /// Emits IL to perform a for loop over an array.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localEnumerable">The local variable holding the enumerable object.</param>
        /// <param name="action">An action to allow the injecting of the loop code.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ForEach(
            this IEmitter emitter,
            ILocal localEnumerable,
            Action<IEmitter, ILocal> action)
        {
            return emitter
                .ForEach(
                    localEnumerable,
                    (il, item, breakAction) => action(il, item));
        }

        /// <summary>
        /// Emits IL to perform a for loop over an enumerable object.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localEnumerable">The local variable holding the enumerable object.</param>
        /// <param name="action">An action to allow the injecting of the loop code.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ForEach(
            this IEmitter emitter,
            ILocal localEnumerable,
            Action<ILocal, Action> action)
        {
            return emitter
                .ForEach(
                    localEnumerable,
                    (il, item, breakLoop) => action(item, breakLoop));
        }

        /// <summary>
        /// Emits IL to perform a for loop over an enumerable object.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localEnumerable">The local variable holding the enumerable instance`.</param>
        /// <param name="action">An action to allow the injecting of the loop code.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ForEach(
            this IEmitter emitter,
            ILocal localEnumerable,
            Action<IEmitter, ILocal, Action> action)
        {
            emitter
                .DefineLabel("loopEnd", out ILabel loopEnd);

            var localType = localEnumerable.LocalType;
            if (localType.IsArray == true)
            {
                emitter.For(
                    localEnumerable,
                    (item) =>
                    {
                        action(emitter, item, () => emitter.Br(loopEnd));
                    });
            }
            else if (localType.IsGenericType == false ||
                typeof(IEnumerable<>).MakeGenericType(localType.GetGenericArguments()).IsAssignableFrom(localEnumerable.LocalType) == false)
            {
                throw new InvalidOperationException("Not a enumerable type");
            }
            else
            {
                var enumerableType = localType.GetGenericArguments()[0];
                var enumeratorType = typeof(IEnumerator<>).MakeGenericType(enumerableType);

                var getEnumerator = typeof(IEnumerable<>).MakeGenericType(enumerableType).GetMethod("GetEnumerator");
                var getCurrent = enumeratorType.GetProperty("Current").GetGetMethod();
                var moveNext = typeof(IEnumerator).GetMethod("MoveNext");

                emitter
                    .DefineLabel("loopStart", out ILabel loopStart)
                    .DefineLabel("loopCheck", out ILabel loopCheck)
                    .DefineLabel("endFinally", out ILabel endFinally)

                    .DeclareLocal(enumeratorType, "localEnumerator", out ILocal localEnumerator)
                    .DeclareLocal(enumerableType, "localItem", out ILocal localItem)

                    .LdLocS(localEnumerable)
                    .CallVirt(getEnumerator)
                    .StLocS(localEnumerator)

                    .Try(out ILabel beginEx)

                    .Br(loopCheck)
                    .MarkLabel(loopStart)
                    .LdLoc(localEnumerator)
                    .CallVirt(getCurrent)
                    .StLocS(localItem)
                    .Nop();

                action(emitter, localItem, () => emitter.Leave(loopEnd));

                emitter
                    .Nop()
                    .MarkLabel(loopCheck)
                    .LdLoc(localEnumerator)
                    .CallVirt(moveNext)
                    .BrTrue(loopStart)

                    .Leave(loopEnd)

                    .Finally()

                    .LdLoc(localEnumerator)
                    .BrFalse(endFinally)

                    .LdLoc(localEnumerator)
                    .CallVirt(DisposeMethodInfo)

                    .Nop()
                    .MarkLabel(endFinally)

                    .EndExceptionBlock();
            }

            emitter
                .Nop()
                .MarkLabel(loopEnd);

            return emitter;
        }

        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <typeparam name="T">The locals type.</typeparam>
        /// <param name="emitter">An emitter instance.</param>
        /// <param name="local">A variable to receive a local.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter DeclareLocal<T>(this IEmitter emitter, out ILocal local)
        {
            emitter.DeclareLocal(typeof(T), out local);
            return emitter;
        }

        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <typeparam name="T">The locals type.</typeparam>
        /// <param name="emitter">An emitter instance.</param>
        /// <param name="localName">The name of the local.</param>
        /// <param name="local">A variable to receive a local.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter DeclareLocal<T>(this IEmitter emitter, string localName, out ILocal local)
        {
            emitter.DeclareLocal(typeof(T), localName, out local);
            return emitter;
        }

        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <typeparam name="T">The locals type.</typeparam>
        /// <param name="emitter">An emitter instance.</param>
        /// <param name="localName">The name of the local.</param>
        /// <param name="pinned">The value indicating whether or not the local is pinned.</param>
        /// <param name="local">A variable to receive a local.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter DeclareLocal<T>(this IEmitter emitter, string localName, bool pinned, out ILocal local)
        {
            emitter.DeclareLocal(typeof(T), localName, pinned, out local);
            return emitter;
        }

        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <typeparam name="T">The locals type.</typeparam>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <param name="pinned">The value indicating whether or not the local is pinned.</param>
        /// <param name="local">A variable to receive a local.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter DeclareLocal<T>(this IEmitter emitter, bool pinned, out ILocal local)
        {
            emitter.DeclareLocal(typeof(T), pinned, out local);
            return emitter;
        }

        /// <summary>
        /// Starts a try block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Try(this IEmitter emitter)
        {
            return emitter.BeginExceptionBlock(out ILabel label);
        }

        /// <summary>
        /// Starts a try block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <param name="label">The label for the end of the block.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Try(this IEmitter emitter, out ILabel label)
        {
            return emitter.BeginExceptionBlock(out label);
        }

        /// <summary>
        /// Starts a catch block.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Catch<TException>(this IEmitter emitter)
            where TException : Exception
        {
            return emitter.Catch(typeof(TException));
        }

        /// <summary>
        /// Starts a catch block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <param name="exceptionType">The label for the end of the block.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Catch(this IEmitter emitter, Type exceptionType)
        {
            return emitter.BeginCatchBlock(exceptionType);
        }

        /// <summary>
        /// Starts a catch block and stores the exception in the local.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <param name="exceptionType">The label for the end of the block.</param>
        /// <param name="local">A <see cref="ILocal"/> to store the exception in.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Catch(this IEmitter emitter, Type exceptionType, ILocal local)
        {
            if (typeof(Exception).IsAssignableFrom(local.LocalType) == false)
            {
                throw new InvalidOperationException("Local must be an exception type");
            }

            return emitter
                .Catch(exceptionType)
                .StLoc(local);
        }

        /// <summary>
        /// Starts a finally block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Finally(this IEmitter emitter)
        {
            return emitter.BeginFinallyBlock();
        }

        /// <summary>
        /// Starts a fault block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Fault(this IEmitter emitter)
        {
            return emitter.BeginFaultBlock();
        }

        /// <summary>
        /// Starts a filter block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Filter(this IEmitter emitter)
        {
            return emitter.BeginExceptFilterBlock();
        }

        /// <summary>
        /// Throws an exception.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Throw(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Throw);
        }

        /// <summary>
        /// Throws an exception.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <param name="message">The exception message.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ThrowException<TException>(
            this IEmitter emitter,
            string message)
            where TException : Exception
        {
            ConstructorInfo ctor =
                typeof(TException)
                .GetConstructor(new[] { typeof(string) });

            if (ctor == null)
            {
                throw new ArgumentException("Type TException does not have a public constructor that takes a string argument");
            }

            emitter
                .LdStr(message)
                .Newobj(ctor)
                .Throw();

            return emitter;
        }

        /// <summary>
        /// Emits IL to load the type for a given type name onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="typeNameLocal">The <see cref="LocalBuilder"/> containing the type name.</param>
        /// <param name="dynamicOnly">A value indicating whether or not to only check for dynamically generated types.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter GetType(
            this IEmitter emitter,
            ILocal typeNameLocal,
            bool dynamicOnly = false)
        {
            return emitter
                .LdLocS(typeNameLocal)
                .Emit(dynamicOnly == false ? OpCodes.Ldc_I4_0 : OpCodes.Ldc_I4_1)
                .Call(TypeFactoryGetType);
        }

        /// <summary>
        /// Emits IL to load the type for a given type name onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="typeName">The type name.</param>
        /// <param name="dynamicOnly">A value indicating whether or not to only check for dynamically generated types.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter GetType(
            this IEmitter emitter,
            string typeName,
            bool dynamicOnly = false)
        {
            return emitter
                .LdStr(typeName)
                .Emit(dynamicOnly == false ? OpCodes.Ldc_I4_0 : OpCodes.Ldc_I4_1)
                .Call(TypeFactoryGetType);
        }
    }
}