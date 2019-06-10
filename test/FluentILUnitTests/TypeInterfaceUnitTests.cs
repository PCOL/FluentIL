namespace FluentILUnitTests
{
    using System;
    using FluentIL;
    using FluentILUnitTests.Resources;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Type Interface Unit Tests.
    /// </summary>
    [TestClass]
    public class TypeInterfaceUnitTests
    {
        /// <summary>
        /// Type Implement Interface.
        /// </summary>
        [TestMethod]
        public void Type_ImplementInterface()
        {
            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}")
                .Public()
                .Implements<ITestInterface>();

            var field = testTypeBuilder
                .NewField<string>("property");

            var property = testTypeBuilder
                .NewProperty<string>("GetSetProperty")
                .Setter(m => m
                    .Public()
                    .Virtual()
                    .Body(il => il
                        .LdArg0()
                        .LdArg1()
                        .StFld(field)
                        .Ret()))
                .Getter(m => m
                    .Public()
                    .Virtual()
                    .Body(il => il
                        .LdArg0()
                        .LdFld(field)
                        .Ret()));

            var testType = testTypeBuilder.CreateType();
            Assert.IsNotNull(testType);

            var instance = Activator.CreateInstance(testType);
            Assert.IsNotNull(instance);

            ITestInterface testInterface = instance as ITestInterface;
            Assert.IsNotNull(testInterface);

            testInterface.GetSetProperty = "Test";
            Assert.AreEqual("Test", testInterface.GetSetProperty);
        }
    }
}