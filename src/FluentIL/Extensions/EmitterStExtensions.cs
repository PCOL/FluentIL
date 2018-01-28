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
        /// Emits the IL to copy a value of a specified type from the top of the evaluation stack
        /// into the supplied address.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The type to store.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StObj(this IEmitter emitter, Type type)
        {
            return emitter.Emit(OpCodes.Stobj, type);
        }

        /// <summary>
        /// Emits the IL to indirectly store a value of type native int from the top of the evaluation stack.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StIndI(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stind_I);
        }

        /// <summary>
        /// Emits the IL to indirectly store a value of type int8 from the top of the evaluation stack.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StIndI1(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stind_I1);
        }

        /// <summary>
        /// Emits the IL to indirectly store a value of type int16 from the top of the evaluation stack.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StIndI2(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stind_I2);
        }

        /// <summary>
        /// Emits the IL to indirectly store a value of type int32 from the top of the evaluation stack.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StIndI4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stind_I4);
        }

        /// <summary>
        /// Emits the IL to indirectly store a value of type int64 from the top of the evaluation stack.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StIndI8(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stind_I8);
        }

        /// <summary>
        /// Emits the IL to indirectly store a value of type float32 from the top of the evaluation stack.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StIndR4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stind_R4);
        }

        /// <summary>
        /// Emits the IL to indirectly store a value of type float64 from the top of the evaluation stack.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StIndR8(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stind_R8);
        }

        /// <summary>
        /// Emits the IL to indirectly store a object reference from the top of the evaluation stack.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StIndRef(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stind_Ref);
        }

        /// <summary>
        /// Emits the IL to indirectly store a value from the top of the evaluation stack.
        /// </summary>
	    /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The type to store.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter StInd(this IEmitter emitter, Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                case TypeCode.SByte:
                case TypeCode.Byte:
                    return emitter.StIndI1();

                case TypeCode.Char:
                case TypeCode.Int16:
                    return emitter.StIndI2();

                case TypeCode.Int32:
                case TypeCode.UInt32:
                    return emitter.StIndI4();

                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return emitter.StIndI8();

                case TypeCode.Single:
                    return emitter.StIndR4();
                    
                case TypeCode.Double:
                    return emitter.StIndR8();

                default:
                    return emitter.StObj(type);
            }
        }
    }
}