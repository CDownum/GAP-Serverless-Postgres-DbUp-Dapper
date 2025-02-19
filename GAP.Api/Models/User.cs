using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GAP.Api.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public Role Role { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [NotMapped]
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

        public List<Budget>? Budgets { get; set; }
    }
}
