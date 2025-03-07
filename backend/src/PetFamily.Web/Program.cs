using FluentValidation;
using PetFamily.Volunteers.Infrastructure;
using PetFamily.Specieses.Application;
using PetFamily.Specieses.Infrastructure;
using PetFamily.Web.Middlewares;
using Serilog;
using Serilog.Events;
using PetFamily.Volunteers.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PetFamily.Accounts.Infrastructure;
using PetFamily.Accounts.Application;
using Microsoft.OpenApi.Models;
using PetFamily.Accounts.Infrastructure.Seeding;
using PetFamily.Accounts.Presentation;
using PetFamily.Web.Services;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

Logger.AddLogger(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSerilog();

builder.Services
    .AddVolunteerApplication()
    .AddVolunteerInfrastructure(builder.Configuration)

    .AddSpeciesApplication()
    .AddSpeciesInfrastructure(builder.Configuration)

    .AddAccountsApplication()
    .AddAccountsInfractructue(builder.Configuration)
    .AddAccountsPresentation();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

var accountsSeeder = app.Services.GetRequiredService<AccountsSeeder>();

await accountsSeeder.SeedAsync();

app.UseExceptionMiddleware();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI");
            c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
        });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
