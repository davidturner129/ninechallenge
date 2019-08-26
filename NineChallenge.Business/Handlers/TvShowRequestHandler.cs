using MediatR;
using NineChallenge.Business.Exceptions;
using NineChallenge.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NineChallenge.Business.Handlers
{
    public class TvShowRequestHandler : IRequestHandler<TvShowRequest, TvShowResponse>
    {
        public Task<TvShowResponse> Handle(TvShowRequest request, CancellationToken cancellationToken)
        {
            if (request?.Payload == null)
            {
                throw new ValidationException();
            }
            var shows = request.Payload
                .Where(x => x.EpisodeCount > 0 && x.Drm)
                .Select(y => new TvShowDetails() { Title = y.Title, Slug = y.Slug, Image = y.Image?.ShowImage })
                .ToList();

            return Task.FromResult(new TvShowResponse()
            {
                Response = shows
            });
        }
    }
}
