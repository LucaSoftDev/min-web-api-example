using Microsoft.EntityFrameworkCore;

namespace min_web_api
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }
}
