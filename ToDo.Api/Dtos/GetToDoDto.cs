namespace ToDo.Api.Dtos;

public record ToDoDto(
    Guid Id,
    string Body,
    DateTime CreateTime,
    bool IsCompleted,
    bool IsImportant
    );