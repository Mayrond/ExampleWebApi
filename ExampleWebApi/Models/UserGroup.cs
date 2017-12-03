using System.Collections.Generic;
using ExampleWebApi.Models.General;

namespace ExampleWebApi.Models
{
    public class UserGroup : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}