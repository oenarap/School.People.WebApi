using School.People.Core.Attributes;
using System;

namespace School.People.WebApi.Models
{
    public class Education : IEducation
    {
        public string Level { get; set; }
        public string SchoolName { get; set; }
        public string DegreeCourse { get; set; }
        public string IfGraduatedYearGraduated { get; set; }
        public string IfNotGraduatedHighestLevelOrUnitsEarned { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string ScholarshipOrHonorsReceived { get; set; }
        public bool IsOngoing { get; set; }
        public Guid Index { get; set; }
        public Guid Id { get; set; }
    }
}
