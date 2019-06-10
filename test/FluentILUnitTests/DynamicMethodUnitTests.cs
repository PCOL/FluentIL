namespace FluentILUnitTests
{
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Dynamic Method Unit Tests.
    /// </summary>
    [TestClass]
    public class DynamicMethodUnitTests
    {
        /// <summary>
        /// Test.
        /// </summary>
        [TestMethod]
        public void Test()
        {
            var addTwoIntegers = DynamicMethodFactory.Default
                .NewDynamicMethod("AddTwoIntegers", typeof(DynamicMethodUnitTests))
                .Param<int>("arg1")
                .Param<int>("arg2")
                .Returns<int>()
                .Body(m => m
                    .LdArg0()
                    .LdArg1()
                    .Add()
                    .Ret())
                .CreateFunc<int, int, int>();

            Assert.AreEqual(2, addTwoIntegers(1, 1));
            Assert.AreEqual(4, addTwoIntegers(2, 2));
            Assert.AreEqual(7, addTwoIntegers(3, 4));
        }
    }
}