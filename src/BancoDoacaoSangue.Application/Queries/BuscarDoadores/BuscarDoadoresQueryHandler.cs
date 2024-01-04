using AutoMapper;
using BancoDoacaoSangue.Application.ViewModels;
using BancoDoacaoSangue.Core.Repositories;
using MediatR;

namespace BancoDoacaoSangue.Application.Queries.BuscarDoadores
{
    public class BuscarDoadoresQueryHandler : IRequestHandler<BuscarDoadoresQuery, List<DoadorViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BuscarDoadoresQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DoadorViewModel>> Handle(BuscarDoadoresQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Doadores.GetAsync(includeString:"Endereco");
            var viewModel = _mapper.Map<List<DoadorViewModel>>(result);
            return viewModel;
        }
    }
}
