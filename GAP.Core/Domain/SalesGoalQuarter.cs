namespace GAP.Core.Domain;

public class SalesGoalQuarter : DomainModel
{
    public int Quarter { get; set; }
    public int GrossSalesNeeded { get; set; }
    public int SalesGoalTotalGrossSalesNeeded { get; set; }
    public decimal PercentQuarterSales => GrossSalesNeeded % SalesGoalTotalGrossSalesNeeded;
    public int Referral { get; set; }
    public int SelfOriginating { get; set; }
    public int Internet { get; set; }
    public int Realtor { get; set; }
    public int WalkIn { get; set; }
    public int FollowUp { get; set; }
    public DateTime LastModified { get; set; }
}