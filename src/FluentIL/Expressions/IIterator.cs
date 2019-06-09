namespace FluentIL.Expressions
{
    /// <summary>
    /// Defines the iterator interface.
    /// </summary>
    public interface IIterator
    {
        /// <summary>
        /// Loads a local value onto the top of the evaluation stack.
        /// </summary>
        /// <param name="local">A reference to a local.</param>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdLoc(ILocal local);

        /// <summary>
        /// Loads the value from local at location 0 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdLoc0();

        /// <summary>
        /// Loads the value from local at localtion 1 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdLoc1();

        /// <summary>
        /// Loads the value from local at location 2 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdLoc2();

        /// <summary>
        /// Loads the value from local at location 3 onto the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdLoc3();

        /// <summary>
        /// Stores the value on the top of the evaluation stack in the local.
        /// </summary>
        /// <param name="local">A reference to a local.</param>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator StLoc(ILocal local);

        /// <summary>
        /// Stores the value on the top of the evaluation stack in location 0.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator StLoc0();

        /// <summary>
        /// Stores the value on the top of the evaluation stack in location 1.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator StLoc1();

        /// <summary>
        /// Stores the value on the top of the evaluation stack in location 2.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator StLoc2();

        /// <summary>
        /// Stores the value on the top of the evaluation stack in location 3.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator StLoc3();

        /// <summary>
        /// Loads an <see cref="int"/> value on the top of the evaluation stack.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI4(int value);

        /// <summary>
        /// Loads the value 0 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI4_0();

        /// <summary>
        /// Loads the value 1 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI4_1();

        /// <summary>
        /// Loads the value 2 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI4_2();

        /// <summary>
        /// Loads the value 3 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI4_3();

        /// <summary>
        /// Loads the value 4 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI4_4();

        /// <summary>
        /// Loads the value 5 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI4_5();

        /// <summary>
        /// Loads the value 6 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI4_6();

        /// <summary>
        /// Loads the value 7 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI4_7();

        /// <summary>
        /// Loads the value 8 on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI4_8();

        /// <summary>
        /// Loads the value minus one on the top of the evaluation stack.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI4_M1();

        /// <summary>
        /// Loads an <see cref="long"/> value on the top of the evaluation stack.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcI8(int value);

        /// <summary>
        /// Loads an <see cref="float"/> value on the top of the evaluation stack.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcR4(float value);

        /// <summary>
        /// Loads an <see cref="double"/> value on the top of the evaluation stack.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator LdcR8(double value);

        /// <summary>
        /// Adds together the two values on the top of the evaluation stack and
        /// replaces them with the result.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator Add();

        /// <summary>
        /// Adds together the two values on the top of the evaluation stack and
        /// replaces them with the result.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator AddOvf();

        /// <summary>
        /// Adds together the two values on the top of the evaluation stack and
        /// replaces them with the result.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator AddOvfUn();

        /// <summary>
        /// Subtracts the two values on the top of the evaluation stack and
        /// replaces them with the result.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator Sub();

        /// <summary>
        /// Subtracts the two values on the top of the evaluation stack and
        /// replaces them with the result.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator SubOvf();

        /// <summary>
        /// Subtracts the two values on the top of the evaluation stack and
        /// replaces them with the result.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator SubOvfUn();

        /// <summary>
        /// Divides the two values on the top of the evaluation stack and
        /// replaces them with the result.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator Div();

        /// <summary>
        /// Divides the two values on the top of the evaluation stack and
        /// replaces them with the result.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator DivUn();

        /// <summary>
        /// Multilies the two values on the top of the evaluation stack and
        /// replaces them with the result.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator Mul();

        /// <summary>
        /// Multilies the two values on the top of the evaluation stack and
        /// replaces them with the result.
        /// </summary>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator MulUn();

        /// <summary>
        /// Increments the contents of a local.
        /// </summary>
        /// <param name="local">The local to increment.</param>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator Inc(ILocal local);

        /// <summary>
        /// Decrements the contents of a local.
        /// </summary>
        /// <param name="local">The local to increment.</param>
        /// <returns>The <see cref="IIterator"/> instance.</returns>
        IIterator Dec(ILocal local);
    }
}