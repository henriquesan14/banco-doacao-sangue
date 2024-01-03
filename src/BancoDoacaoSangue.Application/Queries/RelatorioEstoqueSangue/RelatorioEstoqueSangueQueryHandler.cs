using BancoDoacaoSangue.Core.DTOs;
using BancoDoacaoSangue.Core.Repositories;
using MediatR;

namespace BancoDoacaoSangue.Application.Queries.RelatorioEstoqueSangue
{
    public class RelatorioEstoqueSangueQueryHandler : IRequestHandler<RelatorioEstoqueSangueQuery, List<RelatorioEstoqueSangueDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RelatorioEstoqueSangueQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RelatorioEstoqueSangueDto>> Handle(RelatorioEstoqueSangueQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.EstoqueSangue.GetByQuantidadePorTipoSanguineo();
            return result;
        }
    }
}
