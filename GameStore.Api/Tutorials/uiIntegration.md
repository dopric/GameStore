# UI Integration
## CORS
```c#
// appSettings.json
"AllowedHosts": "*",
 "AllowedOrigins": [
   "http://localhost:4200"
 ],
    
  // Program.cs
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("FrontEndClient", builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
        // or something like this
        options.AddDefaultPolicy(corsBuilder =>
        {
            var allowedOrigins = builder.Configuration["AllowedOrigins"] ?? 
                throw new InvalidOperationException("AllowedOrigins not set in configuration")
            corsbuilder.WithOrigins(allowedOrigins.Split(","))
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
    });

// at the end of Configure method
app.UseCors("FrontEndClient");
```
## Refactor to extension method
```c#
// create Extensions folder and create CorsExtensions.cs
public static class CorsExtensions
{
    public static void AddGAmeStoreCorsExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("FrontEndClient", builder =>
            {
                builder.WithOrigins(configuration["AllowedOrigins"])
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });
    }
}