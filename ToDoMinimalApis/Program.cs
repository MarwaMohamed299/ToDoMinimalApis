using Microsoft.EntityFrameworkCore;
using ToDoMinimalApis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoContext>(opt => opt.UseInMemoryDatabase("TODOList"));

var app = builder.Build();

app.MapGet("/todoitems", async (ToDoContext db) =>
await db.ToDos.ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, ToDoContext db) =>
await db.ToDos.FindAsync(id));

app.MapPut("/todoitems/{id}", async (int id, ToDoContext db, ToDoItem updatedItem) =>
{
    var todo = await db.ToDos.FindAsync(id);
    if (todo is null) return Results.NotFound();

    todo.Name = updatedItem.Name;
    todo.IsComplete = updatedItem.IsComplete;

    await db.SaveChangesAsync();
    return Results.NoContent();
});


app.MapPost("/todoitems", async (ToDoContext db, ToDoItem newItem) =>
{
    db.ToDos.Add(newItem);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{newItem.Id}", newItem);
});

app.MapDelete("/todoitems/{id}", async (int id, ToDoContext db) =>
{
    var todo = await db.ToDos.FindAsync(id);
    if (todo is null) return Results.NotFound();

    db.ToDos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.NoContent();
});





app.MapGet("/", () => "Hello World!");

app.Run();
