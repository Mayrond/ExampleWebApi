using System.Collections.Generic;
using ExampleWebApi.Models.General;

namespace ExampleWebApi.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public virtual ICollection<UserGroup> Groups { get; set; }
        public virtual ICollection<Contractor> CuratedContractors { get; set; }
    }
}