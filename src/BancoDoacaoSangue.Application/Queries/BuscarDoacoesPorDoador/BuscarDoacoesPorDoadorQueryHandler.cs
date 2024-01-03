using AutoMapper;
using BancoDoacaoSangue.Application.Queries.BuscarDoador;
using BancoDoacaoSangue.Application.ViewModels;
using BancoDoacaoSangue.Core.Repositories;
using MediatR;

namespace BancoDoacaoSangue.Application.Queries.BuscarDoacoesPorDoador
{
    public class BuscarDoacoesPorDoadorQueryHandler : IRequestHandler<BuscarDoacoesPorDoadorQuery, List<DoacaoViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BuscarDoacoesPorDoadorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DoacaoViewModel>> Handle(BuscarDoacoesPorDoadorQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Doacoes.GetAsync(d => d.DoadorId.Equals(request.DoadorId));
            var viewModel = _mapper.Map<List<DoacaoViewModel>>(list);
            return viewModel;
        }
    }
}
