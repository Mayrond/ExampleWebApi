using System.Collections.Generic;
using ExampleWebApi.Views.General;

namespace ExampleWebApi.Views
{
    public class FirmView : BaseId
    {
        public string Name { get; set; }
        public ICollection<ContractorView> Contractors { get; set; }
    }
}