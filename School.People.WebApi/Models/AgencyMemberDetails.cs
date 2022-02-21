using School.People.Core.Attributes;
using System;

namespace School.People.WebApi.Models
{
    public class AgencyMemberDetails : IAgencyMemberDetails
    {
        public string AgencyId { get; set; }
        public string GsisIdNumber { get; set; }
        public string PagIbigIdNumber { get; set; }
        public string PhilhealthNumber { get; set; }
        public string SssNumber { get; set; }
        public string Tin { get; set; }
        public Guid Id { get; set; }
    }
}
