
namespace GAP.Core.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public Company Company { get; set; }
        public Role Role { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";
        public decimal GrossAnnualIncome { get; set; }
        public required string ReportingManager { get; set; }
        public required bool Salaried { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Salutation { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? WorkPhone { get; set; }
        public string? Cell { get; set; }
        public required string Email { get; set; }
    }
}
