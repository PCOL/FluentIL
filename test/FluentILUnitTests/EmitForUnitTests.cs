namespace FluentILUnitTests
{
    using System;
    using System.Collections.Generic;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Emit For Unit Tests.
    /// </summary>
    [TestClass]
    public class EmitForUnitTests
    {
        /// <summary>
        /// Create Method With Index For Loop.
        /// </summary>
        [TestMethod]
        public void CreateMethod_WithIndexForLoop()
        {
            var addItem = typeof(List<string>)
                .BuildMethodInfo("Add")
                .HasParameterTypes(typeof(string))
                .FirstOrDefault();

            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}");

            testTypeBuilder
                .NewMethod("TestMethod")
                .Public()
                .Param<string[]>("source")
                .Param<List<string>>("list")
                .Body()
                    .DeclareLocal<string[]>(out ILocal localArray)
                    .DeclareLocal<List<string>>(out ILocal localList)
                    .DeclareLocal<int>("length", out ILocal length)

                    .LdArg1()
                    .StLoc(localArray)
                    .LdArg2()
                    .StLoc(localList)
                    .Nop()
                    .LdLoc(localArray)
                    .LdLen()
                    .StLoc(length)
                    .Nop()
                    .For(
                        length,
                        (il, index) =>
                        {
                            il
                            .LdLoc(localList)
                            .LdLoc(localArray)
                            .LdLoc(index)
                            .LdElemRef()
                            .CallVirt(addItem);
                        })
                    .Ret();

            var testType = testTypeBuilder.CreateType();
            var testMethod = testType.GetMethod("TestMethod");

            var list = new List<string>();
            var source = new[] { "A", "B", "C" };

            var obj = Activator.CreateInstance(testType);
            testMethod.Invoke(obj, new object[] { source, list });

            Assert.AreEqual(source.Length, list.Count);
            for (int i = 0; i < source.Length; i++)
            {
                Assert.AreEqual(source[i], list[i]);
            }
        }

        /// <summary>
        /// Create Method With Index And Array For Loop.
        /// </summary>
        [TestMethod]
        public void CreateMethod_WithIndexAndArrayForLoop()
        {
            var addItem = typeof(List<string>)
                .BuildMethodInfo("Add")
                .HasParameterTypes(typeof(string))
                .FirstOrDefault();

            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}");

            testTypeBuilder
                .NewMethod("TestMethod")
                .Public()
                .Param<string[]>("source")
                .Param<List<string>>("list")
                .Body()
                    .DeclareLocal<string[]>(out ILocal localArray)
                    .DeclareLocal<List<string>>(out ILocal localList)

                    .LdArg1()
                    .StLoc(localArray)
                    .LdArg2()
                    .StLoc(localList)
                    .Nop()
                    .For(
                        localArray,
                        (il, index, item) =>
                        {
                            il
                            .LdLoc(localList)
                            .LdLoc(item)
                            .CallVirt(addItem);
                        })
                    .Ret();

            var testType = testTypeBuilder.CreateType();
            var testMethod = testType.GetMethod("TestMethod");

            var list = new List<string>();
            var source = new[] { "A", "B", "C" };

            var obj = Activator.CreateInstance(testType);
            testMethod.Invoke(obj, new object[] { source, list });

            Assert.AreEqual(source.Length, list.Count);
            for (int i = 0; i < source.Length; i++)
            {
                Assert.AreEqual(source[i], list[i]);
            }
        }
    }
}