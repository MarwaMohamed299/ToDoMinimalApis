using Microsoft.EntityFrameworkCore;

namespace ToDoMinimalApis
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
            
        }
        public DbSet <ToDoItem> ToDos { get; set; }
}
}
 