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
```c#
// measure time on each request middleware
app.Use(async (context, next) => {
    try
    {
        var stopWatch = new Stopwatch();
        stopwatch.Start(); // start timer
        await next(context); // call next middleware
        stopwatch.Stop(); // stop timer
        var elapsed = stopwatch.ElapsedMilliseconds; // get elapsed time
        app.Logger.LogInformation("Request {Method} {Path} => {StatusCode} ({Elapsed} ms)", 
            context.Request.Method, context.Request.Path, context.Response.StatusCode, elapsed);
    }
    catch (Exception e)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
        logger.LogError(e, e.Message);
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync(e.Message);
    }
});
```
## Middleware Class
```c#
public class RequestTimeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimeMiddleware> _logger;

    public RequestTimeMiddleware(RequestDelegate next, ILogger<RequestTimeMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // add try catch block
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        await _next(context);
        stopwatch.Stop();
        var elapsed = stopwatch.ElapsedMilliseconds;
        _logger.LogInformation("Request {Method} {Path} => {StatusCode} ({Elapsed} ms)",
            context.Request.Method, context.Request.Path, context.Response.StatusCode, elapsed);
    }
}
```
## Use Middleware Class
```c#
app.UseMiddleware<RequestTimeMiddleware>();
```
## Use Builtin Exception Handler
```c#
// create directory ErrorHandling and create file ExceptionHandler.cs
public static class ErrorHandlingExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.Run(async context => {
            var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>()
            .CreateLogger("ExceptionHandler");
            var exceptionDetails = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionDetails?.Error;
            logger.LogError(exception, exception?.Message);
            
            var problemDetails = new ProblemDetails
            {
                Title = exception?.Message,
                Status = StatusCodes.Status500InternalServerError,
                Extensions = {
                    {"traceId"} = Activity.Current?.TraceId.ToString()
                }
                Detail = exception?.StackTrace,
                Instance = exceptionDetails?.Path
            };
            var environment = context.RequestServices.GetRequiredService<IHostEnvironment>();
            if(environment.IsDevelopment())
            {
                // problemDetails.Detail = exception?.ToString();
                problemDetails.Extensions.Add("error", exception?.ToString());
            }
            await Results.Problem(problemDetails).ExecuteAsync(context);
         });
    }
}
    
```
## Use Exception Handler
```c#
app.UseExceptionHandler(exceptionHandler: app => app.ConfigureExceptionHandler());
// now we can remove try catch block from all endpoints, cool!
// disable built in exception handler in appsettings.json add this line
"Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware": "None"
```