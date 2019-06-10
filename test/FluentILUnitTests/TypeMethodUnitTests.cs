namespace FluentILUnitTests
{
    using System;
    using System.Reflection;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Type Method Unit Tests.
    /// </summary>
    [TestClass]
    public class TypeMethodUnitTests
    {
        /// <summary>
        /// Create Method With Single String Parameter Returns Passed In String.
        /// </summary>
        [TestMethod]
        public void CreateMethod_WithSingleStringParameter_ReturnsPassedInString()
        {
            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}");

            var testMethodBuilder = testTypeBuilder
                .NewMethod<string>("TestMethod")
                .Public()
                .Param<string>("name");

            testMethodBuilder
                .Body()
                .DeclareLocal<string>(out ILocal local)
                .LdArg1()
                .StLoc(local)
                .Nop()
                .LdLoc(local)
                .Ret();

            var testType = testTypeBuilder.CreateType();

            var testMethod = testType.GetMethod("TestMethod");
            Assert.IsNotNull(testMethod);

            var instance = Activator.CreateInstance(testType);
            Assert.IsNotNull(instance);

            var result = testMethod.Invoke(instance, new object[] { "Hello World" });
            Assert.IsNotNull(result);
            Assert.AreEqual("Hello World", (string)result);
        }

        /// <summary>
        /// Create Method With Two String Parameters Returns Concatenated String.
        /// </summary>
        [TestMethod]
        public void CreateMethod_WithTwoStringParameters_ReturnsConcatenatedString()
        {
            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}");

            var testMethodBuilder = testTypeBuilder
                .NewMethod<string>("TestMethod")
                .Public()
                .Param<string>("first")
                .Param<string>("second");

            testMethodBuilder
                .Body()
                .DeclareLocal<string>(out ILocal local)
                .LdArg1()
                .LdArg2()
                .Call(typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) }))
                .StLoc(local)
                .Nop()
                .LdLoc(local)
                .Ret();

            var testType = testTypeBuilder.CreateType();

            var testMethod = testType.GetMethod("TestMethod");
            Assert.IsNotNull(testMethod);

            var instance = Activator.CreateInstance(testType);
            Assert.IsNotNull(instance);

            var result = testMethod.Invoke(instance, new object[] { "Hello", "World" });
            Assert.IsNotNull(result);
            Assert.AreEqual("HelloWorld", (string)result);
        }
    }
}