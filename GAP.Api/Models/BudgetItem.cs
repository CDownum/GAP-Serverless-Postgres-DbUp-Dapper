using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GAP.Api.Models;

public class BudgetItem 
{
    public int Id { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime LastModified { get; set; }

    [Required]
    [JsonIgnore]
    public required Budget Budget { get; set; }
}