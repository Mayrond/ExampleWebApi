using ExampleWebApi.Views.General;

namespace ExampleWebApi.Views
{
    public class CommertialProposalView : BaseId
    {
        public string Number { get; set; }
        public ContractorView To { get; set; }
        public ContractorView From { get; set; }
    }
}