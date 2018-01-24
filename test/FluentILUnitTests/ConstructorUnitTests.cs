namespace FluentILUnitTests
{
    using System;
    using System.Reflection;
    using FluentIL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConstructorUnitTests
    {
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