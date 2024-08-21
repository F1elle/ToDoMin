using ToDo.Api.Dtos;

namespace ToDo.Api.Abstractions;

public interface IToDosRepository
{
    Task<List<ToDoDto>> GetAll();
    Task<ToDoDto> GetById(Guid id);
    Task<ToDoDto> Create(CreateToDoDto newToDo);
    Task<Guid> Update(Guid id, CreateToDoDto updatedToDo);
    Task<Guid> Delete(Guid id);
    Task<Guid> MarkCompleted(Guid id);
    Task<Guid> MarkImportant(Guid id);
}