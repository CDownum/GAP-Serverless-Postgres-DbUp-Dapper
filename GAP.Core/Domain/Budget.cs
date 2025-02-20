namespace GAP.Core.Domain;

public class Budget
{
    public Guid Id { get; set; }

    public int Year { get; set; }
    public decimal HealthCareContribution { get; set; }

    public decimal FourO1KContribution { get; set; }

    public required User User { get; set; }
    public List<BudgetItem>? BudgetItems { get; set; } = [];
    public List<BudgetGoal>? BudgetGoals { get; set; } = [];

    public decimal TaxableIncome => (User == null ? 0m : User.GrossAnnualIncome) - FourO1KContribution - HealthCareContribution;

    public decimal NetAnnualIncome =>
        TaxableIncome - FourO1KContribution - HealthCareContribution - FederalTax - StateTax - MedicadeTax - FICATax;

    public decimal FederalTax => TaxableIncome * 0.0145m;

    public decimal StateTax => 0m;

    public decimal MedicadeTax => User.GrossAnnualIncome * 0.0145m;

    public decimal FICATax => User.GrossAnnualIncome * 0.0620m;

    public decimal MonthlySurvivalBudgetTotal => BudgetItems?.Sum(x => x.Amount) ?? 0.0m;

    public decimal SurvivalFullYear => MonthlySurvivalBudgetTotal * 12;

    public decimal AllBudgetGoalCost => BudgetGoals?.Sum(x => x.Amount) + SurvivalFullYear ?? 0.0m;

    public decimal Remainder => NetAnnualIncome - AllBudgetGoalCost;

    public DateTime LastModified { get; set; }
}