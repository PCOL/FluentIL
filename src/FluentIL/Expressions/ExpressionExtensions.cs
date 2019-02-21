namespace FluentIL.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using FluentIL;

    /// <summary>
    /// Expression extension methods.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// A list of supported method.
        /// </summary>
        private static readonly MethodInfo[] SupportedMethods = new[]
        {
            typeof(EmitterLdExtensions).GetMethod("LdArg0", new[] { typeof(IEmitter) }),
            typeof(EmitterLdExtensions).GetMethod("LdArg1", new[] { typeof(IEmitter) }),
            typeof(EmitterLdExtensions).GetMethod("LdArg2", new[] { typeof(IEmitter) }),
            typeof(EmitterLdExtensions).GetMethod("LdArg3", new[] { typeof(IEmitter) }),
            typeof(EmitterLdExtensions).GetMethod("LdLoc", new[] { typeof(IEmitter), typeof(ILocal) }),
            typeof(EmitterLdExtensions).GetMethod("LdLoc0", new[] { typeof(IEmitter) }),
            typeof(EmitterLdExtensions).GetMethod("LdLoc1", new[] { typeof(IEmitter) }),
            typeof(EmitterLdExtensions).GetMethod("LdLoc2", new[] { typeof(IEmitter) }),
            typeof(EmitterLdExtensions).GetMethod("LdLoc3", new[] { typeof(IEmitter) }),
            typeof(EmitterLdExtensions).GetMethod("LdFld", new[] { typeof(IEmitter), typeof(IFieldBuilder) }),
            typeof(EmitterLdExtensions).GetMethod("LdNull", new[] { typeof(IEmitter) }),
            typeof(EmitterLdcExtensions).GetMethod("LdcI4", new[] { typeof(IEmitter), typeof(int) }),
            typeof(EmitterLdcExtensions).GetMethod("LdcI4_0", new[] { typeof(IEmitter) }),
            typeof(EmitterLdcExtensions).GetMethod("LdcI4_1", new[] { typeof(IEmitter) }),
            typeof(EmitterLdcExtensions).GetMethod("LdcI4_2", new[] { typeof(IEmitter) }),
            typeof(EmitterLdcExtensions).GetMethod("LdcI4_3", new[] { typeof(IEmitter) }),
            typeof(EmitterLdcExtensions).GetMethod("LdcI4_4", new[] { typeof(IEmitter) }),
            typeof(EmitterLdcExtensions).GetMethod("LdcI4_5", new[] { typeof(IEmitter) }),
            typeof(EmitterLdcExtensions).GetMethod("LdcI4_6", new[] { typeof(IEmitter) }),
            typeof(EmitterLdcExtensions).GetMethod("LdcI4_7", new[] { typeof(IEmitter) }),
            typeof(EmitterLdcExtensions).GetMethod("LdcI4_8", new[] { typeof(IEmitter) }),
        };

        /// <summary>
        /// Emits an expression method.
        /// </summary>
        /// <param name="emitter">A reference to an emitter.</param>
        /// <param name="expressionMethod">A reference to the expression method.</param>
        /// <param name="arguments">A reference to the argument stack.</param>
        internal static void EmitMethod(this IEmitter emitter, MethodInfo expressionMethod, Stack<object> arguments)
        {
            if (expressionMethod.DeclaringType == typeof(IExpression))
            {
                if (expressionMethod.Name != "Value")
                {
                    var method = SupportedMethods.FirstOrDefault(m => m.Name == expressionMethod.Name);
                    if (method == null)
                    {
                        throw new NotSupportedException($"OpCode '{expressionMethod.Name}' not supported");
                    }

                    var parameters = method.GetParameters();
                    object[] values = new object[parameters.Length];
                    values[0] = emitter;
                    for (int i = 1; i < parameters.Length; i++)
                    {
                        values[i] = arguments.Pop();
                    }

                    method.Invoke(null, values);
                }
            }
            else
            {
                emitter.Call(expressionMethod);
            }
        }
    }
}