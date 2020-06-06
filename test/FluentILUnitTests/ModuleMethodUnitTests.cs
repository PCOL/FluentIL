namespace FluentILUnitTests
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Module Method Unit Tests.
    /// </summary>
    [TestClass]
    public class ModuleMethodUnitTests
    {
        /// <summary>
        /// Create Global Method With Parameters And Return.
        /// </summary>
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
                    .Ret());

            addMethodBuilder.Define();

            TypeFactory
                .Default
                .CreateGlobalFunctions();

            var methodInfo = TypeFactory
                .Default
                .GetMethod("Add");

            var method = (Func<int, int, int>)methodInfo.CreateDelegate(typeof(Func<int, int, int>));
            var result = method(10, 20);

            Assert.AreEqual(30, (int)result);
        }
    }
}