namespace FluentILUnitTests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TypePropertyUnitTests
    {
        [TestMethod]
        public void CreateProperty_WithStringField_GetValueMatchesSetValue()
        {
            //DebugOutput.Output = new ConsoleOutput();

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

        [TestMethod]
        public void Test()
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
                    .Public()
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

            setValue.Define().Invoke(obj, new object[] { "Test" });

            var value = (string)property.Define().GetGetMethod().Invoke(obj, null);

            Assert.AreEqual("Test", value);
        }
    }
}