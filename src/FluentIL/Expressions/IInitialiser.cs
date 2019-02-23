namespace FluentIL.Expressions
{
    /// <summary>
    /// Defines the intialiser interface.
    /// </summary>
    public interface IInitialiser
    {
        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <typeparam name="T">The locals type.</typeparam>
        /// <param name="local">A variable to receive a local.</param>
        /// <returns>The <see cref="IInitialiser"/> instance</returns>
        IInitialiser DeclareLocal<T>(out ILocal local);

        /// <summary>
        /// Declares a local.
        /// </summary>
        /// <typeparam name="T">The locals type.</typeparam>
        /// <param name="localName">The name of the local.</param>
        /// <param name="local">A variable to receive a local.</param>
        /// <returns>The <see cref="IInitialiser"/> instance</returns>
        IInitialiser DeclareLocal<T>(string localName, out ILocal local);

        /// <summary>
        /// Loads a local value onto the top of the evaluation stack.
        /// </summary>
        /// <param name="local">A reference to a local.</param>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdLoc(ILocal local);

        /// <summary>
        /// Stores the value on the top of the evaluation stack in the local.
        /// </summary>
        /// <param name="local">A reference to a local.</param>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser StLoc(ILocal local);

        /// <summary>
        /// Loads an <see cref="int"/> value on the top of the evaluation stack.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IInitialiser LdcI4(int value);

        /// <summary>
        /// Loads the value 0 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcI4_0();

        /// <summary>
        /// Loads the value 1 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcI4_1();

        /// <summary>
        /// Loads the value 2 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcI4_2();

        /// <summary>
        /// Loads the value 3 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcI4_3();

        /// <summary>
        /// Loads the value 4 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcI4_4();

        /// <summary>
        /// Loads the value 5 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcI4_5();

        /// <summary>
        /// Loads the value 6 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcI4_6();

        /// <summary>
        /// Loads the value 7 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcI4_7();

        /// <summary>
        /// Loads the value 8 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcI4_8();

        /// <summary>
        /// Loads the value minus one on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcI4_M1();

        /// <summary>
        /// Loads an <see cref="long"/> value on the top of the evaluation stack.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcI8(int value);

        /// <summary>
        /// Loads an <see cref="float"/> value on the top of the evaluation stack.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcR4(float value);

        /// <summary>
        /// Loads an <see cref="double"/> value on the top of the evaluation stack.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="IInitialiser"/> instance.</returns>
        IInitialiser LdcR8(double value);
    }
}