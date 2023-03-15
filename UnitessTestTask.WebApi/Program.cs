using JwtAuthentication.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using UnitessTestTask.Infrastructure;
using UnitessTestTask.Infrastructure.Database;
using UnitessTestTask.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicyName = "AllowAll";

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true,
        reloadOnChange: true);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; //отключение фильтра ModelState
});

var jwtOptions = new JwtOptions();
builder.Configuration.Bind("JwtOptions", jwtOptions);
var tokenManager = new TokenManager(jwtOptions);
builder.Services.AddJwtAuthentication(tokenManager.GetValidationParameters()!);
builder.Services.AddSingleton(tokenManager);

builder.Services.AddSingleton(new DbContext(builder.Configuration.GetConnectionString("UnitessTestTaskDb") ??
                                            "Data Source=UnitessTestDatabase.db"));
builder.Services.AddInfrastructure();

builder.Services.AddControllers();

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "UnitessTestTask API",
        Description = "Тестовое задание для Unitess",
        Contact = new OpenApiContact
        {
            Email = "filippenkov.valeriy@gmail.com"
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Введите действительный токен",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);
});

#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName,
        corsPolicy => corsPolicy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

#endregion

var app = builder.Build();

app.UseStaticFiles();

app.UseCors(corsPolicyName);

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.InjectStylesheet("/swagger-ui/SwaggerDark.css");
});

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();