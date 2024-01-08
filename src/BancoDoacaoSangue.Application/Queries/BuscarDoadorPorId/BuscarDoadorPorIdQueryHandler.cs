using AutoMapper;
using BancoDoacaoSangue.Application.ViewModels;
using BancoDoacaoSangue.Core.Exceptions;
using BancoDoacaoSangue.Core.Repositories;
using MediatR;

namespace BancoDoacaoSangue.Application.Queries.BuscarDoadorPorId
{
    public class BuscarDoadorPorIdQueryHandler : IRequestHandler<BuscarDoadorPorIdQuery, DoadorViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BuscarDoadorPorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DoadorViewModel> Handle(BuscarDoadorPorIdQuery request, CancellationToken cancellationToken)
        {
            var doador = await _unitOfWork.Doadores.GetByIdIncludeEndereco(request.Id);
            if(doador is null)
            {
                throw new NotFoundException($"Não existe um doador com id: {request.Id}");
            }
            var viewModel = _mapper.Map<DoadorViewModel>(doador);
            return viewModel;
        }
    }
}
