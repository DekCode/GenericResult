A supper lightweight handy utility to return a result with a value on success or errors on failure. Better error handling.
Inspired by `FluentResult`

##Quickly why?
- Results with errors allow you to handle errors in your application properly. Let's say a function that returns a boolean can be `true`, `false`, or errors.
- Clean code. The method returns errors and a value at once.

##Why use this package?
- Supper lightweight
- Free + opensource
- Easy to use
- Simple, look at the code below

##Usages
```CSharp
void ResultDemo()
{
    var result = Divide(108.0, 9);
    if (result.IsFailed)
    {
        Console.WriteLine(result.Error);
    }

    Console.WriteLine(result.Value);
}

Result<double> Divide(double dividen, double divider)
{
    if (divider == 0.0)
    {
        Result.Fail("Cannot divide by zero");
    }

    var someNumber = dividen / divider;
    return Result.Succeed(someNumber);
}
```