using Microsoft.EntityFrameworkCore;
using receipttracker;


    class BudgetDb : DbContext
    {
        public BudgetDb(DbContextOptions<BudgetDb> options)
            : base(options) { }
        public DbSet<Budget> Budgets => Set<Budget>();
    }
