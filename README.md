# This is an experiment. Don't use it in a production system.

# Dapper.Repository
This is not an official Dapper project!

A simple light weight Repository wrapper around Dapper & Dapper.Contrib.

## Configuration
Within a C# .NET 5 Web Applications Startup.cs or the project that contains the Domain models:
```c#
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddRepository("[Insert your connection string here]");
    //...
}
```

## Usage
1. Ensure all your domain entities inherit `Entity`
2. Inject `IRepository` where necessary
3. ???
4. Profit
