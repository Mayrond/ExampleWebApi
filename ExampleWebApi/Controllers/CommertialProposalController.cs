using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using AutoMapper;
using SingleProjectEfDefault.Infrastructure;
using SingleProjectEfDefault.Models;
using SingleProjectEfDefault.Services.Interfaces;
using SingleProjectEfDefault.Views;

namespace SingleProjectEfDefault.Controllers
{
    public class CommertialProposalController : ApiController
    {
        private readonly IRepositoryService<CommertialProposal> _repository;
        private readonly IRepositoryService<User> _userRepositoryService;
        private readonly IMapper _mapper;

        public CommertialProposalController(IRepositoryService<CommertialProposal> repository, IMapper mapper, IRepositoryService<User> userRepositoryService)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepositoryService = userRepositoryService;
        }

        public PageResult<CommertialProposalView> GetAll(ODataQueryOptions<CommertialProposal> options)
        {
            return _repository.GetAll().QueryableForOData<CommertialProposal, CommertialProposalView>(options,Request);
        }


        [Route("{id:int}")]
        [HttpGet]
        public CommertialProposalView Get(int id)
        {
            return _mapper.Map<CommertialProposalView>(_repository.Find(id));
        }

        [HttpPost]
        public string Test()
        {
            var user = new User{Age = 1, Name = "asda", Groups = new List<UserGroup>{new UserGroup{Name = "asdsa"}}};
            var retrnedUser = _userRepositoryService.Insert(user);
            var getedUser = _userRepositoryService.Find(1);
            return "ok";

        }
    }
}