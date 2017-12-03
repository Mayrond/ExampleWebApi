using System.Collections.Generic;
using ExampleWebApi.Views.General;

namespace ExampleWebApi.Views
{
    public class UserView : BaseId  
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<UserGroupView> Groups { get; set; }

    }
}