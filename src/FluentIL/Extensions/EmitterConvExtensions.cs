namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Conversion <see cref="IEmitter"/> extension methods.
    /// </summary>
    public static class EmitterConvExtensions
    {
        /// <summary>
        /// Converts the value on the top of the evaluation stack to a native int.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvI(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_I);
        }

        /// <summary>
        /// Converts the value on the top of the evaluation stack to int8 then pads it to int32.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvI1(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_I1);
        }

        /// <summary>
        /// Converts the value on the top of the evaluation stack to int16 then pads it to int32.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvI2(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_I2);
        }

        /// <summary>
        /// Converts the value on the top of the evaluation stack to int32.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvI4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_I4);
        }
        
        /// <summary>
        /// Converts the value on the top of the evaluation stack to int64.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvI8(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_I8);
        }

        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed native int, throwing OverflowException on overflow.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvOvfI(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_Ovf_I);
        }
        
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int8 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvOvfI1(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_Ovf_I1);
        }
        
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int16 and extending it to int32, throwing OverflowException on overflow.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvOvfI2(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_Ovf_I2);
        }
        
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int32, throwing OverflowException on overflow.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvOvfI4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_Ovf_I4);
        }
       
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int64, throwing OverflowException on overflow.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvOvfI8(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_Ovf_I8);
        }
        
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed native int, throwing OverflowException on overflow.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvOvfIUn(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_Ovf_I_Un);
        }
        
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int8 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvOvfI1Un(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_Ovf_I1_Un);
        }

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int16 and extending it to int32, throwing OverflowException on overflow.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvOvfI2Un(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_Ovf_I2_Un);
        }

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int32, throwing OverflowException on overflow.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvOvfI4Un(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_Ovf_I4_Un);
        }

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int64, throwing OverflowException on overflow.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvOvfI8Un(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_Ovf_I8_Un);
        }

        /// <summary>
        /// Converts the unsigned integer value on top of the evaluation stack to float32.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvRUn(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_R_Un);
        }

        /// <summary>
        /// Converts the value on top of the evaluation stack to float32.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvR4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_R4);
        }

        /// <summary>
        /// Converts the value on top of the evaluation stack to float64.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvR8(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_R8);
        }

        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned native int, and extends it to native int.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvU(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_U);
        }

        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int8, and extends it to int32.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvU1(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_U1);
        }

        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int16, and extends it to int32.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvU2(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_U2);
        }

        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int32, and extends it to int32.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvU4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_U4);
        }

        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int64, and extends it to int64.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter ConvU8(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Conv_U8);
        }

        /// <summary>
        /// Emits IL to convert one type to another.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="sourceType">The source type.</param>
        /// <param name="targetType">The destination type.</param>
        /// <param name="isAddress">A value indicating whether or not the convert is for an address.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Conv(
            this IEmitter emitter,
            Type sourceType,
            Type targetType,
            bool isAddress)
        {
            if (sourceType != targetType)
            {
                if (sourceType.IsByRef == true)
                {
                    Type elementType = sourceType.GetElementType();
                    emitter.LdInd(elementType);
                    emitter.Conv(elementType, targetType, isAddress);
                }
                else if (targetType.IsValueType == true)
                {
                    if (sourceType.IsValueType == true)
                    {
                        emitter.EmitConv(targetType);
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
                else if (targetType.IsAssignableFrom(sourceType) == true)
                {
                    if (sourceType.IsValueType == true)
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
        /// Emits IL to convert a type.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="type">The type.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter EmitConv(this IEmitter emitter, Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                case TypeCode.SByte:
                    emitter.ConvI1();
                    break;

                case TypeCode.Char:
                case TypeCode.Int16:
                    emitter.ConvI2();
                    break;

                case TypeCode.Byte:
                    emitter.ConvU2();
                    break;

                case TypeCode.Int32:
                    emitter.ConvI4();
                    break;

                case TypeCode.UInt32:
                    emitter.ConvU4();
                    break;

                case TypeCode.Int64:
                case TypeCode.UInt64:
                    emitter.ConvI8();
                    break;

                case TypeCode.Single:
                    emitter.ConvR4();
                    break;

                case TypeCode.Double:
                    emitter.ConvR8();
                    break;

                default:
                    emitter.Nop();
                    break;
            }

            return emitter;
        }
    }
}