namespace FluentILUnitTests
{
    using System;
    using System.Reflection;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ForExpressionUnitTests
    {
        public delegate int MethodDelegate(int start, int increment, out int counter);

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
                    .Ret()
                );

            var type = testTypeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var method = instance.GetMethodFunc<int, int, int>(methodName);

            var res = method(start, increment);
            Assert.AreEqual(expectedResult, res);
        }

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
                .Param(Type.GetType("System.Int32&"), "counter", ParameterAttributes.Out)
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
                    .Ret()
                );

            var type = testTypeBuilder.CreateType();
            var methodInfo = type.GetMethod(methodName, new[] { typeof(int), typeof(int), Type.GetType("System.Int32&") });
            var instance = Activator.CreateInstance(type);
            var method = (MethodDelegate)Delegate.CreateDelegate(typeof(MethodDelegate), instance, methodInfo);

            var res = method(start, increment, out int ctr);
            Assert.AreEqual(expectedResult, res);
            Assert.AreEqual(10, ctr);
        }
    }
}