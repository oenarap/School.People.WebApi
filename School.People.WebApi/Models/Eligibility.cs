using School.People.Core.Attributes;
using System;

namespace School.People.WebApi.Models
{
    public class Eligibility : IEligibility
    {
        public string EligibilityName { get; set; }
        public double Rating { get; set; }
        public DateTimeOffset? DateOfExamOrConferment { get; set; }
        public Guid? PlaceOfExamConferment { get; set; }
        public string LicenseNumberIfApplicable { get; set; }
        public DateTimeOffset? LicenseDateOfRelease { get; set; }
        public Guid Index { get; set; }
        public Guid Id { get; set; }
    }
}
