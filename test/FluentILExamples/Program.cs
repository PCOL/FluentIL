namespace FluentILExamples
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using FluentIL;
    
    /// <summary>
    /// Main program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// <see cref="Console.WriteLine(string, object[])"/> <see cref="MethodInfo"/>.
        /// </summary>
        private static MethodInfo ConsoleWriteLine = typeof(Console).GetMethodWithParameters("WriteLine", BindingFlags.Public | BindingFlags.Static, new[] { typeof(string), typeof(object[]) });

        /// <summary>
        /// <see cref="Console.WriteLine(string, object, object)"/> <see cref="MethodInfo"/>.
        /// </summary>
        private static MethodInfo ConsoleWriteLineStringObjectObject = typeof(Console).GetMethodWithParameters("WriteLine", BindingFlags.Public | BindingFlags.Static, new[] { typeof(string), typeof(object), typeof(object) });

        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        public static void Main(string[] args)
        {
            // FluentIL.DebugOutput.Output = new ConsoleOutput();

            IfExample();
            WhileExample();
            DoExample();
            ForExample();
        }

        private static void IfExample()
        {
            Console.WriteLine("If Example");
            Console.WriteLine();

            var typeBuilder = TypeFactory
                .Default
                .NewType("IfExampleType");

            typeBuilder.NewDefaultConstructor(MethodAttributes.Public);

            typeBuilder
                .NewMethod("IfExample")
                .Param<string>("arg1")
                .Param<int>("arg2")
                .Returns<bool>()
                .Public()
                .Body(il => il
                    .DeclareLocal<bool>("result", out ILocal result)
                    .DeclareLocal<int>("local", out ILocal local)
                    .LdArg2()
                    .StLoc1()
                    .Nop()
                    .If(e => e.LdLoc<int>(local) == 10 && e.LdArg1<string>() == "Hello",
                        ifil => ifil
                            .LdcI4_1()
                            .StLoc0(),
                        elif => elif
                            .LdcI4_0()
                            .StLoc0())
                    .LdLoc0()
                    .Ret());

            var mytype = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(mytype);
            var method = instance.GetMethodFunc<string, int, bool>("IfExample");
            
            Console.WriteLine("Arg2 {0} == 10 && Arg1 {1} == Hello [{2}]", 10, "Hello", method("Hello", 10));
            Console.WriteLine("Arg2 {0} == 10 && Arg1 {1} == Hello [{2}]", 20, "Hello", method("Hello", 20));
            Console.WriteLine("Arg2 {0} == 10 && Arg1 {1} == Hello [{2}]", 10, "World", method("World", 10));
        }

        private static void WhileExample()
        {
            Console.WriteLine("While Example");
            Console.WriteLine();

            var typeBuilder = TypeFactory
                .Default
                .NewType("WhileExampleType");

            typeBuilder.NewDefaultConstructor(MethodAttributes.Public);

            typeBuilder
                .NewMethod("WhileExample")
                .Param<int>("arg1")
                .Public()
                .Body(il => il
                    .DeclareLocal<int>("localCount", out ILocal localCount)
                    .DeclareLocal<int>("localItem", out ILocal localItem)
                    .LdArg1()
                    .StLoc0()
                    .LdcI4_0()
                    .StLoc1()
                    .Nop()
                    .While(e => e.LdLoc<int>(localItem) < e.LdLoc<int>(localCount),
                        i => i
                            .LdStr("Loop {0} of {1}")
                            .LdLoc1()
                            .Box<int>()
                            .LdLoc0()
                            .Box<int>()
                            .Call(ConsoleWriteLineStringObjectObject)
                            .Nop()
                            .LdLoc(localItem)
                            .LdcI4_1()
                            .Add()
                            .StLoc1())
                    .Ret());

            var mytype = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(mytype);
            var method = instance.GetMethodAction<int>("WhileExample");

            method(10);            
        }

        private static void DoExample()
        {
            Console.WriteLine("Do Example");
            Console.WriteLine();

            var typeBuilder = TypeFactory
                .Default
                .NewType("DoExampleType");

            typeBuilder.NewDefaultConstructor(MethodAttributes.Public);

            typeBuilder
                .NewMethod("DoExample")
                .Param<int>("arg1")
                .Public()
                .Body(il => il
                    .DeclareLocal<int>("localCount", out ILocal localCount)
                    .DeclareLocal<int>("localItem", out ILocal localItem)
                    .LdArg1()
                    .StLoc0()
                    .LdcI4_0()
                    .StLoc1()
                    .Nop()
                    .Do(e => e.LdLoc<int>(localItem) < e.LdLoc<int>(localCount),
                        i => i
                            .LdStr("Loop {0} of {1}")
                            .LdLoc1()
                            .Box<int>()
                            .LdLoc0()
                            .Box<int>()
                            .Call(ConsoleWriteLineStringObjectObject)
                            .Nop()
                            .LdLoc(localItem)
                            .LdcI4_1()
                            .Add()
                            .StLoc1())
                    .Ret());

            var mytype = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(mytype);
            var method = instance.GetMethodAction<int>("DoExample");

            method(10);            
        }

        private static void ForExample()
        {
            Console.WriteLine("For Example");
            Console.WriteLine();

            var typeBuilder = TypeFactory
                .Default
                .NewType("ForExampleType");

            typeBuilder.NewDefaultConstructor(MethodAttributes.Public);

            typeBuilder
                .NewMethod("ForExample")
                .Param<int>("arg1")
                .Public()
                .Body(il => il
                    .DeclareLocal<int>("localCount", out ILocal localCount)
                    .DeclareLocal<int>("localItem", out ILocal localItem)
                    .LdArg1()
                    .StLoc0()
                    .Nop()
                    .For(i => i.LdcI4_0().StLoc(localItem),
                        c => c.LdLoc<int>(localItem) < c.LdLoc<int>(localCount) && c.LdLoc<int>(localItem) != 10,
                        i => i.Inc(localItem),
                        e => e
                            .LdStr("Loop {0} of {1}")
                            .LdLoc1()
                            .Box<int>()
                            .LdLoc0()
                            .Box<int>()
                            .Call(ConsoleWriteLineStringObjectObject)
                            .Nop())
                    .Ret());

            var mytype = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(mytype);
            var method = instance.GetMethodAction<int>("ForExample");

            method(10);
        }
    }
}
