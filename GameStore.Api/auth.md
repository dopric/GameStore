```c# 
bulder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Configuration["Jwt:Issuer"],
        ValidAudience = Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
    options.AddPolicy("User", policy => policy.RequireClaim("User"));
});
```
# install package: Microsoft.AspNetCore.Authentication.JwtBearer
in endpoints at the end add .RequireAuthorization(); to require authorization for all endpoints
in controllers add [Authorize] to require authorization for all endpoints in controller
in controllers add [Authorize(Policy = "Admin")] to require authorization for all endpoints in controller
in controllers add [Authorize(Policy = "User")] to require authorization for all endpoints in controller
