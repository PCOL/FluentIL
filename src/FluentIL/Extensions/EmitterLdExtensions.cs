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
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="value"></param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdStr(this IEmitter emitter, string value)
        {
            emitter.Emit(OpCodes.Ldstr, value);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="local"></param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLoc(this IEmitter emitter, ILocal local)
        {
            emitter.Emit(OpCodes.Ldloc, local);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="local"></param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLocS(this IEmitter emitter, ILocal local)
        {
            emitter.Emit(OpCodes.Ldloc_S, local);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLoc0(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldloc_0);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLoc1(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldloc_1);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLoc2(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldloc_2);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLoc3(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldloc_3);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdLocA(this IEmitter emitter, ILocal local)
        {
            emitter.LdLocA(local.LocalIndex);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdLocA(this IEmitter emitter, int index)
        {
            emitter.Emit(OpCodes.Ldloca, Convert.ToInt16(index));
            return emitter;
        }

        /// <summary>
        /// Emits IL to load the address of a local onto the evaluation stack (short form).
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdLocAS(this IEmitter emitter, ILocal local)
        {
            int index = local.LocalIndex;
            if (index > 255)
            {
                throw new InvalidProgramException("Local index greater than 255 so short form cannot be used");
            }

            emitter.LdLocAS(Convert.ToByte(index));
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdLocAS(this IEmitter emitter, byte index)
        {
            emitter.Emit(OpCodes.Ldloca, index);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field"></param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdFld(this IEmitter emitter, FieldInfo field)
        {
            emitter.Emit(OpCodes.Ldfld, field);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field"></param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdFld(this IEmitter emitter, IFieldBuilder field)
        {
            emitter.Emit(OpCodes.Ldfld, field);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field"></param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdFlda(this IEmitter emitter, FieldBuilder field)
        {
            emitter.Emit(OpCodes.Ldflda, field);
            return emitter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="field"></param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdFlda(this IEmitter emitter, IFieldBuilder field)
        {
            emitter.Emit(OpCodes.Ldflda, field);
            return emitter;
        }

        /// <summary>
        /// Loads an argument by index.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="index"></param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdArg(this IEmitter emitter, int index)
        {
            emitter.Emit(OpCodes.Ldarg, index);
            return emitter;
        }

        /// <summary>
        /// Loads the first argument.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdArg0(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldarg_0);
            return emitter;
        }

        /// <summary>
        /// Loads the second argument.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdArg1(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldarg_1);
            return emitter;
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
        /// <param name="index"></param>
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
        /// <param name="type"></param>
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
        /// <param name="method"></param>
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
        /// <param name="field">The field.</param>
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
        /// <param name="field">The field.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdToken(this IEmitter emitter, IFieldBuilder field)
        {
            return emitter
                .LdToken(field.Define());
        }

        /// <summary>
        /// Loads a null.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <param name="type"></param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdNull(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldnull);
            return emitter;
        }

        /// <summary>
        /// Emits the IL to indirectly load a value onto the evaluation stack.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The type to load.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter EmitLdInd(this IEmitter emitter, Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                case TypeCode.SByte:
                    emitter.Emit(OpCodes.Ldind_I1);
                    break;

                case TypeCode.Char:
                case TypeCode.Int16:
                    emitter.Emit(OpCodes.Ldind_I2);
                    break;

                case TypeCode.Byte:
                    emitter.Emit(OpCodes.Ldind_U1);
                    break;

                case TypeCode.Int32:
                    emitter.Emit(OpCodes.Ldind_I4);
                    break;

                case TypeCode.UInt32:
                    emitter.Emit(OpCodes.Ldind_U4);
                    break;

                case TypeCode.Int64:
                case TypeCode.UInt64:
                    emitter.Emit(OpCodes.Ldind_I8);
                    break;

                case TypeCode.Single:
                    emitter.Emit(OpCodes.Ldind_R4);
                    break;

                case TypeCode.Double:
                    emitter.Emit(OpCodes.Ldind_R8);
                    break;

                default:
                    emitter.Emit(OpCodes.Ldobj, type);
                    break;
            }

            return emitter;
        }
    }
}