using BancoDoacaoSangue.Application.Commands.CadastrarDoacao;
using BancoDoacaoSangue.Application.Queries.RelatorioDoacoes;
using BancoDoacaoSangue.Application.ViewModels;
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

        [ProducesResponseType<List<DoacaoViewModel>>(StatusCodes.Status200OK)]
        [ProducesResponseType<List<DoacaoViewModel>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<List<DoacaoViewModel>>(StatusCodes.Status200OK)]
        [ProducesResponseType<List<DoacaoViewModel>>(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarDoacaoCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [ProducesResponseType<List<RelatorioDoacaoViewModel>>(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> RelatorioDoacoes()
        {
            var query = new RelatorioDoacoesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
