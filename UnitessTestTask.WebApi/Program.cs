using JwtAuthentication.AspNetCore;
using UnitessTestTask.Infrastructure;
using UnitessTestTask.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

var jwtOptions = new JwtOptions();
builder.Configuration.Bind("JwtOptions", jwtOptions);
var tokenManager = new TokenManager(jwtOptions);
builder.Services.AddJwtAuthentication(tokenManager.GetValidationParameters()!);
builder.Services.AddSingleton(tokenManager);

builder.Services.AddSingleton(new DbContext(builder.Configuration.GetConnectionString("UnitessTestTaskDb") ??
                                            "Data Source=UnitessTestDatabase.db"));
builder.Services.AddInfrastructure();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();