namespace FluentILUnitTests
{
    using System;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GenericTypeUnitTests
    {
        [TestMethod]
        public void CreateSimpleGenericType()
        {
            DebugOutput.Output = new ConsoleOutput();

            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}")
                .NewGenericParameter("T", null)
                .Public();

            var typeT = testTypeBuilder.GetGenericParameterType("T");

            var field = testTypeBuilder
                .NewField("property", typeT);

            testTypeBuilder
                .NewMethod("SetValue")
                .Public()
                .Param(typeT, "value")
                .Body(il => il
                    .LdArg0()
                    .LdArg1()
                    .StFld(field)
                    .Ret()
                );

            testTypeBuilder
                .NewProperty("Value", typeT)
                .Getter(m => m
                    .Public()
                    .Body(il => il
                        .LdArg0()
                        .LdFld(field)
                        .Ret()
                    )
                );

            var testType = testTypeBuilder.CreateType();
            Assert.IsNotNull(testType);

            var genericTestType = testType.MakeGenericType(typeof(string));
            Assert.IsNotNull(genericTestType);

            var instance = Activator.CreateInstance(genericTestType);
            Assert.IsNotNull(instance);

            genericTestType.GetMethod("SetValue").Invoke(instance, new object[] { "Test" });
            var result = genericTestType.GetProperty("Value").GetGetMethod().Invoke(instance, null);
            Assert.AreEqual("Test", result as string);
        }
    }
}