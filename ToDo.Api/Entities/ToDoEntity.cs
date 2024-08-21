namespace ToDo.Api.Entities;

public class ToDoEntity
{
    public Guid Id { get; set; }
    public required string Body { get; set; } = string.Empty;
    public required DateTime CreateTime { get; set; }
    public bool IsCompleted { get; set; } = false;
    public bool IsImportant { get; set; } = false;
}
