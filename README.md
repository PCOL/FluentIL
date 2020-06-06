# FluentIL

A .NET library for using reflection emit in a fluent way.

## Usage

FluentIL provides fluent syntax for defining types, fields, properties, methods, and events as well as all IL opcodes
to allow types to be created in a much more readable way.

It also contains syntax for more complex functions like for, foreach, do/while, and if.

## Creating Types

The main way to create a type is using the TypeFactory class. The TypeFactory class contains methods for creating Types, Delegate Types, and Global Methods.

To create a type you use the NewType() method which returns a ITypeBuilder instance:

````c#
var typeBuilder = TypeFactory
    .Default
    .NewType("TestType");
````

## Adding Methods

Now that you have a type builder you can build your type adding methods, properties, fields, events, etc...

````c#
typeBuilder
    .NewMethod<string>("Test")
    .Public()
    .Body()
    .LdStr("Hello World")
    .Call(typeof(Console).GetMethod("WriteLine"))
    .Ret();
````

Once the type is complete you can turn it into a concrete type using the CreateType() method:

````c#
var myType = typeBuilder.CreateType();
````

Now that you have a concrete type you can begin to use it:

````c#
var objInstance = Activator.CreateInstance(myType);
var testAction = objInstance.GetMethodAction("Test");
testAction();
````

## Adding Properties

## Conditionals and Expressions

In addition to the standard IL Op Codes Fluent IL also has high level conditional statements such as If, Else, For, Do, and While. This makes it easier to use by automatically emiting the appropriate IL for those operations. To improve this further contional statements all take expressions.

### Example 'If' statement

````C#
typeBuilder
    .NewMethod("Test")
    .Public()
    .Param<int>("arg")
    .Returns<bool>()
    .Body(m => m
        .DeclareLocal<bool>(out ILocal result)
        .LdcI4_0()
        .StLoc0()
        .Nop()
        .If(e => e.LdArg0<int>() == 10,
            m => m
                .LdcI4_1()
                .StLoc0())
        .Nop()
        .LdLoc(result)
        .Ret());
````

### Example 'For' statement

````C#
typeBuilder
    .NewMethod("Test")
    .Public()
    .Param<int>("arg")
    .Body(m => m
        .DeclareLocal<int>("localCount", out ILocal localCount)
        .DeclareLocal<int>("localItem", out ILocal localItem)
        .LdArg1()
        .StLoc0()
        .Nop()
        .For(i => i.LdcI4_0().StLoc(localItem),
            c => c.LdLoc<int>(localItem) < c.LdLoc<int>(localCount) &&
                c.LdLoc<int>(localItem) != 10,
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
````

## Examples

### Create Type

Creates a simple type with no constructors, methods, or properties.

````c#
var type = TypeFactory
    .Default
    .NewType("TestType")
    .CreateType();
````

### Create Type With Method

Creates a simple type with a test method that takes a single string parameter and returns it.

````c#
var typeBuilder = TypeFactory
    .Default
    .NewType("TestType");

typeBuilder
    .NewMethod<string>("TestMethod")
    .Public()
    .Param<string>("value");
    .Body()
    .Declare<string>(out ILocal local)
    .LdArg1()
    .StLoc(local)
    .Nop()
    .LdLoc()
    .Ret();

var type = typeBuilder.CreateType();
````

### Create Type with Property

Creates a simple type with a string property called Value with public get and set methods.

````c#
var typeBuilder = TypeFactory
    .Default
    .NewType("TestType");

var fieldValue = typeBuilder
    .NewField<string>("value")
    .Private();

typeBuilder
    .NewProperty<string>("Value");
    .Getter(m => m
        .Public()
        .Body()
        .LdArg0()
        .LdFld(fieldValue)
        .Ret())
    .Setter(m => m
        .Public()
        .Body()
        .LdArg0()
        .LdArg1()
        .StFld(fieldValue()
        .Ret())
````