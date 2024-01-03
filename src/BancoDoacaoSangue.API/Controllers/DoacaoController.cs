using BancoDoacaoSangue.Application.Commands.CadastrarDoacao;
using BancoDoacaoSangue.Application.Queries.RelatorioDoacoes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BancoDoacaoSangue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoacaoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarDoacaoCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> RelatorioDoacoes()
        {
            var query = new RelatorioDoacoesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
