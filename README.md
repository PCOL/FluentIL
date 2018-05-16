# FluentIL

A .NET library for using reflection emit in a fluent way.

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
