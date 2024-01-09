using AutoMapper;
using BancoDoacaoSangue.Core.DTOs;
using BancoDoacaoSangue.Core.Exceptions;
using BancoDoacaoSangue.Core.Repositories;
using BancoDoacaoSangue.Infra.Services;
using MediatR;

namespace BancoDoacaoSangue.Application.Commands.AtualizaDoador
{
    public class AtualizaDoadorCommandHandler : IRequestHandler<AtualizaDoadorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICepService _cepService;
        private readonly IMapper _mapper;

        public AtualizaDoadorCommandHandler(IUnitOfWork unitOfWork, ICepService cepService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cepService = cepService;
            _mapper = mapper;
        }

        public async Task Handle(AtualizaDoadorCommand request, CancellationToken cancellationToken)
        {
            var doador = await _unitOfWork.Doadores.GetByIdIncludeEndereco(request.Id);
            if(doador is null)
            {
                throw new NotFoundException($"Não existe doador com id: {request.Id}");
            }
            var emailExiste = await _unitOfWork.Doadores.GetByEmail(request.Email!);
            if (emailExiste != null && emailExiste.Id != doador.Id)
            {
                throw new DoadorJaExisteException("Já existe um doador com este email");
            }
            _mapper.Map(request, doador);
            if(doador.Endereco?.Cep != request.Cep)
            {
                ResponseCepDto responseCep = await _cepService.BuscaCep(request.Cep!);
                doador.Endereco?.SetEnderecoViaCep(responseCep);

            }
            _unitOfWork.Doadores.UpdateAsync(doador);
            await _unitOfWork.CompleteAsync();
        }
    }
}
