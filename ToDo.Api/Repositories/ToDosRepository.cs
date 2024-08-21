using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ToDo.Api.Abstractions;
using ToDo.Api.Database;
using ToDo.Api.Dtos;
using ToDo.Api.Entities;
using ToDo.Api.Mapping;

namespace ToDo.Api.Repositories;

public class ToDosRepository : IToDosRepository
{
    private ToDoDbContext _dbContext;

    public ToDosRepository(ToDoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ToDoDto>> GetAll() => await _dbContext.ToDos
        .Select(entity => entity.ToDto())
        .AsNoTracking().ToListAsync();

    public async Task<ToDoDto> GetById(Guid id)
    {
        ToDoEntity? entity = await _dbContext.ToDos.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
        {
            throw new InvalidOperationException();
        }

        return entity.ToDto();
    }

    public async Task<ToDoDto> Create(CreateToDoDto newToDo)
    {
        if (string.IsNullOrEmpty(newToDo.Body) || newToDo.Body.Length > 250) 
        {
            throw new ArgumentException();
        }
        else {
            var toDoEntity = newToDo.ToEntity();
            await _dbContext.ToDos.AddAsync(toDoEntity);
            await _dbContext.SaveChangesAsync();
            return toDoEntity.ToDto();
        }
    }

    public async Task<Guid> Update(Guid id, CreateToDoDto updatedToDo)
    {
        var currentToDo = await _dbContext.ToDos.FindAsync(id);
        if (currentToDo == null)
        {
            throw new InvalidOperationException();
        }
        if (string.IsNullOrEmpty(updatedToDo.Body) || updatedToDo.Body.Length > 250) 
        {
            throw new ArgumentException();
        }
        else
        {
            currentToDo.Body = updatedToDo.Body;
            currentToDo.CreateTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return currentToDo.Id;
        }
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _dbContext.ToDos.Where(e => e.Id == id).ExecuteDeleteAsync();
        return id;
    }

    public async Task<Guid> MarkCompleted(Guid id)
    {
        var task = await _dbContext.ToDos.FindAsync(id);
        if (task == null)
        {
            throw new InvalidOperationException();
        }

        task.IsCompleted = !task.IsCompleted;
        await _dbContext.SaveChangesAsync();
        return id;
    }

    public async Task<Guid> MarkImportant(Guid id)
    {
        var task = await _dbContext.ToDos.FindAsync(id);
        if (task == null)
        {
            throw new InvalidOperationException();
        }

        task.IsImportant = !task.IsImportant;
        await _dbContext.SaveChangesAsync();
        return id;
    }
}
