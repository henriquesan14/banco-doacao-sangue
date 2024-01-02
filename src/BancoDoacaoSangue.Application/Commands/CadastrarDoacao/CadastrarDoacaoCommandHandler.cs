using AutoMapper;
using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories;
using MediatR;

namespace BancoDoacaoSangue.Application.Commands.CadastrarDoacao
{
    public class CadastrarDoacaoCommandHandler : IRequestHandler<CadastrarDoacaoCommand, int>
    {

        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IMapper _mapper;

        public CadastrarDoacaoCommandHandler(IDoacaoRepository doacaoRepository, IMapper mapper)
        {
            _doacaoRepository = doacaoRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CadastrarDoacaoCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Doacao>(request);
            var result = await _doacaoRepository.AddAsync(entity);
            return result.Id;
        }
    }
}
