namespace FluentIL
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using FluentIL.Builders;
    using FluentIL.Expressions;

    /// <summary>
    /// Reflection emit extension methods.
    /// </summary>
    public static class ReflectionEmitExtensions
    {
        /// <summary>
        /// Defines a global method.
        /// </summary>
        /// <param name="moduleBuilder">A <see cref="ModuleBuilder"/> instance.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="returnType">The return type.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance.</returns>
        public static IMethodBuilder DefineGlobalMethod(
            this ModuleBuilder moduleBuilder,
            string methodName,
            Type returnType)
        {
            return moduleBuilder
                .DefineGlobalMethod(methodName)
                .Returns(returnType);
        }

        /// <summary>
        /// Defines a global method.
        /// </summary>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="moduleBuilder">A <see cref="ModuleBuilder"/> instance.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance.</returns>
        public static IMethodBuilder DefineGlobalMethod<TReturn>(
            this ModuleBuilder moduleBuilder,
            string methodName)
        {
            return moduleBuilder
                .DefineGlobalMethod(methodName)
                .Returns(typeof(TReturn));
        }

        /// <summary>
        /// Defines a global method.
        /// </summary>
        /// <param name="moduleBuilder">A <see cref="ModuleBuilder"/> instance.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance.</returns>
        public static IMethodBuilder DefineGlobalMethod(
            this ModuleBuilder moduleBuilder,
            string methodName)
        {
            Func<string, MethodAttributes, CallingConventions, Type, Type[], Type[], Type[], Type[][], Type[][], MethodBuilder> defineFunc =
                (name,
                attributes,
                callingConvention,
                rType,
                returnTypeRequiredCustomModifiers,
                returnTypeOptionalCustomModifiers,
                parameterTypes,
                parameterTypeRequiredCustomModifiers,
                parameterTypeOptionalCustomModifiers) =>
                {
                    return moduleBuilder.DefineGlobalMethod(
                        name,
                        attributes |= MethodAttributes.Static,
                        callingConvention,
                        rType,
                        returnTypeRequiredCustomModifiers,
                        returnTypeOptionalCustomModifiers,
                        parameterTypes,
                        parameterTypeRequiredCustomModifiers,
                        parameterTypeOptionalCustomModifiers);
                };

            return new FluentMethodBuilder(methodName, defineFunc)
                .Static();
        }

        /// <summary>
        /// Helper method for setting custom attributes.
        /// </summary>
        /// <param name="attrs">A list of custom attributes.</param>
        /// <param name="action">An action for settin the attribute.</param>
        internal static void SetCustomAttributes(
            this IEnumerable<CustomAttributeBuilder> attrs,
            Action<CustomAttributeBuilder> action)
        {
            if (attrs != null)
            {
                foreach (var attr in attrs)
                {
                    action(attr);
                }
            }
        }

        /// <summary>
        /// Emits IL to check if the passed in local variable is null or not, executing the emitted body if not.
        /// </summary>
        /// <param name="ilGen">The <see cref="IEmitter"/> to use.</param>
        /// <param name="local">The locval variable to check.</param>
        /// <param name="emitBody">A function to emit the IL to be executed if the object is not null.</param>
        /// <param name="emitElse">A function to emit the IL to be executed if the object is null.</param>
        /// <returns>The passed in <see cref="IEmitter"/>.</returns>
        public static IEmitter IfNotNull(this IEmitter ilGen, ILocal local, Action<IEmitter> emitBody, Action<IEmitter> emitElse = null)
        {
            return ilGen
                .Emit(OpCodes.Ldloc, local)
                .IfNotNull(emitBody, emitElse);
        }

        /// <summary>
        /// Emits IL to check if the object on the top of the evaluation stack is not null, executing the emitted body if not.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="emitBody">A function to emit the IL to be executed if the object is not null.</param>
        /// <param name="emitElse">A function to emit the IL to be executed if the object is null.</param>
        /// <returns>The passed in <see cref="IEmitter"/>.</returns>
        public static IEmitter IfNotNull(this IEmitter emitter, Action<IEmitter> emitBody, Action<IEmitter> emitElse = null)
        {
            emitter.DefineLabel("endif", out ILabel endIf);

            if (emitElse != null)
            {
                emitter
                    .DefineLabel("else", out ILabel notNull)
                    .Emit(OpCodes.Brtrue, notNull)
                    .Emit(OpCodes.Nop);

                emitElse(emitter);

                emitter
                    .Emit(OpCodes.Br, endIf)
                    .MarkLabel(notNull);

                emitBody(emitter);
            }
            else
            {
                emitter
                    .Emit(OpCodes.Brfalse, endIf)
                    .Emit(OpCodes.Nop);

                emitBody(emitter);
            }

            return emitter
                .MarkLabel(endIf);
        }

        /// <summary>
        /// Emits an if operation.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="expression">An expression.</param>
        /// <param name="action">An action to perform if the expression is true.</param>
        /// <param name="elseAction">An action to perform if the expression is false.</param>
        /// <returns>The passed in <see cref="IEmitter"/>.</returns>
        public static IEmitter If(this IEmitter emitter, Expression<Func<IExpression, bool>> expression, Action<IEmitter> action, Action<IEmitter> elseAction = null)
        {
            var builder = new ExpressionBuilder(emitter);
            builder.EmitIF(expression, action, elseAction);
            return emitter;
        }

        /// <summary>
        /// Emits a while loop.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="expression">The while expression.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The passed in <see cref="IEmitter"/>.</returns>
        public static IEmitter While(this IEmitter emitter, Expression<Func<IExpression, bool>> expression, Action<IEmitter> action)
        {
            var builder = new ExpressionBuilder(emitter);
            builder.EmitWhile(expression, action);
            return emitter;            
        }

        /// <summary>
        /// Emits a do while loop.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="expression">The do while expression.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The passed in <see cref="IEmitter"/>.</returns>
        public static IEmitter Do(this IEmitter emitter, Expression<Func<IExpression, bool>> expression, Action<IEmitter> action)
        {
            var builder = new ExpressionBuilder(emitter);
            builder.EmitDoWhile(expression, action);
            return emitter;            
        }

        /// <summary>
        /// Emits a for loop.
        /// </summary>
        /// <param name="emitter">The <see cref="IEmitter"/> to use.</param>
        /// <param name="initialiser">The initialiser expression.</param>
        /// <param name="condition">The condition expression.</param>
        /// <param name="iterator">The iterator expression.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The passed in <see cref="IEmitter"/>.</returns>
        public static IEmitter For(this IEmitter emitter, Expression<Action<IInitialiser>> initialiser, Expression<Func<ICondition, bool>> condition, Expression<Action<IIterator>> iterator, Action<IEmitter> action)
        {
            var builder = new ExpressionBuilder(emitter);
            builder.EmitFor(initialiser, condition, iterator, action);
            return emitter;            
        }
    }
}