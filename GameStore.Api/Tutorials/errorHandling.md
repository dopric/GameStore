# Error handling and Middleware
```c#
group.MapGet("/error", (IGameRepository repository, ILoggerFactory factory) => {
    
    try
    {
        return Results.Ok(await repository.GetGameAsync(1));
    }
    catch (Exception e)
    {
        var logger = factory.CreateLogger("Error");
        logger.LogError(e, e.Message);
        
        // Environment.MachineName
        // Activity.Current?.TraceId
        return Results.Error(
            title: e.Message, 
            statusCode: StatusCodes.Status500InternalServerError,
            extensions: new Dictionary<string, object?>{
                {"traceId", Activity.Current?.TraceId }); // Results.Problem(e.Message);
    }
```
# Middleware