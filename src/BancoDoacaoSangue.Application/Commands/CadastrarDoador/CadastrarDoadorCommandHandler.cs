using AutoMapper;
using BancoDoacaoSangue.Core.DTOs;
using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Exceptions;
using BancoDoacaoSangue.Core.Repositories;
using BancoDoacaoSangue.Infra.Services;
using MediatR;

namespace BancoDoacaoSangue.Application.Commands.CadastrarDoador
{
    public class CadastrarDoadorCommandHandler : IRequestHandler<CadastrarDoadorCommand, int>
    {
        private readonly IDoadorRepository _doadorRepository;
        private readonly IMapper _mapper;
        private readonly ICepService _cepService;

        public CadastrarDoadorCommandHandler(IDoadorRepository doadorRepository, IMapper mapper, ICepService cepService)
        {
            _doadorRepository = doadorRepository;
            _mapper = mapper;
            _cepService = cepService;
        }

        public async Task<int> Handle(CadastrarDoadorCommand request, CancellationToken cancellationToken)
        {
            var doadorExiste = await _doadorRepository.GetAsync(e => e.Email!.Equals(request.Email));
            if (doadorExiste.Any())
            {
                throw new DoadorJaExisteException("Já existe um doador com este email");
            }
            ResponseCepDto responseCep = await _cepService.BuscaCep(request.Cep!);

            var entity = _mapper.Map<Doador>(request);
            entity.Endereco?.SetEnderecoViaCep(responseCep);
            var result = await _doadorRepository.AddAsync(entity);
            return result.Id;
        }
    }
}
