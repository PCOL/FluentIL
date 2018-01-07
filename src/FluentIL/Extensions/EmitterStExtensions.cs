namespace FluentIL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Store <see cref="IEmitter"/> extension methods.
    /// </summary>
    public static class EmitterStExtensions
    {
        /// <summary>
        /// Emits IL to store the object at the top of the evaluation stack in an argument.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="local">A local</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StArg(this IEmitter emitter, short index)
        {
            return emitter.Emit(OpCodes.Starg, index);
        }

        /// <summary>
        /// Emits IL to store the object at the top of the evaluation stack in an argument.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="local">A local</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StArgS(this IEmitter emitter, byte index)
        {
            return emitter.Emit(OpCodes.Starg_S, index);
        }

        /// <summary>
        /// Emits IL to store the object at the top of the evaluation stack in an argument.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="local">A local</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StArg(this IEmitter emitter, ILocal localValue, short index)
        {
            return emitter
                .LdLoc(localValue)
                .StArg(index);
        }

        /// <summary>
        /// Emits IL to store the object at the top of the evaluation stack in an argument.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="local">A local</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StArgS(this IEmitter emitter, ILocal localValue, byte index)
        {
            return emitter
                .LdLoc(localValue)
                .StArgS(index);
        }

        /// <summary>
        /// Emits IL to store the object at the top of the evaluation stack in a local.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="local">A local</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StLoc(this IEmitter emitter, ILocal local)
        {
            return emitter.Emit(OpCodes.Stloc, local);
        }

        /// <summary>
        /// Emits IL to store the object at the top of the evaluation stack in a local.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="local">A local</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StLocS(this IEmitter emitter, ILocal local)
        {
            return emitter.Emit(OpCodes.Stloc_S, local);
        }

        /// <summary>
        /// Emits IL to store the object at the top of the evaluation stack in a local.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="field">A field</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StFld(this IEmitter emitter, IFieldBuilder field)
        {
            return emitter.Emit(OpCodes.Stfld, field);
        }

        /// <summary>
        /// Emits IL to store the object at the top of the evaluation stack in a local.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <param name="field">A field</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StFld(this IEmitter emitter, FieldInfo field)
        {
            return emitter.Emit(OpCodes.Stfld, field);
        }

        /// <summary>
        /// Emits the IL to indirectly store a value from the top of the evaluation stack.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The type to store.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter EmitStInd(this IEmitter emitter, Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                case TypeCode.SByte:
                case TypeCode.Byte:
                    emitter.Emit(OpCodes.Stind_I1);
                    break;

                case TypeCode.Char:
                case TypeCode.Int16:
                    emitter.Emit(OpCodes.Stind_I2);
                    break;

                case TypeCode.Int32:
                case TypeCode.UInt32:
                    emitter.Emit(OpCodes.Stind_I4);
                    break;

                case TypeCode.Int64:
                case TypeCode.UInt64:
                    emitter.Emit(OpCodes.Stind_I8);
                    break;

                case TypeCode.Single:
                    emitter.Emit(OpCodes.Stind_R4);
                    break;

                case TypeCode.Double:
                    emitter.Emit(OpCodes.Stind_R8);
                    break;

                default:
                    emitter.Emit(OpCodes.Stobj, type);
                    break;
            }

            return emitter;
        }
    }
}