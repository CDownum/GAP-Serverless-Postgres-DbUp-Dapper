using Newtonsoft.Json;

namespace GAP.Api.Models;

public class SalesGoalQuarter
{
    public Guid Id { get; set; }

    public int Quarter { get; set; }

    public int SalesGoalId { get; set; } // Required foreign key property

    [JsonIgnore]
    public required SalesGoal SalesGoal { get; set; }

    public int GrossSalesNeeded { get; set; }

    public decimal PercentQuarterSales => GrossSalesNeeded % SalesGoal.GrossSalesNeeded;

    public int Referral { get; set; }
    public int SelfOriginating { get; set; }
    public int Internet { get; set; }
    public int Realtor { get; set; }
    public int WalkIn { get; set; }
    public int FollowUp { get; set; }
    public DateTime LastModified { get; set; }
}