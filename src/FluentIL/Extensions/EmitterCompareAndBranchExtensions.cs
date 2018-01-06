namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Branch and compare <see cref="IEmitter"/> extension methods.
    /// </summary>
    public static class EmitterCompareAndBranchExtensions
    {
        /// <summary>
        /// Unconditionally transfers control to a target instruction.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Br(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Br, target);
        }

        /// <summary>
        /// Unconditionally transfers control to a target instruction.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BrS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Br_S, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the two values on the top of the evaluation stack are equal.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Beq(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Beq, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if two values are equal.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Beq(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .Beq(target);
        }

        /// <summary>
        /// Transfers control to a target instruction if two values on the top of the evaluation stack are equal. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BeqS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Beq_S, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if two values are equal. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BeqS(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BeqS(target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value on the evaluation stack is greater than the second value on the evaluaton stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Bgt(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Bgt, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Bgt(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .Bgt(target);
        }
        
        /// <summary>
        /// Transfers control to a target instruction if the first value on the evaluation stack is greater than the second value on the evaluaton stack. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgtS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Bgt_S, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgtS(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BgtS(target);
        }
 
        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgtUn(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Bgt_Un, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgtUn(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BgtUn(target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgtUnS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Bgt_Un_S, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgtUnS(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BgtUnS(target);
        }
        
 
        /// <summary>
        /// Transfers control to a target instruction if the first value on the evaluation stack is greater than or equal to the second value on the evaluaton stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Bge(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Bge, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than or equal to the second value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Bge(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .Bge(target);
        }
 
        /// <summary>
        /// Transfers control to a target instruction if the first value on the evaluation stack is greater than or equal to the second value on the evaluaton stack. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgeS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Bge_S, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than or equal to the second value. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgeS(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BgeS(target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgeUn(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Bge_Un, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgeUn(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BgeUn(target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgeUnS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Bge_Un_S, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BgeUnS(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BgeUnS(target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value on the evaluation stack is less than the second value on the evaluaton stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Blt(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Blt, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than the second value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Blt(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .Blt(target);
        }
       
        /// <summary>
        /// Transfers control to a target instruction if the first value on the evaluation stack is less than the second value on the evaluaton stack. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BltS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Blt_S, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than the second value. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BltS(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BltS(target);
        }
 
        /// <summary>
        /// Transfers control to a target instruction if the first value is less than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BltUn(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Blt_Un, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BltUn(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BltUn(target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BltUnS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Blt_Un_S, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BltUnS(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BltUnS(target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value on the evaluation stack is less than or equal to the second value on the evaluaton stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Ble(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Ble, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than or equal to the second value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Ble(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .Ble(target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value on the evaluation stack is less than or equal to the second value on the evaluaton stack. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BleS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Ble_S, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than or equal to the second value. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BleS(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BleS(target);
        }
 
        /// <summary>
        /// Transfers control to a target instruction if the first value on the evaluation stack is less than or equal to the second value,
        /// when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BleUn(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Ble_Un, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than or equal to the second value,
        /// when comparing unsigned integer values or unordered float values.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BleUn(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BleUn(target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value on the evaluation stack is less than or equal to the second value,
        /// when comparing unsigned integer values or unordered float values. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BleUnS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Ble_Un_S, target);
        }

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than or equal to the second value,
        /// when comparing unsigned integer values or unordered float values. (Short form)
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BleUnS(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BleUnS(target);
        }

        /// <summary>
        /// Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BneUn(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Bne_Un, target);
        }

        /// <summary>
        /// Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BneUn(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BneUn(target);
        }
        
        /// <summary>
        /// Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BneUnS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Bne_Un_S, target);
        }

        /// <summary>
        /// Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localValue1">The first value.</param>
        /// <param name="localValue2">The second value.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter BneUnS(this IEmitter emitter, ILocal localValue1, ILocal localValue2, ILabel target)
        {
            return emitter
                .LdLoc(localValue1)
                .LdLoc(localValue2)
                .BneUnS(target);
        }
 
         /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter"></param>
        /// <param name="target"></param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter BrTrue(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Brtrue, target);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter"></param>
        /// <param name="target"></param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter BrTrueS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Brtrue_S, target);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter"></param>
        /// <param name="target"></param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter BrFalse(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Brfalse, target);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emitter"></param>
        /// <param name="target"></param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter BrFalseS(this IEmitter emitter, ILabel target)
        {
            return emitter.Emit(OpCodes.Brfalse_S, target);
        }

        /// <summary>
        /// Compares two values. If they are equal, the integer value 1 (int32) is pushed onto the evaluation stack;
        /// otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Ceq(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ceq);
        }

        /// <summary>
        /// Compares two values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack;
        /// otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Cgt(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Cgt);
        }

        /// <summary>
        /// Compares two unsigned or unordered values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack;
        /// otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter CgtUn(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Cgt_Un);
        }

        /// <summary>
        /// Compares two values. If the first value is less than the second, the integer value 1 (int32) is pushed onto the evaluation stack;
        /// otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter Clt(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Clt);
        }

        /// <summary>
        /// Compares the unsigned or unordered values value1 and value2. If value1 is less than value2, then the integer value 1 (int32) is pushed onto the evaluation stack;
        /// otherwise 0 (int32) is pushed onto the evaluation stack
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="target">The target instruction.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter CltUn(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Clt_Un);
        }

    }
}