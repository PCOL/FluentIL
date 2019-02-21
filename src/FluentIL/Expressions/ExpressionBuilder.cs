namespace FluentIL.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Text;
    using FluentIL;

    /// <summary>
    /// Used to build expressions.
    /// </summary>
    public class ExpressionBuilder
        : ExpressionVisitor
    {
        /// <summary>
        /// Load constant opcodes.
        /// </summary>
        private static readonly OpCode[] LDCI4 = { OpCodes.Ldc_I4_0, OpCodes.Ldc_I4_1, OpCodes.Ldc_I4_2, OpCodes.Ldc_I4_3, OpCodes.Ldc_I4_4, OpCodes.Ldc_I4_5, OpCodes.Ldc_I4_6, OpCodes.Ldc_I4_7, OpCodes.Ldc_I4_8 };

        /// <summary>
        /// A reference to an emitter.
        /// </summary>
        private readonly IEmitter emitter;
        
        /// <summary>
        /// An argument stack.
        /// </summary>
        private readonly Stack<object> arguments = new Stack<object>();

        /// <summary>
        /// The last expression type.
        /// </summary>
        private ExpressionType? lastExpressionType;

        /// <summary>
        /// A label to the true store.
        /// </summary>
        private ILabel storeTrueLabel;

        /// <summary>
        /// A label to the false store.
        /// </summary>
        private ILabel storeFalseLabel;

        /// <summary>
        /// A label to the result store.
        /// </summary>
        private ILabel storeResultLabel;

        /// <summary>
        /// Initialises a new instance of the <see cref="ExpressionBuilder"/> class.
        /// </summary>
        /// <param name="emitter">A reference to the emitter.</param>
        public ExpressionBuilder(IEmitter emitter)
        {
            this.emitter = emitter;
        }

        /// <summary>
        /// Emits an IF statement.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="trueAction">The action to perform if the result of the expression is true.</param>
        /// <param name="falseAction">The action to perform if the result of the expression is false.</param>
        public void EmitIF(LambdaExpression expression, Action<IEmitter> trueAction, Action<IEmitter> falseAction = null)
        {
            this.emitter
                .DeclareLocal<bool>("result", out ILocal result)
                .DefineLabel("storeTrue", out this.storeTrueLabel)
                .DefineLabel("storeFalse", out this.storeFalseLabel)
                .DefineLabel("storeResult", out this.storeResultLabel)
                .DefineLabel("if", out ILabel ifLabel)
                .DefineLabel("else", out ILabel elseLabel)
                .DefineLabel("endif", out ILabel endifLabel);

            Visit(expression.Body);

            this.emitter
                .BrS(this.storeResultLabel)
                .Nop()
                .MarkLabel(this.storeTrueLabel)
                .LdcI4_1()
                .BrS(this.storeResultLabel)
                .MarkLabel(this.storeFalseLabel)
                .LdcI4_0()
                .MarkLabel(this.storeResultLabel)
                .StLoc(result)
                .Nop()
                .LdLoc(result)
                .BrFalse(elseLabel)
                .MarkLabel(ifLabel)
                .Nop();

            trueAction?.Invoke(this.emitter);

            this.emitter
                .Br(endifLabel)
                .MarkLabel(elseLabel)
                .Nop();

            falseAction?.Invoke(this.emitter);

            this.emitter
                .MarkLabel(endifLabel);
        }

        /// <inheritdoc />
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.OrElse ||
                node.NodeType == ExpressionType.AndAlso)
            {
                this.lastExpressionType = node.NodeType;
            }

            Type compareType = node.Left.Type;

            Visit(node.Left);
           
            Visit(node.Right);

            if (node.NodeType != ExpressionType.OrElse &&
                node.NodeType != ExpressionType.AndAlso)
            {
                ProcessOperator(node.NodeType, compareType);
            }
            
            return node;
        }

        /// <inheritdoc />
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.NodeType == ExpressionType.Constant ||
                node.NodeType == ExpressionType.MemberAccess)
            {
                Visit(node.Expression);
            }

            return node;
        }

        /// <inheritdoc />
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value == null)
            {
                this.emitter.LdNull();
                return node;
            }

            Type valueType = node.Value.GetType();
            if (valueType == typeof(bool))
            {
                this.emitter.LdcI4((bool)node.Value == true ? 1 : 0);
            }
            if (valueType == typeof(byte))
            {
                this.emitter.LdcI4_S((byte)node.Value);
            }
            else if (valueType == typeof(short))
            {
                this.emitter
                    .LdcI4((short)node.Value)
                    .ConvI2();
            }
            else if (valueType == typeof(int))
            {
                int value = (int)node.Value;
                if (value >= 0 && value <= 8)
                {
                    this.emitter.Emit(LDCI4[value]);
                }
                else if (value == -1)
                {
                    this.emitter.LdcI4_M1();
                }
                else
                {
                    this.emitter.LdcI4(value);
                }
            }
            else if (valueType == typeof(long))
            {
                this.emitter.LdcI8((long)node.Value);
            }
            else if (valueType == typeof(float))
            {
                this.emitter.LdcR4((float)node.Value);
            }
            else if (valueType == typeof(double))
            {
                this.emitter.LdcR8((double)node.Value);
            }
            else if (valueType == typeof(string))
            {
                this.emitter.LdStr((string)node.Value);
            }
            else
            {
                FieldInfo fieldInfo = valueType.GetFields().FirstOrDefault();

                object value = fieldInfo.GetValue(node.Value);
                if (value is ILocal local)
                {
                    this.arguments.Push(local);
                }
                else if (value is IFieldBuilder field)
                {
                    this.arguments.Push(field);
                }
            }

            return node;
        }

        /// <inheritdoc />
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Object.NodeType == ExpressionType.Call)
            {
                Visit(node.Object);    
            }

            Visit(node.Arguments);
            emitter.EmitMethod(node.Method, this.arguments);

            return node;
        }

        /// <summary>
        /// Processs the operator.
        /// </summary>
        /// <param name="expressionType">The expression type.</param>
        /// <param name="comparisonType">The comparison type.</param>
        private void ProcessOperator(ExpressionType expressionType, Type comparisonType)
        {
            bool last = true;
            if (this.lastExpressionType.HasValue == true)
            {
                last = false;
            }

            MethodInfo compareMethod = null;
            if (comparisonType != null &&
                comparisonType.IsClass == true)
            {
                compareMethod = comparisonType.GetMethod("op_Equality", BindingFlags.Public | BindingFlags.Static, null, new[] { comparisonType, comparisonType }, null);
            }

            switch(expressionType)
            {
                case ExpressionType.Equal:
                    if (compareMethod != null)
                    {
                        this.emitter.Call(compareMethod);
                        if (last == false)
                        {
                            if (this.lastExpressionType == ExpressionType.AndAlso)
                            {
                                this.emitter.BrFalseS(this.storeFalseLabel);
                            }
                            else
                            {
                                this.emitter.BrTrueS(this.storeTrueLabel);
                            }
                        }
                    }
                    else
                    {
                        if (last == true)
                        {
                            this.emitter.Ceq();
                        }
                        else if (this.lastExpressionType == ExpressionType.AndAlso)
                        {
                            this.emitter.BneUnS(this.storeFalseLabel);
                        }
                        else
                        {
                            this.emitter.Beq(this.storeTrueLabel);
                        }
                    }

                    break;

                case ExpressionType.NotEqual:
                    if (last == true)
                    {
                        this.emitter.CgtUn();
                    }
                    else if (this.lastExpressionType == ExpressionType.AndAlso)
                    {
                        this.emitter.Beq(this.storeFalseLabel);
                    }
                    else
                    {
                        this.emitter.BneUnS(this.storeTrueLabel);
                    }

                    break;

                case ExpressionType.GreaterThan:
                    if (last == true)
                    {
                        this.emitter.Clt();
                    }
                    else if (this.lastExpressionType == ExpressionType.AndAlso)
                    {
                        this.emitter.BleS(this.storeFalseLabel);
                    }
                    else
                    {
                        this.emitter.BgtS(this.storeTrueLabel);
                    }

                    break;

                case ExpressionType.GreaterThanOrEqual:
                    if (last == true)
                    {
                        this.emitter
                            .Clt()
                            .LdcI4_0()
                            .Ceq();
                    }
                    else if (this.lastExpressionType == ExpressionType.AndAlso)
                    {
                        this.emitter.BltS(this.storeFalseLabel);
                    }
                    else
                    {
                        this.emitter.BgeS(this.storeTrueLabel);
                    }

                    break;

                case ExpressionType.LessThan:
                    if (last == true)
                    {
                        this.emitter.Cgt();
                    }
                    else if (this.lastExpressionType == ExpressionType.AndAlso)
                    {
                        this.emitter.BgeS(this.storeFalseLabel);
                    }
                    else
                    {
                        this.emitter.BltS(this.storeTrueLabel);
                    }

                    break;

                case ExpressionType.LessThanOrEqual:
                    if (last == true)
                    {
                        this.emitter
                            .Cgt()
                            .LdcI4_0()
                            .Ceq();
                    }
                    else if (this.lastExpressionType == ExpressionType.AndAlso)
                    {
                        this.emitter.BgtS(this.storeFalseLabel);
                    }
                    else
                    {
                        this.emitter.BleS(this.storeTrueLabel);
                    }

                    break;

                case ExpressionType.Modulo:
                    this.emitter.Rem();
                    break;

            }

            this.lastExpressionType = null;
        }
    }
}