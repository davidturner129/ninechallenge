using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NineChallenge.Business.Models
{
    public class TvShowRequest : IRequest<TvShowResponse>
    {
        public List<TvShowRequestDetails> Payload { get; set; }
    }
}
