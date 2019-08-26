using NineChallenge.Business.Exceptions;
using NineChallenge.Business.Handlers;
using NineChallenge.Business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NineChallenge.Business.Tests
{
    public class TvShowRequestHandlerTests
    {
        [Theory]
        [InlineData("first expected show", "second expected show")]
        public async Task Handle_ShouldReturnOnlyShowsWithEpisodes(string firstExpectedTitle, string secondExpectedTitle)
        {
            var handler = new TvShowRequestHandler();
            var request = new TvShowRequest()
            {
                Payload = new List<TvShowRequestDetails>()
                {
                    new TvShowRequestDetails() {Title = "ignored show 1", EpisodeCount = 0, Drm = true},
                    new TvShowRequestDetails() {Title = firstExpectedTitle, EpisodeCount = 100, Drm = true},
                    new TvShowRequestDetails() {Title = secondExpectedTitle, EpisodeCount = 1, Drm = true},
                    new TvShowRequestDetails() {Title = "ignored show 1", EpisodeCount = -1, Drm = true }
                }
            };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result?.Response);
            Assert.Equal(2, result.Response.Count);
            Assert.Equal(firstExpectedTitle, result.Response[0].Title);
            Assert.Equal(secondExpectedTitle, result.Response[1].Title);

        }

        [Theory]
        [InlineData("first expected show")]
        public async Task Handle_ShouldReturnOnlyShowsWithDrmEnabled(string firstExpectedTitle)
        {
            var handler = new TvShowRequestHandler();
            var request = new TvShowRequest()
            {
                Payload = new List<TvShowRequestDetails>()
                {
                    new TvShowRequestDetails() {Title = "ignored show 1", Drm = false, EpisodeCount = 100},
                    new TvShowRequestDetails() {Title = firstExpectedTitle, Drm = true, EpisodeCount = 100},
                    new TvShowRequestDetails() {Title = "ignored show 2", Drm = false, EpisodeCount = 100},
                    new TvShowRequestDetails() {Title = "ignored show 3", Drm = false, EpisodeCount = 100}
                }
            };

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result?.Response);
            Assert.Single(result.Response);
            Assert.Equal(firstExpectedTitle, result.Response[0].Title);

        }

        [Fact]
        public async Task Handle_ShouldThrowValidationExceptionForInvalidData()
        {
            var handler = new TvShowRequestHandler();
            var request = new TvShowRequest();

            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
