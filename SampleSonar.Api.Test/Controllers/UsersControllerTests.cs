using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleSonar.Api.Controllers;
using SampleSonar.Core.Commands;
using SampleSonar.Core.Queries;
using SampleSonar.Data.Entities;
using SampleSonar.Data.Models.Requests;
using SampleSonar.Data.Models.Responses;

namespace SampleSonar.Api.Test.Controllers
{
    public class UsersControllerTests
    {
        private UsersController controller;
        private Mock<IMediator> mediator;

        [SetUp]
        public void Setup()
        {
            this.mediator = new Mock<IMediator>();
            this.controller = new UsersController(this.mediator.Object)
            {
                Url = Mock.Of<IUrlHelper>(),
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            this.controller.ControllerContext.HttpContext.Request.Host = new HostString("http://tempuri.org");
        }

        [Test]
        public async Task Return_List_Success()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<IEnumerable<User>>());
            var result = await controller.Get();
            var objectResult = result as OkObjectResult;
            Assert.That(objectResult?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task Return_GetUser_Success()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<User>());
            var result = await controller.Get(It.IsAny<int>());
            var objectResult = result as OkObjectResult;
            Assert.That(objectResult?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task Return_GetUser_NoContent()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()));
            var result = await controller.Get(It.IsAny<int>());
            var objectResult = result as NoContentResult;
            Assert.That(objectResult?.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public async Task Return_CreateUser_Success()
        {
            mediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<User>());
            var result = await controller.Post(It.IsAny<CreateUserRequest>());
            var objectResult = result as OkObjectResult;
            Assert.That(objectResult?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public Task Return_CreateUser_BadRequest()
        {
            mediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(new BadHttpRequestException("Bad Request")).Verifiable();
            Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(It.IsAny<CreateUserRequest>()));
            return Task.CompletedTask;
        }
    }
}
