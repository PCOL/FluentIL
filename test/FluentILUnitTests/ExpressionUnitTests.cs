namespace FluentILUnitTests
{
    using System;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpressionUnitTests
    {
        [TestMethod]
        public void EmitIfWithSimpleEqualsExpression()
        {
            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}")
                .Public();

            testTypeBuilder
                .NewMethod("SimpleIfEquals")
                .Param<int>("arg1")
                .Param<int>("arg2")
                .Returns<bool>()
                .Public()
                .Body(il => il
                    .DeclareLocal<bool>(out ILocal result)
                    .LdcI4_0()
                    .StLoc0()
                    .Nop()
                    .If(e => e.LdArg1<int>() == e.LdArg2<int>(),
                        i => i
                            .LdcI4_1()
                            .StLoc0())
                    .LdLoc0()
                    .Ret()
                );

            var type = testTypeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var method = instance.GetMethodFunc<int, int, bool>("SimpleIfEquals");

            Assert.IsTrue(method(10, 10));
        }
    }
}