using AutoMapper;
using BancoDoacaoSangue.Core.Entities;
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
            var entity = _mapper.Map<Doacao>(request);
            var result = await _unitOfWork.Doacoes.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return result.Id;
        }
    }
}
