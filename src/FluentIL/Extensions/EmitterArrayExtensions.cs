namespace FluentIL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL.Builders;

    /// <summary>
    /// Array helper/extension methods.
    /// </summary>
    public static class EmitterArrayExtensions
    {
        /// <summary>
        /// Emits the <see cref="OpCodes.Newarr"/> IL opcode.
        /// </summary>
        /// <typeparam name="T">The array type.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter NewArr<T>(this IEmitter emitter)
        {
            return emitter.NewArr(typeof(T));
        }

        /// <summary>
        /// Emits the <see cref="OpCodes.Newarr"/> IL opcode.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="arrayType">The array type.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter NewArr(this IEmitter emitter, Type arrayType)
        {
            return emitter.Emit(OpCodes.Newarr, arrayType);
        }

        /// <summary>
        /// Emits the IL to create an array of a given length and type onto
        /// the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The array type.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="length">The array length.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter NewArr<T>(this IEmitter emitter, int length)
        {
            return emitter.NewArr(typeof(T), length);
        }

        /// <summary>
        /// Emits the IL to create an array of a given length and type onto
        /// the top of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="arrayType">The array type.</param>
        /// <param name="length">The array length.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter NewArr(this IEmitter emitter, Type arrayType, int length)
        {
            return emitter
                .LdcI4(length)
                .NewArr(arrayType);
        }

        /// <summary>
        /// Emits the IL to create an array of a given length and type, and
        /// stores it in a given local.
        /// </summary>
        /// <typeparam name="T">The array type.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="length">The array length.</param>
        /// <param name="localArray">The local to store the array in.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter NewArr<T>(this IEmitter emitter, int length, ILocal localArray)
        {
            return emitter.NewArr(typeof(T), length, localArray);
        }

        /// <summary>
        /// Emits the IL to create an array of a given length and type, and
        /// stores it in a given local.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="arrayType">The array type.</param>
        /// <param name="length">The array length.</param>
        /// <param name="localArray">The local to store the array in.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter NewArr(this IEmitter emitter, Type arrayType, int length, ILocal localArray)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            return emitter
                .LdcI4(length)
                .NewArr(arrayType)
                .StLoc(localArray);
        }

        /// <summary>
        /// Emits IL to load the length of an array onto the top of the evaluation stack.
        /// The array is popped from top of the evaluation stack so must have been put
        /// there prior to making this call.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLen(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldlen);
        }

        /// <summary>
        /// Emits IL to load the length of an array onto the tp of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localArray">A local containing an array.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdLen(this IEmitter emitter, ILocal localArray)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("Local must be an array");
            }

            return emitter
                .LdLoc(localArray)
                .Emit(OpCodes.Ldlen);
        }

        /// <summary>
        /// Emits the <see cref="OpCodes.Ldelem"/> opcode.
        /// </summary>
        /// <typeparam name="T">The array element type.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdArrayElem<T>(this IEmitter emitter)
        {
            return emitter.LdArrayElem(typeof(T));
        }

        /// <summary>
        /// Emits the <see cref="OpCodes.Ldelem"/> opcode.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="elementType">The element type.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdArrayElem(this IEmitter emitter, Type elementType)
        {
            return emitter.Emit(OpCodes.Ldelem, elementType);
        }

        /// <summary>
        /// Emits the IL to load and array element onto the top of the evalation stack.
        /// </summary>
        /// <typeparam name="T">The array element type.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The index of the array element to load.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdArrayElem<T>(this IEmitter emitter, int index)
        {
            return emitter
                .LdArrayElem(typeof(T), index);
        }

        /// <summary>
        /// Emits the IL to load and array element onto the top of the evalation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="elementType">The element type.</param>
        /// <param name="index">The index of the array element to load.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdArrayElem(this IEmitter emitter, Type elementType, int index)
        {
            return emitter
                .LdcI4(index)
                .LdArrayElem(elementType);
        }

        /// <summary>
        /// Emits the IL to load an array element of a given array onto the top of the evalation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localArray">A local containing an array.</param>
        /// <param name="index">The index of the array element to load.</param>
        /// <returns>The <see cref="IEmitter"/> instance.</returns>
        public static IEmitter LdArrayElem(this IEmitter emitter, ILocal localArray, int index)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("Local must be an array");
            }

            return emitter
                .LdLoc(localArray)
                .LdcI4(index)
                .LdArrayElem(localArray.LocalType);
        }

        /// <summary>
        /// Emits the IL to load a array element of type <b>native int</b> onto the top
        /// of the evaluation stack as a <b>native int</b>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemI(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldelem_I);
        }

        /// <summary>
        /// Emits the IL to load a array element of type <b>native int</b> onto the top
        /// of the evaluation stack as a <b>native int</b>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemI(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .LdElemI();
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="byte"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemI1(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldelem_I1);
            return emitter;
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="byte"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemI1(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .LdElemI1();
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="short"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemI2(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldelem_I2);
            return emitter;
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="short"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemI2(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .LdElemI2();
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="int"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemI4(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldelem_I4);
            return emitter;
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="int"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemI4(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .LdElemI4();
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="long"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemI8(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldelem_I8);
            return emitter;
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="long"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemI8(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .LdElemI8();
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="float"/> onto the top
        /// of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemR4(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldelem_R4);
            return emitter;
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="float"/> onto the top
        /// of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemR4(this IEmitter emitter, int index)
        {
            emitter.LdcI4(index);
            emitter.LdElemR4();
            return emitter;
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="double"/> onto the top
        /// of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemR8(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldelem_R8);
            return emitter;
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="double"/> onto the top
        /// of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemR8(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .LdElemR8();
        }

        /// <summary>
        /// Emits the IL to load a array element containing an object reference onto the top
        /// of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemRef(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldelem_Ref);
            return emitter;
        }

        /// <summary>
        /// Emits the IL to load a array element containing an object reference onto the top
        /// of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemRef(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .LdElemRef();
        }

        /// <summary>
        /// Emits the IL to load a array element of type <b>unsigned int8</b> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemU1(this IEmitter emitter)
        {
            emitter.Emit(OpCodes.Ldelem_U1);
            return emitter;
        }

        /// <summary>
        /// Emits the IL to load a array element of type <b>unsigned int8</b> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemU1(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .LdElemU1();
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="ushort"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemU2(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldelem_U2);
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="ushort"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemU2(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .LdElemU2();
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="uint"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemU4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldelem_U4);
        }

        /// <summary>
        /// Emits the IL to load a array element of type <see cref="uint"/> onto the top
        /// of the evaluation stack as an <see cref="int"/>.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemU4(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .LdElemU4();
        }

        /// <summary>
        /// Emits the IL to load the address of a given array element onto the top
        /// of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemAddr(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Ldelema);
        }

        /// <summary>
        /// Emits the IL to load the address of a given array element onto the top
        /// of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter LdElemAddr(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .LdElemAddr();
        }

        /// <summary>
        /// Emits the IL to replace the array element at a given index with the value on the top
        /// of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElem<T>(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stelem, typeof(T));
        }

        /// <summary>
        /// Emits the IL to replace the array element at a given index with the value on the top
        /// of the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="elementType">The elements type.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElem(this IEmitter emitter, Type elementType)
        {
            return emitter.Emit(OpCodes.Stelem, elementType);
        }

        /// <summary>
        /// Emits the IL to replace the array element at a given index of the array on the top of the
        /// evaluation stack with a value in a local.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="elementType">The elements type.</param>
        /// <param name="index">The element index.</param>
        /// <param name="localValue">A local conatng the value to store.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElem(this IEmitter emitter, Type elementType, int index, ILocal localValue)
        {
            return emitter
                .LdcI4(index)
                .LdLoc(localValue)
                .StElem(elementType);
        }

        /// <summary>
        /// Emits the IL to replace the array element at a given index of a given array with the
        /// value in a local.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localArray">A local contaning an array.</param>
        /// <param name="index">The element index.</param>
        /// <param name="localValue">A local conatng the value to store.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElem(this IEmitter emitter, ILocal localArray, int index, ILocal localValue)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            return emitter
                .LdLoc(localArray)
                .LdcI4(index)
                .LdLoc(localValue)
                .StElem(localArray.LocalType);
        }

        /// <summary>
        /// Emit IL to set an array element.
        /// The array, index, and value must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stelem_I);
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// The array and index must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="value">A value to store.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI(this IEmitter emitter, int value)
        {
            return emitter
                .LdcI4(value)
                .StElemI();
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// The array must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <param name="value">A value to store.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI(this IEmitter emitter, int index, int value)
        {
            return emitter
                .LdcI4(index)
                .LdcI4(value)
                .StElemI();
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localArray">A local containing an array.</param>
        /// <param name="index">The array element index.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI(this IEmitter emitter, ILocal localArray, int index, int value)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            return emitter
                .LdLoc(localArray)
                .LdcI4(index)
                .LdcI4(value)
                .StElemI();
        }

        /// <summary>
        /// Emit IL to set an array element.
        /// The array, index, and value must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI1(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stelem_I1);
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// The array must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI1(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .StElemI1();
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localArray">A local containing an array.</param>
        /// <param name="index">The array element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI1(this IEmitter emitter, ILocal localArray, int index)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            return emitter
                .LdLoc(localArray)
                .StElemI1(index);
        }

        /// <summary>
        /// Emit IL to set an array element.
        /// The array, index, and value must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI2(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stelem_I2);
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// The array must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI2(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .StElemI2();
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localArray">A local containing an array.</param>
        /// <param name="index">The array element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI2(this IEmitter emitter, ILocal localArray, int index)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            return emitter
                .LdLoc(localArray)
                .StElemI2(index);
        }

        /// <summary>
        /// Emit IL to set an array element.
        /// The array, index, and value must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stelem_I4);
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// The array must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The array element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI4(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .StElemI4();
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localArray">A local containing an array.</param>
        /// <param name="index">The array element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI4(this IEmitter emitter, ILocal localArray, int index)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            return emitter
                .LdLoc(localArray)
                .StElemI4(index);
        }

        /// <summary>
        /// Emit IL to set an array element.
        /// The array, index, and value must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI8(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stelem_I8);
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// The array must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The array element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI8(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .StElemI8();
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localArray">A local containing an array.</param>
        /// <param name="index">The array element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemI8(this IEmitter emitter, ILocal localArray, int index)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            return emitter
                .LdLoc(localArray)
                .StElemI8(index);
        }

        /// <summary>
        /// Emit IL to set an array element.
        /// The array, index, and value must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemR4(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stelem_R4);
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// The array must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The array element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemR4(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .StElemR4();
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localArray">A local containing an array.</param>
        /// <param name="index">The array element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemR4(this IEmitter emitter, ILocal localArray, int index)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            return emitter
                .LdLoc(localArray)
                .StElemR4(index);
        }

        /// <summary>
        /// Emit IL to set an array element.
        /// The array, index, and value must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemR8(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stelem_R8);
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// The array must have previously been pushed onto the evaluation stack.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="index">The array element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemR8(this IEmitter emitter, int index)
        {
            return emitter
                .LdcI4(index)
                .StElemR8();
        }

        /// <summary>
        /// Emit IL to set an array element to a given value.
        /// </summary>
        /// <param name="emitter">A <see cref="IEmitter"/> instance.</param>
        /// <param name="localArray">A local containing an array.</param>
        /// <param name="index">The array element index.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemR8(this IEmitter emitter, ILocal localArray, int index)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            return emitter
                .LdLoc(localArray)
                .StElemR8(index);
        }

        /// <summary>
        /// Replaces the array element at a given index with the object ref value (type O) on the evaluation stack.
        /// </summary>
        /// <param name="emitter">An <see cref="IEmitter"/>.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter StElemRef(this IEmitter emitter)
        {
            return emitter.Emit(OpCodes.Stelem_Ref);
        }

        /// <summary>
        /// Emits the IL to allocate and fill an array.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="localArray">The local to store the array in.</param>
        /// <param name="length">The size of the array.</param>
        /// <param name="action">The action to execute for each index in the array.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter Array(this IEmitter emitter, ILocal localArray, int length, Action<int> action)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            ArrayBuilder arrayBuilder = new ArrayBuilder(emitter, localArray.LocalType, length, localArray);
            for (int i = 0; i < length; i++)
            {
                arrayBuilder.Set(i, action);
            }

            return emitter;
        }

        /// <summary>
        /// Emits the IL to allocate and fill an array.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="arrayType">The <see cref="Type"/> to array to emit.</param>
        /// <param name="localArray">The local to store the array in.</param>
        /// <param name="length">The size of the array.</param>
        /// <param name="action">The action to execute for each index in the array.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter Array(this IEmitter emitter, Type arrayType, ILocal localArray, int length, Action<int> action)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            ArrayBuilder arrayBuilder = new ArrayBuilder(emitter, arrayType, length, localArray);
            for (int i = 0; i < length; i++)
            {
                arrayBuilder.Set(i, action);
            }

            return emitter;
        }

        /// <summary>
        /// Emits the IL to allocate and fill an array.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="localArray">The local to store the array in.</param>
        /// <param name="localTypes">The local variables to add to the array.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter TypeArray(this IEmitter emitter, ILocal localArray, params ILocal[] localTypes)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            ArrayBuilder arrayBuilder = new ArrayBuilder(emitter, typeof(Type), localTypes.Length, localArray);
            for (int i = 0; i < localTypes.Length; i++)
            {
                arrayBuilder.Set(i, () => emitter.LdLoc(localTypes[i]));
            }

            return emitter;
        }

        /// <summary>
        /// Emits the IL to allocate and fill an array.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="localArray">The local to store the array in.</param>
        /// <param name="types">The types to add to the array.</param>
        /// <returns>The <see cref="IEmitter"/>.</returns>
        public static IEmitter TypeArray(this IEmitter emitter, ILocal localArray, params Type[] types)
        {
            if (localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }

            ArrayBuilder arrayBuilder = new ArrayBuilder(emitter, typeof(Type), types.Length, localArray);
            for (int i = 0; i < types.Length; i++)
            {
                arrayBuilder.Set(i, () => emitter.EmitTypeOf(types[i]));
            }

            return emitter;
        }
    }
}