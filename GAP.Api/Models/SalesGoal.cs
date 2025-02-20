using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GAP.Api.Models;

public class SalesGoal
{
    public Guid Id { get; set; }

    public int Year { get; set; }
    public decimal AverageSalesPrice { get; set; }

    public decimal CommissionRate { get; set; }
    public decimal AverageCommision { get; set; }
    public decimal AverageLossRatio { get; set; }
    public decimal NetSalesClosed { get; set; }
    public decimal NetSalesNeeded { get; set; }
    public decimal GrossSalesNeeded { get; set; }
    
    public List<SalesGoalQuarter> SalesGoalQuarters { get; set; } = [];

    public DateTime LastModified { get; set; }

    public required User User { get; set; }
}