using ToDo.Api.Abstractions;
using ToDo.Api.Database;
using ToDo.Api.Endpoints;
using ToDo.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDbContext>();
builder.Services.AddScoped<IToDosRepository, ToDosRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();



app.Run();
