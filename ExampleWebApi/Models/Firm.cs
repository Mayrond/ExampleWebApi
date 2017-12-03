using System.Collections.Generic;
using ExampleWebApi.Models.General;

namespace ExampleWebApi.Models
{
    public class Firm : BaseEntity
    {
        public string  Name { get; set; }
        public virtual ICollection<Contractor> Contractors { get; set; }
    }
}