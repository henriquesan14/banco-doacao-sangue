using BancoDoacaoSangue.Application.Commands.CadastrarDoador;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BancoDoacaoSangue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoadorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoadorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CadastrarDoadorCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
