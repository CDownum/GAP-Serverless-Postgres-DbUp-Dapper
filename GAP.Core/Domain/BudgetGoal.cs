namespace GAP.Core.Domain;

public class BudgetGoal : DomainModel
{
    public required string Description { get; set; }

    public decimal Amount { get; set; }

    public DateTime LastModified { get; set; }
}