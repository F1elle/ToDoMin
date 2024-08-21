using ToDo.Api.Abstractions;
using ToDo.Api.Database;
using ToDo.Api.Endpoints;
using ToDo.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDbContext>();
builder.Services.AddScoped<IToDosRepository, ToDosRepository>();

var app = builder.Build();


app.MapEndpoints();


app.Run();
