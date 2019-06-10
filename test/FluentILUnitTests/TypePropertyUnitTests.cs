namespace FluentILUnitTests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Type Property Unit Tests.
    /// </summary>
    [TestClass]
    public class TypePropertyUnitTests
    {
        /// <summary>
        /// Create Property With String Field Get Value Matches Set Value.
        /// </summary>
        [TestMethod]
        public void CreateProperty_WithStringField_GetValueMatchesSetValue()
        {
            ////DebugOutput.Output = new ConsoleOutput();

            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}")
                .Public();

            var fieldBuilder = testTypeBuilder
                .NewField<string>("property")
                .Private();

            var testPropertyBuilder = testTypeBuilder
                .NewProperty<string>("Value");

            testPropertyBuilder
                .Getter()
                .Public()
                .NewSlot()
                .Body()
                .LdArg0()
                .LdFld(fieldBuilder)
                .Ret();

            testPropertyBuilder
                .Setter()
                .Public()
                .NewSlot()
                .Body()
                .LdArg0()
                .LdArg1()
                .StFld(fieldBuilder)
                .Ret();

            var testType = testTypeBuilder.CreateType();
            Assert.IsNotNull(testType);

            var testProperty = testType.GetProperty("Value");
            Assert.IsNotNull(testProperty);

            var instance = Activator.CreateInstance(testType);
            Assert.IsNotNull(instance);

            testProperty.SetValue(instance, "Hello World");

            var result = testProperty.GetValue(instance);

            Assert.AreEqual("Hello World", (string)result);
        }

        /// <summary>
        /// Set Property With Private Setter Using A Set Method.
        /// </summary>
        [TestMethod]
        public void SetPropertyWithPrivateSetter_UsingASetMethod()
        {
            var typeBuilder = TypeFactory
                .Default
                .NewType()
                .Public();

            var field = typeBuilder
                .NewField<string>("value")
                .Private();

            var property = typeBuilder
                .NewProperty<string>("Value")
                .Setter(m => m
                    .Private()
                    .Body()
                    .LdArg0()
                    .LdArg1()
                    .StFld(field)
                    .Ret())
                .Getter(m => m
                    .Public()
                    .Body()
                    .LdArg0()
                    .LdFld(field)
                    .Ret());

            var setValue = typeBuilder
                .NewMethod("SetValue")
                .Public()
                .Param<string>("value")
                .Body(m => m
                    .LdArg0()
                    .LdArg1()
                    .Call(property.SetMethod)
                    .Ret());

            var type = typeBuilder.CreateType();
            var obj = Activator.CreateInstance(type);

            type.GetMethod("SetValue").Invoke(obj, new object[] { "Test" });
            var value = type.GetProperty("Value").GetValue(obj);

            Assert.AreEqual("Test", value);
        }
    }
}