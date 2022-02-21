using School.People.Core;
using System;

namespace School.People.WebApi.Models
{
    public class Person : IPerson
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string NameExtension { get; set; }
        public string Title { get; set; }
        public Guid Id { get; set; }
    }
}
