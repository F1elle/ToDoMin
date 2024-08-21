using Microsoft.EntityFrameworkCore;
using ToDo.Api.Configurations;
using ToDo.Api.Entities;

namespace ToDo.Api.Database;

public class ToDoDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options, IConfiguration configuration) : base(options) 
    { 
        _configuration = configuration;
    }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configuration.GetConnectionString(nameof(ToDoDbContext)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ToDosConfiguration());
    }

    public DbSet<ToDoEntity> ToDos { get; set; } 
}
