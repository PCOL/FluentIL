namespace FluentIL
{
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Load constant <see cref="IEmitter"/> extension methods.
    /// </summary>
    public static class EmitterLdcExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter"></param>
        /// <param name="value"></param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdcI4(this IEmitter emitter, int value)
        {
            return emitter.Emit(OpCodes.Ldc_I4, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI4_0(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldc_I4_0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI4_1(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldc_I4_1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI4_2(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldc_I4_2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI4_3(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldc_I4_3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI4_4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldc_I4_4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI4_5(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldc_I4_5);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI4_6(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldc_I4_6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI4_7(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldc_I4_7);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI4_8(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldc_I4_8);
        }

        /// <summary>
        /// Pushes the integer value of -1 onto the evaluation stack as an int32.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI4_M1(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldc_I4_M1);
        }

        /// <summary>
        /// Pushes the supplied int8 value onto the evaluation stack as an int32, short form.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI4_S(this IEmitter emitter, byte value)
        {
            return emitter.Emit(OpCodes.Ldc_I4_S, value);
        }
        
        /// <summary>
        /// Pushes a supplied value of type int64 onto the evaluation stack as an int64.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcI8(this IEmitter emitter, long value)
        {
            return emitter.Emit(OpCodes.Ldc_I8, value);
        }

        /// <summary>
        /// Pushes a supplied value of type float32 onto the evaluation stack as type F (float).
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcR4(this IEmitter emitter, float value)
        {
            return emitter.Emit(OpCodes.Ldc_R4, value);
        }

        /// <summary>
        /// Pushes a supplied value of type float64 onto the evaluation stack as type F (float).
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdcR8(this IEmitter emitter, double value)
        {
            return emitter.Emit(OpCodes.Ldc_R8, value);
        }
    }
}