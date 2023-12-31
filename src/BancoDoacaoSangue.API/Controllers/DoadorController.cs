﻿using BancoDoacaoSangue.Application.Commands.AtualizaDoador;
using BancoDoacaoSangue.Application.Commands.CadastrarDoador;
using BancoDoacaoSangue.Application.Queries.BuscarDoador;
using BancoDoacaoSangue.Application.Queries.BuscarDoadores;
using BancoDoacaoSangue.Application.Queries.BuscarDoadorPorId;
using BancoDoacaoSangue.Application.ViewModels;
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


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(CadastrarDoadorCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        [ProducesResponseType<List<DoacaoViewModel>>(StatusCodes.Status200OK)]
        [HttpGet("{id}/doacoes")]
        public async Task<IActionResult> BuscarDoacoes(int id)
        {
            var query = new BuscarDoacoesPorDoadorQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [ProducesResponseType<List<DoadorViewModel>>(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> BuscarDoadores()
        {
            var query = new BuscarDoadoresQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<IActionResult> AtualizaDoador(AtualizaDoadorCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [ProducesResponseType<DoacaoViewModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarDoador(int id)
        {
            var query = new BuscarDoadorPorIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
