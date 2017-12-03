using ExampleWebApi.Models.General;

namespace ExampleWebApi.Models
{
    public class CommertialProposal : BaseEntity
    {
        public string Number { get; set; }
        public virtual Contractor To { get; set; }
        public virtual Contractor From { get; set; }
    }
}