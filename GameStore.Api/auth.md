Lesson 07.02
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

# Access Tokens JSON Web Tokens (JWT)
* this creates new section in appsettings.json
* dotnet user-jwts create  
* dotnet user-jtws print myid
```c#
"Authentication": {
    "Schemes": {
      "Bearer": {
        "ValidAudiences": [
          "http://localhost:61038",
          "https://localhost:44351",
          "http://localhost:5257",
          "https://localhost:7010"
        ],
        "ValidIssuer": "dotnet-user-jwts"
      }
    }
```
# Use Token
in Authorization tab select Bearer Token and paste token

# Roll Based Authorization

```c#
.RequireAuthorization(policy => policy.RequireRole("Admin"));
```
```commandline
dotnet user-jwts create --role Admin
```
```c#