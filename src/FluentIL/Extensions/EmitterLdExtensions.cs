namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Load <see cref="IEmitter"/> extension methods.
    /// </summary>
    public static class EmitterLdExtensions
    {
        /// <summary>
        /// Emits the IL to load a string onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="value">The string to load.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdStr(this IEmitter emitter, string value)
        {
            return emitter.Emit(OpCodes.Ldstr, value);
        }

        /// <summary>
        /// Emits the IL to load a local onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="local">The local to load.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLoc(this IEmitter emitter, ILocal local)
        {
            return emitter.Emit(OpCodes.Ldloc, local);
        }

        /// <summary>
        /// Emits the IL to load a local onto the evaluation stack (short form).
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="local">The local to load.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLocS(this IEmitter emitter, ILocal local)
        {
            return emitter.Emit(OpCodes.Ldloc_S, local);
        }

        /// <summary>
        /// Emits the IL to load a local at position 0 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLoc0(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldloc_0);
        }

        /// <summary>
        /// Emits the IL to load a local at position 1 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLoc1(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldloc_1);
        }

        /// <summary>
        /// Emits the IL to load a local at position 2 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLoc2(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldloc_2);
        }

        /// <summary>
        /// Emits the IL to load a local at position 3 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLoc3(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldloc_3);
        }

        /// <summary>
        /// Emits the IL to load the address of a local onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="local">A local.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdLocA(this IEmitter emitter, ILocal local)
        {
            return emitter.LdLocA(local.LocalIndex);
        }

        /// <summary>
        /// Emits the IL to load the address of a local onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="index">A locals index.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdLocA(this IEmitter emitter, int index)
        {
            return emitter.Emit(OpCodes.Ldloca, Convert.ToInt16(index));
        }

        /// <summary>
        /// Emits IL to load the address of a local onto the evaluation stack (short form).
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="local">The local.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdLocAS(this IEmitter emitter, ILocal local)
        {
            return emitter.Defer(
                (e) =>
                {
                    int index = local.LocalIndex;
                    if (index > 255)
                    {
                        throw new InvalidProgramException("Local index greater than 255 so short form cannot be used");
                    }

                    e.LdLocAS(Convert.ToByte(index));
                });
        }

        /// <summary>
        /// Emits IL to load the address of a local onto the evaluation stack (short form).
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="index">A locals index.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdLocAS(this IEmitter emitter, byte index)
        {
            return emitter.Emit(OpCodes.Ldloca_S, index);
        }

        /// <summary>
        /// Emits the IL to load the value of a field onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field">The field to load.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdFld(this IEmitter emitter, FieldInfo field)
        {
            return emitter.Emit(OpCodes.Ldfld, field);
        }

        /// <summary>
        /// Emits the IL to load the value of a field onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field">The field to load.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdFld(this IEmitter emitter, IFieldBuilder field)
        {
            return emitter.Emit(OpCodes.Ldfld, field);
        }

        /// <summary>
        /// Emits the IL to load the address of a field onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field">The field to load the address of.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdFlda(this IEmitter emitter, FieldBuilder field)
        {
            return emitter.Emit(OpCodes.Ldflda, field);
        }

        /// <summary>
        /// Emits the IL to load the address of a field onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field">The field to load the address of.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdFlda(this IEmitter emitter, IFieldBuilder field)
        {
            return emitter.Emit(OpCodes.Ldflda, field);
        }

        /// <summary>
        /// Emits the IL to load the value of a static field onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field">A <see cref="IFieldBuilder"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdsFld(this IEmitter emitter, IFieldBuilder field)
        {
            return emitter.Emit(OpCodes.Ldsfld, field);
        }

        /// <summary>
        /// Emits the IL to load the value of a static field onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field">A <see cref="FieldBuilder"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdsFld(this IEmitter emitter, FieldBuilder field)
        {
            return emitter.Emit(OpCodes.Ldsfld, field);
        }

        /// <summary>
        /// Emits the IL to load the address of a static field onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field">A <see cref="IFieldBuilder"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdsFlda(this IEmitter emitter, IFieldBuilder field)
        {
            return emitter.Emit(OpCodes.Ldsflda, field);
        }

        /// <summary>
        /// Emits the IL to load the address of a static field onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field">A <see cref="FieldBuilder"/>.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdsFlda(this IEmitter emitter, FieldBuilder field)
        {
            return emitter.Emit(OpCodes.Ldsflda, field);
        }

        /// <summary>
        /// Loads an argument by index.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The arguments index.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdArg(this IEmitter emitter, int index)
        {
            return emitter.Emit(OpCodes.Ldarg, index);
        }

        /// <summary>
        /// Loads the first argument.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdArg0(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldarg_0);
        }

        /// <summary>
        /// Loads the second argument.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdArg1(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldarg_1);
        }

        /// <summary>
        /// Loads the third argument.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdArg2(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldarg_2);
            return emitter;
        }

        /// <summary>
        /// Loads the forth argument.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdArg3(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldarg_3);
            return emitter;
        }

        /// <summary>
        /// Loads an argument by index (short form).
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The argument index.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdArgS(this IEmitter emitter, byte index)
        {
            emitter.Emit(OpCodes.Ldarg_S, index);
            return emitter;
        }

        /// <summary>
        /// Loads a types token.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The type to load the token for.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdToken(this IEmitter emitter, Type type)
        {
            emitter.Emit(OpCodes.Ldtoken, type);
            return emitter;
        }

        /// <summary>
        /// Loads a method token.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="method">The method to load the token for.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdToken(this IEmitter emitter, MethodInfo method)
        {
            emitter.Emit(OpCodes.Ldtoken, method);
            return emitter;
        }

        /// <summary>
        /// Loads a fields token.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field">The field to load the token for.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdToken(this IEmitter emitter, FieldInfo field)
        {
            emitter.Emit(OpCodes.Ldtoken, field);
            return emitter;
        }

        /// <summary>
        /// Loads a fields token.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field">The field to load the token for.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdToken(this IEmitter emitter, IFieldBuilder field)
        {
            return emitter
                .LdToken(field.Define());
        }

        /// <summary>
        /// Emits the IL to Load a null onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdNull(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldnull);
            return emitter;
        }

        /// <summary>
        /// Emits the IL to indirectly copy a value type onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdObj(this IEmitter emitter, Type type)
        {
            emitter.Emit(OpCodes.Ldobj, type);
            return emitter;
        }

        /// <summary>
        /// Emits the IL to indirectly load a native int onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdIndI(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldind_I);
        }

        /// <summary>
        /// Emits the IL to indirectly load an int8 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdIndI1(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldind_I1);
        }

        /// <summary>
        /// Emits the IL to indirectly load an int16 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdIndI2(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldind_I2);
        }

        /// <summary>
        /// Emits the IL to indirectly load an int32 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdIndI4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldind_I4);
        }

        /// <summary>
        /// Emits the IL to indirectly load an int64 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdIndI8(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldind_I8);
        }

        /// <summary>
        /// Emits the IL to indirectly load a float32 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdIndR4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldind_R4);
        }

        /// <summary>
        /// Emits the IL to indirectly load a float64 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdIndR8(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldind_R8);
        }

        /// <summary>
        /// Emits the IL to indirectly load an object onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdIndRef(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldind_Ref);
        }

        /// <summary>
        /// Emits the IL to indirectly load an unsigned int8 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdIndU1(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldind_U1);
        }

        /// <summary>
        /// Emits the IL to indirectly load an unsigned int16 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdIndU2(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldind_U2);
        }

        /// <summary>
        /// Emits the IL to indirectly load an unsigned int32 onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdIndU4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldind_U4);
        }

        /// <summary>
        /// Emits the IL to indirectly load a value onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The type to load.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdInd(this IEmitter emitter, Type type)
        {
#if NETSTANDARD1_6
            switch (Type.GetTypeCode(type))
#else
            switch (Type.GetTypeCode(type))
#endif
            {
                case TypeCode.Boolean:
                case TypeCode.SByte:
                    return emitter.LdIndI1();

                case TypeCode.Char:
                case TypeCode.Int16:
                    return emitter.LdIndI2();

                case TypeCode.Byte:
                    return emitter.LdIndU1();

                case TypeCode.Int32:
                    return emitter.LdIndI4();

                case TypeCode.UInt32:
                    return emitter.LdIndU4();

                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return emitter.LdIndI8();

                case TypeCode.Single:
                    return emitter.LdIndR4();

                case TypeCode.Double:
                    return emitter.LdIndR8();

                default:
                    return emitter.LdObj(type);
            }
        }
    }
}