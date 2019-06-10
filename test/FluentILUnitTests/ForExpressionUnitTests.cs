namespace FluentILUnitTests
{
    using System;
    using System.Reflection;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// For Expression Unit Tests.
    /// </summary>
    [TestClass]
    public class ForExpressionUnitTests
    {
        /// <summary>
        /// Test 2 delegate.
        /// </summary>
        /// <param name="start">The start value.</param>
        /// <param name="increment">The increment value.</param>
        /// <param name="counter">The counter value.</param>
        /// <returns>The incremented value.</returns>
        public delegate int Test2Delegate(int start, int increment, out int counter);

        /// <summary>
        /// Test 3 delegate.
        /// </summary>
        /// <param name="start">The start value.</param>
        /// <param name="increment">The increment value.</param>
        /// <param name="counter">The counter value.</param>
        /// <returns>The incremented value.</returns>
        public delegate int Test3Delegate(int start, int increment, ref int counter);

        /// <summary>
        /// Test 1.
        /// </summary>
        /// <param name="start">The start value.</param>
        /// <param name="increment">The increment value.</param>
        /// <param name="expectedResult">The expected result.</param>
        [TestMethod]
        [DataRow(0, 10, 100)]
        [DataRow(0, 100, 1000)]
        [DataRow(100, 100, 1100)]
        [DataRow(1000, -100, 0)]
        [DataRow(1100, -100, 100)]
        public void Test(int start, int increment, int expectedResult)
        {
            var methodName = $"Method_{Guid.NewGuid()}";

            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}")
                .Public();

            testTypeBuilder
                .NewMethod(methodName)
                .Param<int>("start")
                .Param<int>("increment")
                .Returns<int>()
                .Public()
                .Body(il => il
                    .DeclareLocal<int>(out ILocal result)
                    .DeclareLocal<int>(out ILocal counter)
                    .LdArg1()
                    .StLoc0()
                    .Nop()
                    .For(i => i.LdcI4_0().StLoc(counter),
                        e => e.LdLoc<int>(counter) < 10,
                        i => i.Inc(counter),
                        i => i
                            .LdLoc0()
                            .LdArg2()
                            .Add()
                            .StLoc0())
                    .LdLoc0()
                    .Ret());

            var type = testTypeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var method = instance.GetMethodFunc<int, int, int>(methodName);

            var res = method(start, increment);
            Assert.AreEqual(expectedResult, res);
        }

        /// <summary>
        /// Test 2.
        /// </summary>
        /// <param name="start">The start value.</param>
        /// <param name="increment">The increment value.</param>
        /// <param name="expectedResult">The expected result.</param>
        [TestMethod]
        [DataRow(0, 10, 100)]
        public void Test2(int start, int increment, int expectedResult)
        {
            DebugOutput.Output = new ConsoleOutput();

            var methodName = $"Method_{Guid.NewGuid()}";

            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}")
                .Public();

            testTypeBuilder
                .NewMethod(methodName)
                .Param<int>("start")
                .Param<int>("increment")
                .OutParam<int>("counter")
                .Returns<int>()
                .Public()
                .Body(il => il
                    .DeclareLocal<int>(out ILocal result)
                    .DeclareLocal<int>(out ILocal counter)
                    .LdArg1()
                    .StLoc1()
                    .Nop()
                    .For(i => i.LdcI4_0().StLoc(counter),
                        e => e.LdLoc<int>(counter) < 10,
                        i => i.Inc(counter),
                        i => i
                            .LdLoc0()
                            .LdArg2()
                            .Add()
                            .StLoc0())
                    .LdArg3()
                    .LdLoc1()
                    .StIndI4()
                    .LdLoc0()
                    .Ret());

            var type = testTypeBuilder.CreateType();
            var methodInfo = type.GetMethod(methodName, new[] { typeof(int), typeof(int), typeof(int).MakeByRefType() });
            var instance = Activator.CreateInstance(type);
            var method = (Test2Delegate)Delegate.CreateDelegate(typeof(Test2Delegate), instance, methodInfo);

            var res = method(start, increment, out int ctr);
            Assert.AreEqual(expectedResult, res);
            Assert.AreEqual(10, ctr);
        }

        /// <summary>
        /// Test 3.
        /// </summary>
        /// <param name="start">The start value.</param>
        /// <param name="increment">The increment value.</param>
        /// <param name="ctr">The counter value.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <param name="expectedCounter">The expected counter result.</param>
        [TestMethod]
        [DataRow(0, 10, 0, 100, 100)]
        [DataRow(0, 10, 100, 100, 200)]
        public void Test3(int start, int increment, int ctr, int expectedResult, int expectedCounter)
        {
            DebugOutput.Output = new ConsoleOutput();

            var methodName = $"Method_{Guid.NewGuid()}";

            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}")
                .Public();

            testTypeBuilder
                .NewMethod(methodName)
                .Param<int>("start")
                .Param<int>("increment")
                .RefParam<int>("counter")
                .Returns<int>()
                .Public()
                .Body(il => il
                    .DeclareLocal<int>(out ILocal result)
                    .DeclareLocal<int>(out ILocal counter)
                    .LdArg1()
                    .StLoc1()
                    .Nop()
                    .For(i => i.LdcI4_0().StLoc(counter),
                        e => e.LdLoc<int>(counter) < 10,
                        i => i.Inc(counter),
                        i => i
                            .LdLoc0()
                            .LdArg2()
                            .Add()
                            .StLoc0()
                            .LdArg3()
                            .Dup()
                            .LdIndI4()
                            .LdArg2()
                            .Add()
                            .StIndI4())
                    .LdLoc0()
                    .Ret());

            var type = testTypeBuilder.CreateType();
            var methodInfo = type.GetMethod(methodName, new[] { typeof(int), typeof(int), typeof(int).MakeByRefType() });
            var instance = Activator.CreateInstance(type);
            var method = (Test3Delegate)Delegate.CreateDelegate(typeof(Test3Delegate), instance, methodInfo);

            var res = method(start, increment, ref ctr);
            Assert.AreEqual(expectedResult, res);
            Assert.AreEqual(expectedCounter, ctr);
        }
    }
}