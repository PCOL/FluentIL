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
        /// Program entry point.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        public static void Main(string[] args)
        {
            FluentIL.DebugOutput.Output = new ConsoleOutput();

            var typeBuilder = TypeFactory
                .Default
                .NewType("MyType");

            typeBuilder.NewDefaultConstructor(MethodAttributes.Public);

            typeBuilder
                .NewMethod("Hello")
                .Param<string>("arg")
                .Returns<bool>()
                .Public()
                .Body(il => il
                    .DeclareLocal<bool>("result", out ILocal result)
                    .DeclareLocal<string>("local1", out ILocal local1)
                    .DeclareLocal<int>("local2", out ILocal local2)
                    .LdcI4(10)
                    .StLoc2()
                    .Nop()
                    .IF(e => e.LdLoc<int>(local2) == 10 && e.LdArg1<string>() == "Hello",
                        ifil => ifil
                            .LdStr("if")
                            .StLoc1()
                            .LdcI4_0()
                            .StLoc0(),
                        elif => elif
                            .LdStr("else")
                            .StLoc1()
                            .LdcI4_1()
                            .StLoc0())
                    .WriteLineLoc(local1)
                    .LdLoc0()
                    .Ret());

            var mytype = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(mytype);
            var method = instance.GetMethodFunc<string, bool>("Hello");
            
            method("Hello");
        }
    }
}
