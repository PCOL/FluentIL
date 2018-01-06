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
        private static readonly OpCode[] ConvOpCodes = new OpCode[]
        {
            OpCodes.Nop,
            OpCodes.Nop,
            OpCodes.Nop,
            OpCodes.Conv_I1,
            OpCodes.Conv_I2,
            OpCodes.Conv_I1,
            OpCodes.Conv_U1,
            OpCodes.Conv_I2,
            OpCodes.Conv_U2,
            OpCodes.Conv_I4,
            OpCodes.Conv_U4,
            OpCodes.Conv_I8,
            OpCodes.Conv_U8,
            OpCodes.Conv_R4,
            OpCodes.Conv_R8,
            OpCodes.Nop,
            OpCodes.Nop,
            OpCodes.Nop,
            OpCodes.Nop
        };

        private static readonly OpCode[] LdindOpCodes = new OpCode[]
        {
            OpCodes.Ldobj,
            OpCodes.Ldobj,
            OpCodes.Ldobj,
            OpCodes.Ldind_I1,
            OpCodes.Ldind_I2,
            OpCodes.Ldind_I1,
            OpCodes.Ldind_U1,
            OpCodes.Ldind_I2,
            OpCodes.Ldind_U2,
            OpCodes.Ldind_I4,
            OpCodes.Ldind_U4,
            OpCodes.Ldind_I8,
            OpCodes.Ldind_I8,
            OpCodes.Ldind_R4,
            OpCodes.Ldind_R8,
            OpCodes.Ldobj,
            OpCodes.Ldobj,
            OpCodes.Ldobj,
            OpCodes.Ldobj
        };

        private static readonly OpCode[] StindOpCodes = new OpCode[]
        {
            OpCodes.Stobj,
            OpCodes.Stobj,
            OpCodes.Stobj,
            OpCodes.Stind_I1,
            OpCodes.Stind_I2,
            OpCodes.Stind_I1,
            OpCodes.Stind_I1,
            OpCodes.Stind_I2,
            OpCodes.Stind_I2,
            OpCodes.Stind_I4,
            OpCodes.Stind_I4,
            OpCodes.Stind_I8,
            OpCodes.Stind_I8,
            OpCodes.Stind_R4,
            OpCodes.Stind_R8,
            OpCodes.Stobj,
            OpCodes.Stobj,
            OpCodes.Stobj,
            OpCodes.Stobj
        };

        private static readonly OpCode[] LdcI4OpCodes = new OpCode[]
        {
            OpCodes.Ldc_I4_0,
            OpCodes.Ldc_I4_1,
            OpCodes.Ldc_I4_2,
            OpCodes.Ldc_I4_3,
            OpCodes.Ldc_I4_4,
            OpCodes.Ldc_I4_5,
            OpCodes.Ldc_I4_6,
            OpCodes.Ldc_I4_7,
            OpCodes.Ldc_I4_8
        };

        /// <summary>
        /// <see cref="IDisposable"/> Dispose <see cref="MethodInfo"/>
        /// </summary>
        private static readonly MethodInfo DisposeMethodInfo = typeof(IDisposable).GetTypeInfo().GetMethod("Dispose");

        /// <summary>
        /// <see cref="Type"/> GetTypeFromHandle <see cref="MethodInfo"/>
        /// </summary>
        private static readonly MethodInfo GetTypeFromHandleMethodInfo = typeof(Type).GetTypeInfo().GetMethod("GetTypeFromHandle");

        /// <summary>
        /// The <see cref="MethodInfo"/> for the <see cref="TypeFactory"/> GetType method.
        /// </summary>
        private static readonly MethodInfo GetTypeMethodInfo = typeof(TypeFactory).GetMethod("GetType", new Type[] { typeof(string), typeof(bool) });

        /// <summary>
        /// Writes a local to standard output.
        /// </summary>
        /// <param name="emitter">An emitter.</param>
        /// <param name="local">The local to write out.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter WriteLineLoc(this IEmitter emitter, ILocal local)
        {
            return emitter.EmitWriteLine(local);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter"></param>
        /// <param name="ctor"></param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter Newobj(this IEmitter emitter, ConstructorInfo ctor)
        {
            return emitter.Emit(OpCodes.Newobj, ctor);
        }

        /// <summary>
        /// 
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
        /// 
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
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter SizeOf(this IEmitter emitter, Type valueType)
        {
            if (valueType.GetTypeInfo().IsValueType == false)
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
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="constrainedType">The <see cref="Type"/> to cast to.</param>
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
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="referenceType">A reference type</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Box<T>(this IEmitter emitter)
            where T : class
        {
            return emitter.Box(typeof(T));
        }

        /// <summary>
        /// Converts a value type to an object reference (type O).
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="referenceType">A reference type</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Box(this IEmitter emitter, Type referenceType)
        {
            if (referenceType.GetTypeInfo().IsClass == false)
            {
                throw new InvalidProgramException("Box instruction must take a reference type");
            }

            return emitter.Emit(OpCodes.Box, referenceType);
        }

        /// <summary>
        /// Converts a value type to an object reference (type O).
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="referenceType">A reference type</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Box(this IEmitter emitter, Type referenceType, ILocal localValue)
        {
            return emitter
                .LdLoc(localValue)
                .Box(referenceType);
        }

        /// <summary>
        /// Converts the boxed representation of a value type to its unboxed form.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="valueType">A reference type</param>
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
        /// <param name="valueType">A reference type</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Unbox(this IEmitter emitter, Type valueType)
        {
            if (valueType.GetTypeInfo().IsValueType == false)
            {
                throw new InvalidProgramException("Unbox instruction must take a value type");
            }

            return emitter.Emit(OpCodes.Unbox, valueType);
        }

        /// <summary>
        /// Converts the boxed representation of a type specified in the instruction to its unboxed form.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="valueType">A reference type</param>
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
        /// <param name="type">A reference type</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter UnboxAny(this IEmitter emitter, Type valueType)
        {
            if (valueType.GetTypeInfo().IsValueType == false)
            {
                throw new InvalidProgramException("Unbox instruction must take a value type");
            }

            return emitter.Emit(OpCodes.Unbox_Any, valueType);
        }

        /// <summary>
        /// Pushes a typed reference to an instance of a specific type onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">A reference type</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter MkRefAny(this IEmitter emitter, Type type)
        {
            return emitter.Emit(OpCodes.Mkrefany, type);
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
            if (valueType.GetTypeInfo().IsValueType == false)
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
        /// Tests whether an object reference (type O) is an instance of a particular class.
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
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Leave(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Leave, target);
        }

        /// <summary>
        /// Exits a protected region of code, unconditionally transferring control to a target instruction (short form).
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LeaveS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Leave_S, target);
        }

        /// <summary>
        /// Emits IL to load the address of an objects virtual method onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="methodInfo"></param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdVirtFunc(this IEmitter emitter, MethodInfo methodInfo)
        {
            return emitter.Emit(OpCodes.Ldvirtftn, methodInfo);
        }

        /// <summary>
        /// Exits current method and jumps to specified method.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="methodInfo"></param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Jmp(this IEmitter emitter, MethodInfo methodInfo)
        {
            return emitter.Emit(OpCodes.Jmp, methodInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="method"></param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Call(this IEmitter emitter, MethodInfo method)
        {
            return emitter.Emit(OpCodes.Call, method);
        }

        /// <summary>
        /// 
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
        /// Calls a constructor.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="ctor">The constructor to call.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Call(this IEmitter emitter, ConstructorInfo ctor)
        {
            return emitter.Emit(OpCodes.Call, ctor);
        }

        /// <summary>
        /// Calls a constructor.
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
        /// 
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="method"></param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Calli(this IEmitter emitter, MethodInfo method)
        {
            return emitter.Emit(OpCodes.Calli, method);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="method"></param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter CallVirt(this IEmitter emitter, MethodInfo method)
        {
            return emitter.Emit(OpCodes.Callvirt, method);
        }

        /// <summary>
        /// Perform a 'typeof()' style operation.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> to emit the 'typeof()' for.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter TypeOf<T>(this IEmitter emitter)
        {
            return emitter.TypeOf(typeof(T));
        }

        /// <summary>
        /// Perform a 'typeof()' style operation.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The <see cref="Type"/> to emit the 'typeof()' for.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter TypeOf(this IEmitter emitter, Type type)
        {
            return emitter
                .LdToken(type)
                .Call(GetTypeFromHandleMethodInfo);
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
            ILabel beginBlock;
            ILabel endFinally;

            // Try
            emitter.BeginExceptionBlock(out beginBlock);

            generateBlock();

            // Finally
            return emitter
                .BeginFinallyBlock()
                .DefineLabel(out endFinally)
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
            MethodInfo stringFormatMethod = typeof(string).GetTypeInfo().GetMethod("Format", new Type[] { typeof(string), typeof(object[]) });
            ILocal localArray;

            return emitter
                .DeclareLocal(typeof(object), out localArray)
                .Array(
                    localArray,
                    locals.Length,
                    (index) =>
                    {
                        emitter.LdLoc(locals[index]);
                        if (locals[index].LocalType.GetTypeInfo().IsValueType == true)
                        {
                            emitter.Emit(OpCodes.Box, locals[index].LocalType);
                        }
                    })
                .LdStr(format)
                .LdLoc(localArray)
                .Call(stringFormatMethod);
        }

        /// <summary>
        /// Emits IL to convert one type to another.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="sourceType">The source type.</param>
        /// <param name="targetType">The destination type.</param>
        /// <param name="isAddress">A value indicating whether or not the convert is for an address.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter EmitConv(this IEmitter emitter, Type sourceType, Type targetType, bool isAddress)
        {
            if (sourceType != targetType)
            {
                var sourceTypeInfo = sourceType.GetTypeInfo();
                var targetTypeInfo = targetType.GetTypeInfo();

                if (sourceType.IsByRef == true)
                {
                    Type elementType = sourceType.GetElementType();
                    emitter.LdInd(elementType);
                    emitter.EmitConv(elementType, targetType, isAddress);
                }
                else if (targetTypeInfo.IsValueType == true)
                {
                    if (sourceTypeInfo.IsValueType == true)
                    {
                        OpCode opCode = ConvOpCodes[(int)Type.GetTypeCode(targetType)];
                    }
                    else
                    {
                        emitter.Emit(OpCodes.Unbox, targetType);
                        if (isAddress == false)
                        {
                            emitter.LdInd(targetType);
                        }
                    }
                }
                else if (targetTypeInfo.IsAssignableFrom(sourceType) == true)
                {
                    if (sourceTypeInfo.IsValueType == true)
                    {
                        if (isAddress == true)
                        {
                            emitter.LdInd(sourceType);
                        }

                        emitter.Emit(OpCodes.Box, sourceType);
                    }
                }
                else if (targetType.IsGenericParameter == true)
                {
                    emitter.Emit(OpCodes.Unbox_Any, targetType);
                }
                else
                {
                    emitter.Emit(OpCodes.Castclass, targetType);
                }
            }

            return emitter;
        }

        /// <summary>
        /// Emits the IL to load a value onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The type to load.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdInd(this IEmitter emitter, Type type)
        {
            OpCode opCode = LdindOpCodes[(int)Type.GetTypeCode(type)];
            if (opCode.Equals(OpCodes.Ldobj) == true)
            {
                emitter.Emit(opCode, type);
            }
            else
            {
                emitter.Emit(opCode);
            }

            return emitter;
        }

        /// <summary>
        /// Emits the IL to store the value from the top of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The type to store.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StInd(this IEmitter emitter, Type type)
        {
            OpCode opCode = StindOpCodes[(int)Type.GetTypeCode(type)];
            if (opCode.Equals(OpCodes.Stobj) == true)
            {
                emitter.Emit(opCode, type);
            }
            else
            {
                emitter.Emit(opCode);
            }

            return emitter;
        }

        /// <summary>
        /// Emits IL to perform a for loop over an array.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="local">The local variable holding the array.</param>
        /// <param name="action">An action to allow the injecting of the loop code.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter For(this IEmitter emitter, ILocal local, Action<ILocal> action)
        {
            ILabel beginLoop;
            ILabel loopCheck;
            ILocal index;
            ILocal item;

            emitter
                .DefineLabel("beginLoop", out beginLoop)
                .DefineLabel("loopCheck", out loopCheck)

                .DeclareLocal<int>("index", out index)
                .DeclareLocal(local.LocalType.GetElementType(), "item", out item)

                .LdcI4_0()
                .StLoc(index)
                .Br(loopCheck)
                .MarkLabel(beginLoop)

                .LdLoc(local)
                .LdLocS(index)
                .LdElemRef()
                .StLocS(item)
                .Nop();

            action(item);

            return emitter
                .Nop()
                .LdLocS(index)
                .LdcI4_1()
                .Emit(OpCodes.Add)
                .StLocS(index)
                .MarkLabel(loopCheck)

                .LdLocS(index)
                .LdLoc(local)
                .LdLen()
                .ConvI4()
                .BltS(beginLoop);
        }

        /// <summary>
        /// Emits IL to perform a for loop over an array.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="local">The local variable holding the array.</param>
        /// <param name="action">An action to allow the injecting of the loop code.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter For(this IEmitter emitter, int startIndex, int length, Action<ILocal> action)
        {
            ILabel beginLoop;
            ILabel loopCheck;
            ILocal localIndex;
            ILocal localLength;

            emitter
                .DefineLabel("beginLoop", out beginLoop)
                .DefineLabel("loopCheck", out loopCheck)

                .DeclareLocal<int>("index", out localIndex)
                .DeclareLocal<int>("length", out localLength)

                .LdcI4(startIndex)
                .StLoc(localIndex)
                .LdcI4(length)
                .StLoc(localLength)
                .Br(loopCheck)
                .MarkLabel(beginLoop)
                .Nop();

            action(localIndex);

            return emitter
                .Nop()
                .LdLoc(localIndex)
                .LdcI4_1()
                .Emit(OpCodes.Add)
                .StLoc(localIndex)

                .MarkLabel(loopCheck)
                .LdLocS(localIndex)
                .LdLocS(localLength)
                .BltS(beginLoop);
        }

        /// <summary>
        /// Emits IL to perform a for loop over an array.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="local">The local variable holding the array.</param>
        /// <param name="action">An action to allow the injecting of the loop code.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ForEach(this IEmitter emitter, ILocal local, Action<ILocal> action)
        {
            if (local.LocalType.IsArray == true)
            {
                return emitter.For(local, action);
            }

            TypeInfo localTypeInfo = local.LocalType.GetTypeInfo();
            if (localTypeInfo.IsGenericType == false ||
                typeof(IEnumerable<>).MakeGenericType(localTypeInfo.GetGenericArguments()).GetTypeInfo().IsAssignableFrom(local.LocalType) == false)
            {
                throw new InvalidOperationException("Not a enumerable type");
            }

            Type enumerableType = localTypeInfo.GetGenericArguments()[0];
            Type enumeratorType = typeof(IEnumerator<>).MakeGenericType(enumerableType);

            MethodInfo getEnumerator = typeof(IEnumerable<>).MakeGenericType(enumerableType).GetTypeInfo().GetMethod("GetEnumerator");
            MethodInfo getCurrent = enumeratorType.GetTypeInfo().GetMethod("get_Current");
            MethodInfo moveNext = typeof(IEnumerator).GetTypeInfo().GetMethod("MoveNext");

            ILocal localEnumerator;
            ILocal localItem;

            ILabel loopStart;
            ILabel loopCheck;
            ILabel loopEnd;
            ILabel endFinally;
            ILabel beginEx;

            emitter
                .DefineLabel("loopStart", out loopStart)
                .DefineLabel("loopCheck", out loopCheck)
                .DefineLabel("loopEnd", out loopEnd)
                .DefineLabel("endFinally", out endFinally)

                .DeclareLocal(enumeratorType, "localEnumerator", out localEnumerator)
                .DeclareLocal(enumerableType, "localItem", out localItem)

                .LdLocS(local)
                .CallVirt(getEnumerator)
                .StLocS(localEnumerator)

            // Try
                .BeginExceptionBlock(out beginEx)

                .BrS(loopCheck)
                .MarkLabel(loopStart)
                .LdLocS(localEnumerator)
                .CallVirt(getCurrent)
                .StLocS(localItem)
                .Nop();

            action(localItem);

            emitter
                .Nop()
                .Nop()

                .MarkLabel(loopCheck)
                .LdLocS(localEnumerator)
                .CallVirt(moveNext)
                .BrTrueS(loopStart)

                .LeaveS(loopEnd)

            // Finally
                .BeginFinallyBlock()

                .LdLocS(localEnumerator)
                .BrFalseS(endFinally)

                .LdLocS(localEnumerator)
                .CallVirt(DisposeMethodInfo)
                .Nop()

                .MarkLabel(endFinally)
                .EndExceptionBlock()
                .MarkLabel(loopEnd);

            return emitter;
        }

        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <typeparam name="T">The locals type.</typeparam>
        /// <param name="emitter">An emitter instance.</param>
        /// <param name="local">A variable to receive a local.</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
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
        /// <returns>The <see cref="IEmitter"/> instance</returns>
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
        /// <returns>The <see cref="IEmitter"/> instance</returns>
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
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter DeclareLocal<T>(this IEmitter emitter,bool pinned, out ILocal local)
        {
            emitter.DeclareLocal(typeof(T), pinned, out local);
            return emitter;
        }

        /// <summary>
        /// Starts a try block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter Try(this IEmitter emitter)
        {
            ILabel label;
            return emitter.BeginExceptionBlock(out label);
        }

        /// <summary>
        /// Starts a try block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <param name="label">The label for the end of the block.</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter Try(this IEmitter emitter, out ILabel label)
        {
            return emitter.BeginExceptionBlock(out label);
        }

        /// <summary>
        /// Starts a catch block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <param name="label">The label for the end of the block.</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
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
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter Catch(this IEmitter emitter, Type exceptionType)
        {
            return emitter.BeginCatchBlock(exceptionType);
        }

        /// <summary>
        /// Starts a catch block and stores the exception in the local.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <param name="exceptionType">The label for the end of the block.</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter Catch(this IEmitter emitter, Type exceptionType, ILocal local)
        {
            if (typeof(Exception).GetTypeInfo().IsAssignableFrom(local.LocalType) == false)
            {
                throw new InvalidOperationException("Local must be an exception type");
            }

            return emitter.Catch(exceptionType)
                .StLoc(local);
        }

        /// <summary>
        /// Starts a finally block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter Finally(this IEmitter emitter)
        {
            return emitter.BeginFinallyBlock();
        }

        /// <summary>
        /// Starts a fault block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter Fault(this IEmitter emitter)
        {
            return emitter.BeginFaultBlock();
        }

        /// <summary>
        /// Starts a filter block.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter Filter(this IEmitter emitter)
        {
            return emitter.BeginExceptFilterBlock();
        }

        /// <summary>
        /// Throws an exception.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter Throw(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Throw);
        }

        /// <summary>
        /// Throws an exception.
        /// </summary>
        /// <typeparam name="TException">The exception type</typeparam>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <param name="message">The exception message</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter ThrowException<TException>(this IEmitter emitter, string message)
            where TException : Exception
        {
            ConstructorInfo ctor =
                typeof(TException)
                .GetTypeInfo()
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
        /// <param name="typeName">The <see cref="LocalBuilder"/> containing the type name.</param>
        /// <param name="dynamicOnly">A value indicating whether or not to only check for dynamically generated types.</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter GetType(this IEmitter emitter, ILocal typeNameLocal, bool dynamicOnly = false)
        {
            return emitter
                .LdLocS(typeNameLocal)
                .Emit(dynamicOnly == false ? OpCodes.Ldc_I4_0 : OpCodes.Ldc_I4_1)
                .Call(GetTypeMethodInfo);
        }

        /// <summary>
        /// Emits IL to load the type for a given type name onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="typeName">The type name.</param>
        /// <param name="dynamicOnly">A value indicating whether or not to only check for dynamically generated types.</param>
        /// <returns>The <see cref="IEmitter"/> instance</returns>
        public static IEmitter GetType(this IEmitter emitter, string typeName, bool dynamicOnly = false)
        {
            return emitter
                .LdStr(typeName)
                .Emit(dynamicOnly == false ? OpCodes.Ldc_I4_0 : OpCodes.Ldc_I4_1)
                .Call(GetTypeMethodInfo);
        }

        /// <summary>
        /// Emits the IL to perform a IF operation.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>> instance.</param>
        /// <param name="expression">A expression to evaluate.</param>
        /// <returns></returns>
        public static IEmitter IF(this IEmitter emitter, Expression<Func<bool>> expression)
        {
            DebugOutput.WriteLine("Return Type: {0}", expression.ReturnType);
            DebugOutput.WriteLine("Node Type: {0}", expression.NodeType);

            BinaryExpression body = expression.Body as BinaryExpression;
            MethodCallExpression left = body.Left as MethodCallExpression;
            ParameterExpression right = body.Right as ParameterExpression;

            DebugOutput.WriteLine("Body: {0}", body.GetType().Name);
            DebugOutput.WriteLine("Body: {0}", left.Object.GetType());

            return emitter;
        }
    }
}