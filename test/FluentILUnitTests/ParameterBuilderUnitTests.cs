namespace FluentILUnitTests
{
    using System;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParameterBuilderUnittests
    {
        [TestMethod]
        public void CreateMethod_WithParametersUsingParams()
        {
            const string Format = "arg1 = {0}, arg2 = {1}";

            var formatMethodInfo = typeof(string).GetMethod("Format", new[] { typeof(string), typeof(object), typeof(object) });

            var typeBuilder = TypeFactory
                .Default
                .NewType(Guid.NewGuid().ToString())
                .Public();

            var methodBuilder = typeBuilder
                .NewMethod("TestMethod")
                .Param<string>("arg1")
                .Param<int>("arg2")
                .Returns<string>()
                .Public()
                .Body(e => e
                    .LdStr(Format)
                    .LdArg1()
                    .LdArg2()
                    .Box<int>()
                    .Call(formatMethodInfo)
                    .Ret());
                
            var type = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var method = instance.GetMethodFunc<string, int, string>("TestMethod");

            var arg1 = "Hello";
            var arg2 = 100;

            Assert.AreEqual(string.Format(Format, arg1, arg2), method(arg1, arg2));
        }

        [TestMethod]
        public void CreateMethod_WithParametersUsingParameterBuilders()
        {
            const string Format = "arg1 = {0}, arg2 = {1}";

            var formatMethodInfo = typeof(string).GetMethod("Format", new[] { typeof(string), typeof(object), typeof(object) });

            var typeBuilder = TypeFactory
                .Default
                .NewType(Guid.NewGuid().ToString())
                .Public();

            var methodBuilder = typeBuilder
                .NewMethod("TestMethod")
                .Returns<string>()
                .Public();

            var arg1Parm = methodBuilder.CreateParam<string>("arg1");
            var arg2Parm = methodBuilder.CreateParam<int>("arg2");

            methodBuilder
                .Params(arg1Parm, arg2Parm)
                .Body(e => e
                    .LdStr(Format)
                    .LdArg1()
                    .LdArg2()
                    .Box<int>()
                    .Call(formatMethodInfo)
                    .Ret());
                
            var type = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var method = instance.GetMethodFunc<string, int, string>("TestMethod");

            var arg1 = "Hello";
            var arg2 = 100;

            Assert.AreEqual(string.Format(Format, arg1, arg2), method(arg1, arg2));
        }

        [TestMethod]
        public void CreateMethod_WithParametersUsingParamActions()
        {
            const string Format = "arg1 = {0}, arg2 = {1}";

            var formatMethodInfo = typeof(string).GetMethod("Format", new[] { typeof(string), typeof(object), typeof(object) });

            var typeBuilder = TypeFactory
                .Default
                .NewType(Guid.NewGuid().ToString())
                .Public();

            var methodBuilder = typeBuilder
                .NewMethod("TestMethod")
                .Param(p => p.Name("arg1").Type<string>())
                .Param(p => p.Name("arg2").Type<int>())
                .Returns<string>()
                .Public()
                .Body(e => e
                    .LdStr(Format)
                    .LdArg1()
                    .LdArg2()
                    .Box<int>()
                    .Call(formatMethodInfo)
                    .Ret());
                
            var type = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var method = instance.GetMethodFunc<string, int, string>("TestMethod");

            var arg1 = "Hello";
            var arg2 = 100;

            Assert.AreEqual(string.Format(Format, arg1, arg2), method(arg1, arg2));
        }
    }
}