using Microsoft.EntityFrameworkCore;
using receipttracker;
using receipttracker.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BudgetDb>(opt =>
    opt.UseInMemoryDatabase("BudgetList"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/", () => "Hello World!");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/budgetitems", async (BudgetDb db) =>
    await db.Budgets.ToListAsync());

app.MapGet("/budgetitems/complete", async (BudgetDb db) =>
    await db.Budgets.Where(t => t.IsComplete).ToListAsync());

app.MapGet("/budgetitems/{id}", async (int id, BudgetDb db) =>
    await db.Budgets.FindAsync(id)
        is Budget budget
            ? Results.Ok(budget)
            : Results.NotFound());

app.MapPost("/budgetitems", async (Budget budget, BudgetDb db) =>
{
    db.Budgets.Add(budget);
    await db.SaveChangesAsync();

    return Results.Created($"/budgetitems/{budget.Id}", budget);
});

app.MapPut("/budgetitems/{id}", async (int id, Budget inputBudget, BudgetDb db) =>
{
    var Budget = await db.Budgets.FindAsync(id);

    if (Budget is null) return Results.NotFound();

    Budget.Name = inputBudget.Name;
    Budget.IsComplete = inputBudget.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/budgetitems/{id}", async (int id, BudgetDb db) =>
{
    if (await db.Budgets.FindAsync(id) is Budget Budget)
    {
        db.Budgets.Remove(Budget);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.UseHttpsRedirection();

app.UseAuthorization();

// app.MapControllers();

app.Run();
