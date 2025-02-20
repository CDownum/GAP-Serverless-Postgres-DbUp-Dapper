namespace GAP.Core.Domain;

public class BudgetItem 
{
    public Guid Id { get; set; }

    public required string Description { get; set; }

    public decimal Amount { get; set; }

    public DateTime LastModified { get; set; }
}