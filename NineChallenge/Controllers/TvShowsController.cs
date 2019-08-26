using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NineChallenge.Business.Exceptions;
using NineChallenge.Business.Models;

namespace NineChallenge.Controllers
{
    [Route("")]
    public class TvShowsController : Controller
    {
        private readonly IMediator _mediator;

        public TvShowsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TvShowRequest request)
        {
            if (request == null)
            {
                return ParsingError();
            }
            try
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (ValidationException)
            {
                return ParsingError();
            }
           
        }

        private IActionResult ParsingError()
        {
            return BadRequest(new { Error = "Could not decode request: JSON parsing failed" });
        }
    }
}
