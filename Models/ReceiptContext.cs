using Microsoft.EntityFrameworkCore;

namespace receipttracker.Models
{
    public class ReceiptContext(DbContextOptions<ReceiptContext> options) : DbContext(options)
    {
        public DbSet<Receipt> Receipts { get; set; } = null!;
    }
}
