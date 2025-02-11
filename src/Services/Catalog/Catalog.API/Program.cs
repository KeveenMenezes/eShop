using BuildingBlocks.Behaviors;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services
    .AddCarter()
    .AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    })
    .AddValidatorsFromAssembly(typeof(Program).Assembly)
    .AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    })
    .UseLightweightSessions();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.MapCarter();

await app.RunAsync();

