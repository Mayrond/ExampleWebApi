using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;
using AutoMapper;
using ExampleWebApi.Controllers;
using ExampleWebApi.Infrastructure;
using ExampleWebApi.Models;
using ExampleWebApi.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace ExampleWebApi.Tests.Controllers
{
    [TestFixture]
    public class CommertialProposalControllerTests
    {
        private CommertialProposalController _commertialProposalController;
        private List<CommertialProposal> _commertialProposalsMock;

        [SetUp]
        public void SetUp()
        {
            DependencyInjector.Initialize();
            var mapper = DependencyInjector.Resolve<IMapper>();
            _commertialProposalsMock = new List<CommertialProposal>
            {
                new CommertialProposal{Id = 1, Number = "1"},
                new CommertialProposal{Id = 2, Number = "2"},
                new CommertialProposal{Id = 3, Number = "3"},
            };
            var repository = new Mock<IRepositoryService<CommertialProposal>>();
            repository.Setup(rep => rep.GetAll()).Returns(_commertialProposalsMock.AsQueryable);
            _commertialProposalController = new CommertialProposalController(repository.Object, mapper);

        }

        [Test]
        public void GetAll_ShouldReturnAllEntities()
        {
            _commertialProposalController.Request = new HttpRequestMessage(HttpMethod.Get, string.Empty);
            var commertialProposals = _commertialProposalController.GetAll(GetEmptyODataQueryOptions<CommertialProposal>());
            Assert.AreEqual(3, commertialProposals.Items.Count());
            Assert.AreEqual(_commertialProposalsMock[0].Number, commertialProposals.Items.ToList()[0].Number);
            Assert.AreEqual(_commertialProposalsMock[1].Number, commertialProposals.Items.ToList()[1].Number);
            Assert.AreEqual(_commertialProposalsMock[2].Number, commertialProposals.Items.ToList()[2].Number);
        }

        private ODataQueryOptions<TEntity> GetEmptyODataQueryOptions<TEntity>() where TEntity : class
        {
            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<TEntity>(typeof(TEntity).FullName?.Replace(".", ""));
            var context = new ODataQueryContext(modelBuilder.GetEdmModel(), typeof(TEntity));
            return new ODataQueryOptions<TEntity>(context, _commertialProposalController.Request);
        }
    }
}