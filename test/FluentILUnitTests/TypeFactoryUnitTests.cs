namespace FluentILUnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentIL;
    using System;

    [TestClass]
    public class TypeFactoryUnitTests
    {
        [TestMethod]
        public void DefaultTypeFactory_NewType_CreatesNewType()
        {
            var testTypeBuilder = TypeFactory
                .Default
                .NewType();

            Assert.IsNotNull(testTypeBuilder);

            var testType = testTypeBuilder.CreateType();

            Assert.IsNotNull(testType);

            var instance = Activator.CreateInstance(testType);

            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void TypeFactory_NewType_CreatesNewType()
        {
            var typeFactory = new TypeFactory("TestAssembly", "TestModule");
            var testTypeBuilder = typeFactory
                .NewType();

            Assert.IsNotNull(testTypeBuilder);

            var testType = testTypeBuilder.CreateType();

            Assert.IsNotNull(testType);

            var instance = Activator.CreateInstance(testType);

            Assert.IsNotNull(instance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTypeFactory_WithNullAssemblyName_Throws()
        {
            new TypeFactory(null, "TestModule");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTypeFactory_WithEmptyAssemblyName_Throws()
        {
            new TypeFactory(string.Empty, "TestModule");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTypeFactory_WithWhitespaceAssemblyName_Throws()
        {
            new TypeFactory(" ", "TestModule");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTypeFactory_WithNullModuleName_Throws()
        {
            new TypeFactory("TestAssembly", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTypeFactory_WithEmptyModuleName_Throws()
        {
            new TypeFactory("TestAssembly", string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTypeFactory_WithWhitespaceModuleName_Throws()
        {
            new TypeFactory("TestAssembly", " ");
        }
    }
}
