using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Api.Entities;

namespace ToDo.Api.Configurations;

public class ToDosConfiguration : IEntityTypeConfiguration<ToDoEntity>
{
    public void Configure(EntityTypeBuilder<ToDoEntity> builder)
    {
        builder.HasKey(task => task.Id);
        builder.Property(task => task.Body).IsRequired().HasMaxLength(250);
    }
}