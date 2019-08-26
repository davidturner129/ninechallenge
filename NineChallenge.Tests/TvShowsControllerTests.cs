using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using NineChallenge.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NineChallenge.Business.Exceptions;
using System.Threading;
using NineChallenge.Business.Models;

namespace NineChallenge.Tests
{
    public class TvShowsControllerTests
    {

        [Fact]
        public async Task Post_ShouldReturnOkIfNoException()
        {
            var controller = new TvShowsController(Substitute.For<IMediator>());

            var response = await controller.Post(new Business.Models.TvShowRequest());

            var okResult = response as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequestIfException()
        {
            var mock = Substitute.For<IMediator>();
            mock.Send(new Business.Models.TvShowRequest(), CancellationToken.None).ReturnsForAnyArgs<TvShowResponse>(x => { throw new ValidationException(); });
            var controller = new TvShowsController(mock);

            var response = await controller.Post(new Business.Models.TvShowRequest());

            var badRequestResult = response as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequestIfRequestIsNull()
        {
            var controller = new TvShowsController(Substitute.For<IMediator>());

            var response = await controller.Post(null);

            var badRequestResult = response as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

    }
}
