namespace receipttracker
{
    public class Budget
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Amount { get; set; }

        public bool IsComplete { get; set; }
        public string? Secret { get; set; }
    }
}
