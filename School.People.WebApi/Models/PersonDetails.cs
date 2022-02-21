using School.People.Core.Attributes;
using System;

namespace School.People.WebApi.Models
{
    public class PersonDetails : IPersonDetails
    {
        public string Sex { get; set; }
        public string CivilStatus { get; set; }
        public string OtherCivilStatus { get; set; }
        public double HeightInCentimeters { get; set; }
        public double WeightInKilograms { get; set; }
        public string BloodType { get; set; }
        public Guid Id { get; set; }
    }
}
