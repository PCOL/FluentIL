namespace FluentILUnitTests
{
    using System;
    using System.Linq.Expressions;
    using FluentIL;
    using FluentIL.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// If Expression Unit Tests.
    /// </summary>
    [TestClass]
    public class IfExpressionUnitTests
    {
        /// <summary>
        /// Emit If With Simple Equals Constant Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleEqualsConstantExpression()
        {
            var method = this.CreateType<int, bool>(
                e => e.LdArg1<int>() == 10);

            Assert.IsTrue(method(10));
            Assert.IsFalse(method(1));
            Assert.IsFalse(method(-1));
        }

        /// <summary>
        /// Emit If With Simple Not Equals Constant Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleNotEqualsConstantExpression()
        {
            var method = this.CreateType<int, bool>(
                e => e.LdArg1<int>() != 10);

            Assert.IsFalse(method(10));
            Assert.IsTrue(method(1));
            Assert.IsTrue(method(-2));
        }

        /// <summary>
        /// Emit If With Simple Greater Than Constant Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleGreaterThanConstantExpression()
        {
            var method = this.CreateType<int, bool>(
                e => e.LdArg1<int>() > 10);

            Assert.IsTrue(method(20));
            Assert.IsFalse(method(10));
            Assert.IsFalse(method(-10));
        }

        /// <summary>
        /// Emit If With Simple Greater Than Or Equal Constant Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleGreaterThanOrEqualConstantExpression()
        {
            var method = this.CreateType<int, bool>(
                e => e.LdArg1<int>() >= 10);

            Assert.IsTrue(method(20));
            Assert.IsTrue(method(10));
            Assert.IsFalse(method(1));
        }

        /// <summary>
        /// Emit If With Simple Less Than Constant Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleLessThanConstantExpression()
        {
            var method = this.CreateType<int, bool>(
                e => e.LdArg1<int>() < 10);

            Assert.IsTrue(method(1));
            Assert.IsFalse(method(10));
            Assert.IsFalse(method(20));
        }

        /// <summary>
        /// Emit If With Simple Less Than Or Equal Constant Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleLessThanOrEqualConstantExpression()
        {
            var method = this.CreateType<int, bool>(
                e => e.LdArg1<int>() <= 10);

            Assert.IsTrue(method(1));
            Assert.IsTrue(method(10));
            Assert.IsFalse(method(20));
        }

        /// <summary>
        /// Emit If With Simple Equals Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleEqualsExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() == e.LdArg2<int>());

            Assert.IsTrue(method(10, 10));
            Assert.IsFalse(method(10, 1));
            Assert.IsFalse(method(1, 2));
        }

        /// <summary>
        /// Emit If With Simple Not Equals Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleNotEqualsExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() != e.LdArg2<int>());

            Assert.IsFalse(method(10, 10));
            Assert.IsTrue(method(10, 1));
            Assert.IsTrue(method(1, 2));
        }

        /// <summary>
        /// Emit If With Simple Greater Than Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleGreaterThanExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() > e.LdArg2<int>());

            Assert.IsTrue(method(20, 10));
            Assert.IsFalse(method(10, 10));
            Assert.IsFalse(method(1, 2));
        }

        /// <summary>
        /// Emit If With Simple Greater Than Or Equal Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleGreaterThanOrEqualExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() >= e.LdArg2<int>());

            Assert.IsTrue(method(20, 10));
            Assert.IsTrue(method(10, 10));
            Assert.IsFalse(method(1, 2));
        }

        /// <summary>
        /// Emit If With Simple Less Than Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleLessThanExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() < e.LdArg2<int>());

            Assert.IsTrue(method(10, 20));
            Assert.IsFalse(method(10, 10));
            Assert.IsFalse(method(2, 1));
        }

        /// <summary>
        /// Emit If With Simple Less Than Or Equal Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithSimpleLessThanOrEqualExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() <= e.LdArg2<int>());

            Assert.IsTrue(method(10, 20));
            Assert.IsTrue(method(10, 10));
            Assert.IsFalse(method(2, 1));
        }

        /// <summary>
        /// Emit If With Equals And Not Equals Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithEqualsAndNotEqualsExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() == 10 && e.LdArg2<int>() != 10);

            Assert.IsTrue(method(10, 1));
            Assert.IsFalse(method(10, 10));
            Assert.IsTrue(method(10, 20));
            Assert.IsFalse(method(1, 20));
        }

        /// <summary>
        /// Emit If With Equals And Greater Than Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithEqualsAndGreaterThanExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() == 10 && e.LdArg2<int>() > 10);

            Assert.IsTrue(method(10, 20));
            Assert.IsFalse(method(10, 10));
            Assert.IsFalse(method(1, 20));
        }

        /// <summary>
        /// Emit If With Equals And Greater Than Or Equal Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithEqualsAndGreaterThanOrEqualExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() == 10 && e.LdArg2<int>() >= 10);

            Assert.IsTrue(method(10, 20));
            Assert.IsTrue(method(10, 10));
            Assert.IsFalse(method(1, 20));
            Assert.IsFalse(method(1, 10));
        }

        /// <summary>
        /// Emit If With Equals And Less Than Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithEqualsAndLessThanExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() == 10 && e.LdArg2<int>() < 10);

            Assert.IsTrue(method(10, 1));
            Assert.IsFalse(method(10, 10));
            Assert.IsFalse(method(1, 20));
            Assert.IsFalse(method(1, 1));
        }

        /// <summary>
        /// Emit If With Equals And Less Than Or Equal Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithEqualsAndLessThanOrEqualExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() == 10 && e.LdArg2<int>() <= 10);

            Assert.IsTrue(method(10, 1));
            Assert.IsTrue(method(10, 10));
            Assert.IsFalse(method(1, 20));
            Assert.IsFalse(method(1, 10));
        }

        /// <summary>
        /// Emit If With Equals Or Not Equals Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithEqualsOrNotEqualsExpression()
        {
            var method = this.CreateType<int, int, bool>(
                e => e.LdArg1<int>() == 10 || e.LdArg2<int>() != 10);

            Assert.IsTrue(method(10, 1));
            Assert.IsTrue(method(10, 10));
            Assert.IsTrue(method(1, 20));
            Assert.IsFalse(method(1, 10));
        }

        /// <summary>
        /// Emit If With Modulus Equals Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithModulusEqualsExpression()
        {
            var method = this.CreateType<int, bool>(
                e => (e.LdArg1<int>() % 2) == 0);

            Assert.IsTrue(method(10));
            Assert.IsTrue(method(20));
            Assert.IsFalse(method(1));
            Assert.IsFalse(method(3));
        }

        /// <summary>
        /// Emit If With Modulus Not Equals Expression.
        /// </summary>
        [TestMethod]
        public void EmitIfWithModulusNotEqualsExpression()
        {
            var method = this.CreateType<int, bool>(
                e => (e.LdArg1<int>() % 2) != 0);

            Assert.IsFalse(method(10));
            Assert.IsFalse(method(20));
            Assert.IsTrue(method(1));
            Assert.IsTrue(method(3));
        }

        /// <summary>
        /// Creates a type to test an expression.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="expression">The expression to test.</param>
        /// <returns>A function to call to test the expression.</returns>
        private Func<T, TResult> CreateType<T, TResult>(Expression<Func<IExpression, bool>> expression)
        {
            var methodName = $"Method_{Guid.NewGuid()}";

            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}")
                .Public();

            testTypeBuilder
                .NewMethod(methodName)
                .Param<int>("arg1")
                .Returns<bool>()
                .Public()
                .Body(il => il
                    .DeclareLocal<bool>(out ILocal result)
                    .LdcI4_0()
                    .StLoc0()
                    .Nop()
                    .If(expression,
                        i => i
                            .LdcI4_1()
                            .StLoc0())
                    .LdLoc0()
                    .Ret());

            var type = testTypeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            return instance.GetMethodFunc<T, TResult>(methodName);
        }

        /// <summary>
        /// Creates a type to test an expression.
        /// </summary>
        /// <typeparam name="T1">The first argument type.</typeparam>
        /// <typeparam name="T2">The second argument type.</typeparam>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="expression">The expression to test.</param>
        /// <returns>A function to call to test the expression.</returns>
        private Func<T1, T2, TResult> CreateType<T1, T2, TResult>(Expression<Func<IExpression, bool>> expression)
        {
            var methodName = $"Method_{Guid.NewGuid()}";

            var testTypeBuilder = TypeFactory
                .Default
                .NewType($"TestType_{Guid.NewGuid()}")
                .Public();

            testTypeBuilder
                .NewMethod(methodName)
                .Param<int>("arg1")
                .Param<int>("arg2")
                .Returns<bool>()
                .Public()
                .Body(il => il
                    .DeclareLocal<bool>(out ILocal result)
                    .LdcI4_0()
                    .StLoc0()
                    .Nop()
                    .If(expression,
                        i => i
                            .LdcI4_1()
                            .StLoc0())
                    .LdLoc0()
                    .Ret());

            var type = testTypeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            return instance.GetMethodFunc<T1, T2, TResult>(methodName);
        }
    }
}