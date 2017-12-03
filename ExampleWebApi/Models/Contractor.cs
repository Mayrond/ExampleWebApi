using System.Collections.Generic;
using ExampleWebApi.Models.General;

namespace ExampleWebApi.Models
{
    public class Contractor : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<User> Curators { get; set; }
        public virtual Firm Firm { get; set; }
    }
}