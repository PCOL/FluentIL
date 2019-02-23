namespace FluentIL.Expressions
{
    /// <summary>
    /// Defines the condition interface.
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Loads a local value onto the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The local type.</typeparam>
        /// <param name="local">A reference to a local.</param>
        /// <returns>The local value.</returns>
        T LdLoc<T>(ILocal local);

        /// <summary>
        /// Loads the value from local at position 0 onto the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The local type.</typeparam>
        /// <returns>The local value.</returns>l
        T LdLoc0<T>();

        /// <summary>
        /// Loads the value from local at position 1 onto the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The local type.</typeparam>
        /// <returns>The local value.</returns>
        T LdLoc1<T>();

        /// <summary>
        /// Loads the value from local at position 2 onto the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The local type.</typeparam>
        /// <returns>The local value.</returns>
        T LdLoc2<T>();

        /// <summary>
        /// Loads the value from local at position 3 onto the top of the evaluation stack.
        /// </summary>
        /// <typeparam name="T">The local type.</typeparam>
        /// <returns>The local value.</returns>
        T LdLoc3<T>();
        
        /// <summary>
        /// Loads an integer value onto the top of the evaluation stack.
        /// </summary>
        /// <param name="value">The value to load.</param>
        /// <returns>The value.</returns>
        int LdcI4(int value);

        /// <summary>
        /// Loads an integer value 0 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_0();

        /// <summary>
        /// Loads an integer value 1 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_1();

        /// <summary>
        /// Loads an integer value 2 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_2();

        /// <summary>
        /// Loads an integer value 3 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_3();

        /// <summary>
        /// Loads an integer value 4 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_4();

        /// <summary>
        /// Loads an integer value 5 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_5();

        /// <summary>
        /// Loads an integer value 6 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_6();

        /// <summary>
        /// Loads an integer value 7 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_7();

        /// <summary>
        /// Loads an integer value 8 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The value.</returns>
        int LdcI4_8();
    }
}