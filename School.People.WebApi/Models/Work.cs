using System;
using School.People.Core.Attributes;

namespace School.People.WebApi.Models
{
    public class Work : IWork
    {
        public decimal MonthlySalary { get; set; }
        public string SalaryGradeAndStepIncrement { get; set; }
        public string StatusOfAppointment { get; set; }
        public bool IsGovernmentService { get; set; }
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
