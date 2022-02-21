using School.People.Core.Attributes;
using System;

namespace School.People.WebApi.Models
{
    public class Citizenship : ICitizenship
    {
        public string DualCitizenshipMode { get; set; }
        public string DualCitizenship { get; set; }
        public string Country { get; set; }
        public Guid Id { get; set; }
    }
}
