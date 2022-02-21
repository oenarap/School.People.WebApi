using School.People.Core.Attributes;
using System;

namespace School.People.WebApi.Models
{
    public class DateOfBirth : IDateOfBirth
    {
        public DateTimeOffset? Birthdate { get; set; }
        public Guid Id { get; set; }
    }
}
