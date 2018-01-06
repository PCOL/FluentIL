namespace FluentILUnitTests
{
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ModuleMethodUnitTests
    {
        [TestMethod]
        public void CreateGlobalMethod_WithParametersAndReturn()
        {
            var addMethodBuilder = TypeFactory
                .Default
                .NewGlobalMethod("Add")
                .Public()
                .Static()
                .Param<int>("first")
                .Param<int>("second")
                .Returns<int>()
                .Body(il => il
                    .LdArg0()
                    .LdArg1()
                    .Add()
                    .Ret()
                );

            TypeFactory
                .Default
                .CreateGlobalFunctions();

            var method = TypeFactory
                .Default
                .GetMethod("Add");

            var result = method.Invoke(null, new object[] { 10, 20 } );

            Assert.AreEqual(30, (int)result);
        }
    }
}