namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Arithmetic <see cref="IEmitter"/> extension methods.
    /// </summary>
    public static class EmitterArithmeticExtensions
    {
        /// <summary>
        /// Throws ArithmeticException if value is not a finite number.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter CkFinite(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ckfinite);
        }

        /// <summary>
        /// Negates a value and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Neg(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Neg);
        }

        /// <summary>
        /// Adds two values and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Add(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Add);
        }

        /// <summary>
        /// Adds two values and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Add(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .Add();
        }

        /// <summary>
        /// Adds two integers, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter AddOvf(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Add_Ovf);
        }

        /// <summary>
        /// Adds two integers, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter AddOvf(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .AddOvf();
        }

        /// <summary>
        /// Adds two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter AddOvfUn(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Add_Ovf_Un);
        }

        /// <summary>
        /// Adds two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter AddOvfUn(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .AddOvfUn();
        }

        /// <summary>
        /// Subtracts one value from another and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Sub(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Sub);
        }

        /// <summary>
        /// Subtracts one value from another and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Sub(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .Sub();
        }

        /// <summary>
        /// Subtracts one integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter SubOvf(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Sub_Ovf);
        }

        /// <summary>
        /// Subtracts one integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter SubOvf(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .SubOvf();
        }

        /// <summary>
        /// Subtracts one integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter SubOvfUn(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Sub_Ovf_Un);
        }

        /// <summary>
        /// Subtracts one integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter SubOvfUn(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .SubOvfUn();
        }

        /// <summary>
        /// Multiplies two values and pushes the result on the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Mul(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Mul);
        }

        /// <summary>
        /// Multiplies two values and pushes the result on the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Mul(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .Mul();
        }

        /// <summary>
        /// Multiplies two integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter MulOvf(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Mul_Ovf);
        }

        /// <summary>
        /// Multiplies two integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter MulOvf(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .MulOvf();
        }

        /// <summary>
        /// Multiplies two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter MulOvfUn(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Mul_Ovf_Un);
        }

        /// <summary>
        /// Multiplies two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter MulOvfUn(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .MulOvfUn();
        }

        /// <summary>
        /// Divides two values and pushes the result as a floating-point (type F) or quotient (type int32) onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Div(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Div);
        }

        /// <summary>
        /// Divides two values and pushes the result as a floating-point (type F) or quotient (type int32) onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Div(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .Div();
        }

        /// <summary>
        /// Divides two unsigned integer values and pushes the result (int32) onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter DivUn(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Div_Un);
        }

        /// <summary>
        /// Divides two unsigned integer values and pushes the result (int32) onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter DivUn(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .DivUn();
        }

        /// <summary>
        /// Divides two values and pushes the remainder onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Rem(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Rem);
        }

        /// <summary>
        /// Divides two values and pushes the remainder onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Rem(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .Rem();
        }

        /// <summary>
        /// Divides two unsigned values and pushes the remainder onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter RemUn(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Rem_Un);
        }

        /// <summary>
        /// Divides two unsigned values and pushes the remainder onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first local value.</param>
        /// <param name="localValue2">The second local value.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter RemUn(this IEmitter emitter, ILocal localValue1, ILocal localValue2)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .RemUn();
        }

    }
}