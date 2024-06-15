namespace receipttracker
{
    public class BudgetItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Amount { get; set; }

        public bool IsComplete { get; set; }
  

        public BudgetItemDTO() { }
        public BudgetItemDTO(Budget budgetItem) =>
            (Id, Name, Description, Amount, IsComplete) = (budgetItem.Id, budgetItem.Name, budgetItem.Description, budgetItem.Amount, budgetItem.IsComplete);
    }
}
