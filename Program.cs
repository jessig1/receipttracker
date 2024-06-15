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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var budgetItems = app.MapGroup("/budgetitems");

budgetItems.MapGet("/", GetAllBudgets);
budgetItems.MapGet("/complete", GetCompleteBudgets);
budgetItems.MapGet("/{id}", GetBudget);
budgetItems.MapPost("/", CreateBudget);
budgetItems.MapPut("/{id}", UpdateBudget);
budgetItems.MapDelete("/{id}", DeleteBudget);


static async Task<IResult> GetAllBudgets(BudgetDb db)
{
    return TypedResults.Ok(await db.Budgets.ToArrayAsync());
}

static async Task<IResult> GetCompleteBudgets(BudgetDb db)
{
    return TypedResults.Ok(await db.Budgets.Where(t => t.IsComplete).ToListAsync());
}

static async Task<IResult> GetBudget(int id, BudgetDb db)
{
    return await db.Budgets.FindAsync(id)
        is Budget budget
            ? TypedResults.Ok(budget)
            : TypedResults.NotFound();
}

static async Task<IResult> CreateBudget(Budget budget, BudgetDb db)
{
    db.Budgets.Add(budget);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/budgetitems/{budget.Id}", budget);
}

static async Task<IResult> UpdateBudget(int id, Budget inputBudget, BudgetDb db)
{
    var Budget = await db.Budgets.FindAsync(id);

    if (Budget is null) return TypedResults.NotFound();

    Budget.Name = inputBudget.Name;
    Budget.IsComplete = inputBudget.IsComplete;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteBudget(int id, BudgetDb db)
{
    if (await db.Budgets.FindAsync(id) is Budget budget)
    {
        db.Budgets.Remove(budget);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}
app.UseHttpsRedirection();

app.UseAuthorization();

// app.MapControllers();

app.Run();
