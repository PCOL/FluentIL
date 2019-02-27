namespace FluentIL.Expressions
{
    using System;
    using System.Reflection;
    using FluentIL;

    /// <summary>
    /// Defines the expressiom interface.
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// Loads an argument onto the top of the evaluation stack.
        /// </summary>
        /// <param name="index">The argument index.</param>
        /// <returns>The expression.</returns>
        IExpression LdArg(int index);

        /// <summary>
        /// Loads an argument onto the top of the evaluation stack (short form).
        /// </summary>
        /// <param name="index">The argument index.</param>
        /// <returns>The expression.</returns>
        IExpression LdArgS(int index);

        /// <summary>
        /// Loads the first argument onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The expression.</returns>
        IExpression LdArg0();

        /// <summary>
        /// Loads the second argument onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The expression.</returns>
        IExpression LdArg1();

        /// <summary>
        /// Loads the third argument onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The expression.</returns>
        IExpression LdArg2();

        /// <summary>
        /// Loads the fourth argument onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The expression.</returns>
        IExpression LdArg3();

        /// <summary>
        /// Loads an argument onto the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <param name="index">The argument index.</param>
        /// <returns>The argument value.</returns>
        T LdArg<T>(int index);

        /// <summary>
        /// Loads an argument onto the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <param name="index">The argument index.</param>
        /// <returns>The argument value.</returns>
        T LdArgS<T>(int index);

        /// <summary>
        /// Loads the first argument onto the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <returns>The argument value.</returns>
        T LdArg0<T>();

        /// <summary>
        /// Loads the second argument onto the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <returns>The argument value.</returns>
        T LdArg1<T>();

        /// <summary>
        /// Loads the third argument onto the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <returns>The argument value.</returns>
        T LdArg2<T>();

        /// <summary>
        /// Loads the fourth argument onto the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <returns>The argument value.</returns>
        T LdArg3<T>();

        /// <summary>
        /// Loads a local value onto the evaluaton stack.
        /// </summary>
        /// <param name="local">The local to load.</param>
        /// <returns>The expression.</returns>
        IExpression LdLoc(ILocal local);

        /// <summary>
        /// Loads a local value onto the evaluaton stack.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="local">The local to load.</param>
        /// <returns>The local value.</returns>
        T LdLoc<T>(ILocal local);
        
        /// <summary>
        /// Loads a local value onto the evaluaton stack.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="local">The local to load.</param>
        /// <returns>The local value.</returns>
        T LdLocS<T>(ILocal local);

        /// <summary>
        /// Loads the value of the first local onto the evaluaton stack.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <returns>The local value.</returns>
        T LdLoc0<T>();

        /// <summary>
        /// Loads the value of the second local onto the evaluaton stack.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <returns>The local value.</returns>
        T LdLoc1<T>();

        /// <summary>
        /// Loads the value of the third local onto the evaluaton stack.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <returns>The local value.</returns>
        T LdLoc2<T>();

        /// <summary>
        /// Loads the value of the fourth local onto the evaluaton stack.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <returns>The local value.</returns>
        T LdLoc3<T>();

        /// <summary>
        /// Loads a null value onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value null.</returns>
        object LdNull();

        /// <summary>
        /// Loads an integer value onto the top of the evaluation stack.
        /// </summary>
        /// <param name="value">The value to load.</param>
        /// <returns>The value.</returns>
        int LdcI4(int value);

        /// <summary>
        /// Loads the value 0 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_0();

        /// <summary>
        /// Loads the value 1 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_1();

        /// <summary>
        /// Loads the value 2 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_2();

        /// <summary>
        /// Loads the value 3 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_3();

        /// <summary>
        /// Loads the value 4 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_4();

        /// <summary>
        /// Loads the value 5 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_5();

        /// <summary>
        /// Loads the value 6 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_6();

        /// <summary>
        /// Loads the value 7 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_7();

        /// <summary>
        /// Loads the value 8 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_8();

        /// <summary>
        /// Loads the value of -1 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_M1();

        /// <summary>
        /// Loads an int value onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_S();

        /// <summary>
        /// Loads a long value onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        long LdcI8(long value);

        /// <summary>
        /// Loads a double value onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        float LdcR4(float value);

        /// <summary>
        /// Loads a double value onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        double LdcR8(double value);

        /// <summary>
        /// Loads the value of a field onto the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The fields type.</typeparam>
        /// <param name="field">The field.</param>
        /// <returns>The field value.</returns>
        T LdFld<T>(IFieldBuilder field);

        /// <summary>
        /// Loads a field.
        /// </summary>
        /// <param name="field">The field to load.</param>
        /// <returns>An expression.</returns>
        IExpression LdFld(IFieldBuilder field);

        /// <summary>
        /// Returns the value on top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <returns>The value.</returns>
        T Value<T>();

        /// <summary>
        /// Returns the value on top of the evaluation stack.
        /// </summary>
        /// <param name="type">The value type.</param>
        /// <returns>The value.</returns>
        object Value(Type type);

        /// <summary>
        /// Calls a method.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="methodInfo">The method to call.</param>
        /// <returns>The result.</returns>
        TResult Call<TResult>(MethodInfo methodInfo);
    }
}