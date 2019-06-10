namespace FluentILUnitTests
{
    using System;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Type Factory Unit Tests.
    /// </summary>
    [TestClass]
    public class TypeFactoryUnitTests
    {
        /// <summary>
        /// Default Type Factory New Type Creates New Type.
        /// </summary>
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

        /// <summary>
        /// Type Factory New Type Creates New Type.
        /// </summary>
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

        /// <summary>
        /// Create Type Factory With Null Assembly Name Throws.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTypeFactory_WithNullAssemblyName_Throws()
        {
            new TypeFactory(null, "TestModule");
        }

        /// <summary>
        /// Create Type Factory With Empty Assembly Name Throws.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTypeFactory_WithEmptyAssemblyName_Throws()
        {
            new TypeFactory(string.Empty, "TestModule");
        }

        /// <summary>
        /// Create Type Factory With Whitespace Assembly Name Throws.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTypeFactory_WithWhitespaceAssemblyName_Throws()
        {
            new TypeFactory(" ", "TestModule");
        }

        /// <summary>
        /// Create Type Factory With Null Module Name Throws.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTypeFactory_WithNullModuleName_Throws()
        {
            new TypeFactory("TestAssembly", null);
        }

        /// <summary>
        /// Create Type Factory With Empty Module Name Throws.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTypeFactory_WithEmptyModuleName_Throws()
        {
            new TypeFactory("TestAssembly", string.Empty);
        }

        /// <summary>
        /// Create Type Factory With Whitespace Module Name Throws.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTypeFactory_WithWhitespaceModuleName_Throws()
        {
            new TypeFactory("TestAssembly", " ");
        }
    }
}
