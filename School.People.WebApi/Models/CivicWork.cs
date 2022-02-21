using School.People.Core.Attributes;
using System;

namespace School.People.WebApi.Models
{
    public class CivicWork : ICivicWork
    {
        public double TotalHoursWorked { get; set; }
        public Guid Index { get; set; }
        public Guid Id { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public bool IsOngoing { get; set; }
        public Guid? LocationAddressId { get; set; }
        public string PositionTitle { get; set; }
        public string EmployerOrganizationOrBusinessName { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
