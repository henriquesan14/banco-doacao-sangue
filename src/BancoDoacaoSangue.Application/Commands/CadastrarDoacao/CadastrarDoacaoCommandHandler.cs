using AutoMapper;
using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Exceptions;
using BancoDoacaoSangue.Core.Repositories;
using MediatR;

namespace BancoDoacaoSangue.Application.Commands.CadastrarDoacao
{
    public class CadastrarDoacaoCommandHandler : IRequestHandler<CadastrarDoacaoCommand, int>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CadastrarDoacaoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CadastrarDoacaoCommand request, CancellationToken cancellationToken)
        {
            var doador = await _unitOfWork.Doadores.GetByIdAsync(request.DoadorId!.Value);
            if(doador is null)
            {
                throw new NotFoundException($"Não existe doador com o id {request.DoadorId}");
            }
            var doacoes = await _unitOfWork.Doacoes.GetLastDoacaoByDoador(request.DoadorId.Value);
            ValidaDoacao(doador, doacoes);
            var entity = _mapper.Map<Doacao>(request);
            var result = await _unitOfWork.Doacoes.AddAsync(entity);

            await AtualizaEstoque(doador, request.QuantidadeMl);
            
            await _unitOfWork.CompleteAsync();
            return result.Id;
        }

        private async Task AtualizaEstoque(Doador doador, int? quantidade)
        {
            var estoque = await _unitOfWork.EstoqueSangue.GetByTipoSanguineoFatorRh(doador.TipoSanguineo!, doador.FatorRh!);
            if(estoque == null)
            {
                var novoEstoque = new EstoqueSangue
                {
                    FatorRh = doador.FatorRh,
                    TipoSanguineo = doador.TipoSanguineo,
                    QuantidadeMl = quantidade
                };
                await _unitOfWork.EstoqueSangue.AddAsync(novoEstoque);
                return;
            }
            estoque.QuantidadeMl += quantidade;
            _unitOfWork.EstoqueSangue.UpdateAsync(estoque);
        }

         

        private void ValidaDoacao(Doador doador, List<Doacao> doacoes)
        {
            var dataHoje = DateTime.Now;

            if (doador.Peso < 50)
            {
                throw new DoadorValidationException("Doador não pode ter menos de 50Kg");
            }
            if (doador.MenorDeIdade)
            {
                throw new DoadorValidationException("Doador não pode ser menor de idade");
            }
            if (doacoes.Any() && (doador.Genero!.Equals("M") && dataHoje.Date.Subtract(doacoes[0].CriadoEm.Date).Days < 60))
            {
                throw new DoadorValidationException("Homens só podem doar de 60 em 60 dias");
            }
            if (doacoes.Any() && (doador.Genero!.Equals("F") && dataHoje.Subtract(doacoes[0].CriadoEm).Days < 90))
            {
                throw new DoadorValidationException("Mulheres só podem doar de 90 em 90 dias");
            }
        }

    }
}
