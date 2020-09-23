A handy utility that allows a method to return a result which contains a value on success or errors on failure

##Usages
```CSharp
void Test()
{
    var result = IsNumber("xyz");
    if (result.IsFailed)
    {
         Console.WriteLine(result.Error);
    }

    var number = result.Value;
    ...
}

Result<Number> IsNumber(string input)
{
    if (int.TryParse(input, out long number))
    {
         return Result<Number>.Succeeds(new Number(number));
    }
    return Result<Number>.Fails("Invalid input");
}
```