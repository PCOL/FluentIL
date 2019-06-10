namespace FluentILUnitTests
{
    using System;
    using System.Collections.Generic;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Emit For Each Unit Tests.
    /// </summary>
    [TestClass]
    public class EmitForEachUnitTests
    {
        /// <summary>
        /// Create Method With For Each.
        /// </summary>
        [TestMethod]
        public void CreateMethod_WithForEach()
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
                .Param<IEnumerable<string>>("source")
                .Param<List<string>>("list")
                .Body()
                    .DeclareLocal<IEnumerable<string>>(out ILocal localSource)
                    .DeclareLocal<List<string>>(out ILocal localList)
                    .LdArg1()
                    .StLoc(localSource)
                    .LdArg2()
                    .StLoc(localList)
                    .Nop()
                    .ForEach(
                        localSource,
                        (il, item) =>
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