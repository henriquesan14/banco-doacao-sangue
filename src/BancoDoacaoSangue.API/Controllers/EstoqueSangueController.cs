using BancoDoacaoSangue.Application.Queries.RelatorioEstoqueSangue;
using BancoDoacaoSangue.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BancoDoacaoSangue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueSangueController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EstoqueSangueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType<List<RelatorioEstoqueSangueDto>>(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> RelatorioEstoqueSangue()
        {
            var query = new RelatorioEstoqueSangueQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
