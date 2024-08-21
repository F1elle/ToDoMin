using ToDo.Api.Abstractions;
using ToDo.Api.Dtos;

namespace ToDo.Api.Endpoints;

public static class ToDosEndpoints
{
    private const string GetTaskEndpoint = "GetTaskEndpoint";

    public static RouteGroupBuilder MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("tasks");

        // GET /tasks
        group.MapGet("/", async (IToDosRepository repository) => {
            return Results.Ok(await repository.GetAll());
        }
        ); // must return something like result

        // GET /tasks/{id}
        group.MapGet("/{id}", async (Guid id, IToDosRepository repository) =>
        {
            try
            {
                var task = await repository.GetById(id);
                return Results.Ok(task);
            }
            catch
            {
                return Results.NotFound("No tasks with such id");
            }
        }).WithName(GetTaskEndpoint);

        // POST /tasks
        group.MapPost("/", async (CreateToDoDto newToDo, IToDosRepository repository) =>
        {
            try
            {
                var task = await repository.Create(newToDo);

                return Results.CreatedAtRoute(GetTaskEndpoint, task);
            }
            catch
            {
                return Results.BadRequest("The task body can't be more than 250 characters long or empty");
            }
            
        });

        // DELETE /tasks/{id}
        group.MapDelete("/{id}", async (Guid id, IToDosRepository repository) =>
        {
            await repository.Delete(id);

            return Results.NoContent();
        });

        // PUT /tasks/{id}
        group.MapPut("/{id}", async (Guid id, CreateToDoDto updatedDto, IToDosRepository repository) =>
        {
            try
            {
                await repository.Update(id, updatedDto);
                return Results.NoContent();
            }
            catch (InvalidOperationException)
            {
                return Results.NotFound();
            }
            catch (ArgumentException)
            {
                return Results.BadRequest("The task body can't be more than 250 characters long or empty");
            }
        });

        group.MapPut("/{id}/mark-completed", async (Guid id, IToDosRepository repository) => {
            try 
            {
                await repository.MarkCompleted(id);
                return Results.NoContent();
            }
            catch
            {
                return Results.NotFound("No tasks with such id");
            }
        });

        group.MapPut("/{id}/mark-important", async (Guid id, IToDosRepository repository) => {
            try
            {
                await repository.MarkImportant(id);
                return Results.NoContent();
            }
            catch
            {
                return Results.NotFound("No tasks with such id");
            }
        });

        return group;
    }
}