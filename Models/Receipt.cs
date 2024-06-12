namespace receipttracker.Models
{
    public class Receipt
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
