using ToDo.Api.Dtos;
using ToDo.Api.Entities;

namespace ToDo.Api.Mapping;

public static class ToDoMapping {
    public static ToDoDto ToDto(this ToDoEntity entity)
    {
        return new ToDoDto(
            Id:  entity.Id,
            Body: entity.Body,
            CreateTime: entity.CreateTime,
            IsCompleted: entity.IsCompleted,
            IsImportant: entity.IsImportant);
    }

    public static ToDoEntity ToEntity(this CreateToDoDto dto)
    {
        return new ToDoEntity
        {
            Body = dto.Body,
            CreateTime = DateTime.Now,
        };
    }
}