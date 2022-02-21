using School.People.Core.Attributes;
using System;

namespace School.People.WebApi.Models
{
    public class ContactDetails : IContactDetails
    {
        public string EmailAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public Guid Id { get; set; }
    }
}
