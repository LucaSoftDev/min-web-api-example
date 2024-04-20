using Microsoft.EntityFrameworkCore;
using min_web_api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

var todoItems = app.MapGroup("/todo-items");

todoItems.MapGet("/", GetAllTodos);
todoItems.MapGet("/complete", GetCompleteTodos);
todoItems.MapGet("/{id:int}", GetTodo);
todoItems.MapPost("/", CreateTodo);
todoItems.MapPut("/{id:int}", UpdateTodo);
todoItems.MapDelete("/{id:int}", DeleteTodo);

app.Run();
return;
static async Task<IResult> GetAllTodos(TodoDb db)
{
    return TypedResults.Ok(await db.Todos
        .Select(x => new TodoItemDto(x))
        .ToListAsync());
}

static async Task<IResult> GetCompleteTodos(TodoDb db)
{
    return TypedResults.Ok(await db.Todos.Where(t => t.IsCompleted)
            .Select(x => new TodoItemDto(x))
            .ToListAsync());
}

static async Task<IResult> GetTodo(int id, TodoDb db)
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(new TodoItemDto(todo));
}

static async Task<IResult> CreateTodo(TodoItemDto todoItemDto, TodoDb db)
{
    var todo = new Todo
    {
        Name = todoItemDto.Name,
        IsCompleted = todoItemDto.IsCompleted
    };
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/todoItemDto-items/{todoItemDto.Id}", new TodoItemDto(todo));
}

static async Task<IResult> UpdateTodo(int id, TodoItemDto todoItemDto, TodoDb db)
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return TypedResults.NotFound();

    todo.Name = todoItemDto.Name;
    todo.IsCompleted = todoItemDto.IsCompleted;

    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> DeleteTodo(int id, TodoDb db)
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null)
    {
        return TypedResults.NotFound();
    }
    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}