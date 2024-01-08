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

        public AtualizaDoadorCommandHandler(IUnitOfWork unitOfWork, ICepService cepService)
        {
            _unitOfWork = unitOfWork;
            _cepService = cepService;
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
