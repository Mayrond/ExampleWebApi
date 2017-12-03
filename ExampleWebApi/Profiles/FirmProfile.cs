using AutoMapper;
using ExampleWebApi.Models;
using ExampleWebApi.Views;

namespace ExampleWebApi.Profiles
{
    public class FirmProfile : Profile
    {
        public FirmProfile()
        {
            CreateMap<Firm, FirmView>();
            CreateMap<Contractor, ContractorView>();
        }
    }
}