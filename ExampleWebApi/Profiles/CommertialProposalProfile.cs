using AutoMapper;
using ExampleWebApi.Models;
using ExampleWebApi.Views;

namespace ExampleWebApi.Profiles
{
    public class CommertialProposalProfile : Profile
    {
        public CommertialProposalProfile()
        {
            CreateMap<CommertialProposal, CommertialProposalView>();
        }
    }
}