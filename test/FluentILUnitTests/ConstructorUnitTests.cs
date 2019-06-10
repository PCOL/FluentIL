namespace FluentILUnitTests
{
    using System;
    using System.Reflection;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Constructor Unit Tests.
    /// </summary>
    [TestClass]
    public class ConstructorUnitTests
    {
        /// <summary>
        /// New Constructor.
        /// </summary>
        [TestMethod]
        public void NewConstructor()
        {
            var typeBuilder = TypeFactory
                .Default
                .NewType(Guid.NewGuid().ToString())
                .Public();

            typeBuilder
                .NewConstructor()
                .Public()
                .Body()
                .Ret();

            var type = typeBuilder.CreateType();
            var obj = Activator.CreateInstance(type);

            Assert.IsNotNull(obj);
        }

        /// <summary>
        /// New Default Constructor.
        /// </summary>
        [TestMethod]
        public void NewDefaultConstructor()
        {
            var typeBuilder = TypeFactory
                .Default
                .NewType(Guid.NewGuid().ToString())
                .Public();

            typeBuilder
                .NewDefaultConstructor(MethodAttributes.Public);

            var type = typeBuilder.CreateType();
            var obj = Activator.CreateInstance(type);

            Assert.IsNotNull(obj);
        }
    }
}