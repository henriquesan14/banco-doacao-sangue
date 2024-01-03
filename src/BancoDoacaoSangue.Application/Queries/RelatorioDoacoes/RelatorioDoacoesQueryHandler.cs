using AutoMapper;
using BancoDoacaoSangue.Application.ViewModels;
using BancoDoacaoSangue.Core.Repositories;
using MediatR;

namespace BancoDoacaoSangue.Application.Queries.RelatorioDoacoes
{
    public class RelatorioDoacoesQueryHandler : IRequestHandler<RelatorioDoacoesQuery, List<RelatorioDoacaoViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RelatorioDoacoesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RelatorioDoacaoViewModel>> Handle(RelatorioDoacoesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Doacoes.GetRelatorioDoacoes();
            var viewModel = _mapper.Map<List<RelatorioDoacaoViewModel>>(result);
            return viewModel;
        }
    }
}
