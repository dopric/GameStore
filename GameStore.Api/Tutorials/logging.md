# Logging
## Sending logs via WebApplication logger
```csharp
 # use in program.cs
 app.Logger.LogInformation(1, "Application started");
```
## Using ILoggerFactory
```csharp
public class TestClass{
    // IServiceProvider serviceProvider
    var logger = serviceProvider.GetRequiredService<ILoggerFactory>()
        .CreateLogger<TestClass>();
    }
```

## Using Logger using dependency injection
```csharp
public class MyController{
    private readonly ILogger _logger;

    public MyController(ILogger<MyController> logger)
    {
        _logger = logger;
    }
}
``` 
## Using JsonLogger
```csharp
// in program.cs
builder.Logging.AddJsonConsole(options => 
    options.JsonWriterOptions = new JsonWriterOptions
{
    Indented = true
});
```

## Using Structured logging
```csharp
public class MyController{
    private readonly ILogger _logger;

    public MyController(ILogger<MyController> logger)
    {
        _logger = logger;
        var game = new Game("Mario Bros", 10);
        // in Json message Name and Price can be pulled out as properties in State
        _logger.LogInformation("Created game with {Name} and {Price}", game.Name, game.Price);
    }
}    
```
## Configuring log levels
```csharp
// in appsettings.json
"Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.Hosting.Lifetime": "Information",
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  }
```
## Use HttpLogging