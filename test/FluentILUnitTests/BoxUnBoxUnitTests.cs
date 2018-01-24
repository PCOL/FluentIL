using System;
using System.Reflection.Emit;
using FluentIL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentILUnitTests
{
    [TestClass]
    public class BoxUnBoxUnitTests
    {
        [TestMethod]
        public void BoxValueType()
        {
            var typeBuilder = TypeFactory
                .Default
                .NewType("BoxTest")
                .Public();

            typeBuilder
                .NewMethod("IntToString")
                .Public()
                .Param<int>()
                .Returns<string>()
                .Body()
                .LdArg1()
                .Box(typeof(int))
                .CallVirt(typeof(object).GetMethod("ToString"))
                .Ret();

            var type = typeBuilder.CreateType();
            var obj = Activator.CreateInstance(type);
            var intToString = type.GetMethod("IntToString", new[] { typeof(int) });

            var value = intToString.Invoke(obj, new object[] { 10 });
            Assert.AreEqual("10", value);
        }

        [TestMethod]
        public void UnboxValueType()
        {
            var typeBuilder = TypeFactory
                .Default
                .NewType("UnboxTest")
                .Public();

            typeBuilder
                .NewMethod("ObjectToInt")
                .Public()
                .Param<object>()
                .Returns<int>()
                .Body()
                .DeclareLocal<int>(out ILocal intValue)
                .LdArg1()
                .Unbox(typeof(int))
                .LdIndI4()
                .Ret();

            var type = typeBuilder.CreateType();
            var obj = Activator.CreateInstance(type);
            var objectToInt = type.GetMethod("ObjectToInt", new[] { typeof(object) });

            var boxedInt = (object)10;
            var value = objectToInt.Invoke(obj, new object[] { boxedInt });
            Assert.AreEqual(10, value);
        }
    }
}