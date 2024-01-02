using BancoDoacaoSangue.Application.Commands.CadastrarDoacao;
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
        public async Task<IActionResult> Add([FromBody] CadastrarDoacaoCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
