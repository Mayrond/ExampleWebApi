using System.Collections.Generic;
using ExampleWebApi.Views.General;

namespace ExampleWebApi.Views
{
    public class ContractorView : BaseId
    {
        public string Name { get; set; }
        public List<UserView> Curators { get; set; }
    }
}