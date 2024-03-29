﻿using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using AutoMapper;
using ExampleWebApi.Infrastructure;
using ExampleWebApi.Models;
using ExampleWebApi.Services.Interfaces;
using ExampleWebApi.Views;

namespace ExampleWebApi.Controllers
{
    [RoutePrefix("api/CommertialProposal")]
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

        [HttpGet]
        [Route("Old")]
        public PageResult<CommertialProposalView> Test(ODataQueryOptions<CommertialProposal> options)
        {
            return _repository.GetAll().QueryableForODataOld<CommertialProposal, CommertialProposalView>(options, Request);
        }
    }
}