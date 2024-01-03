using AutoMapper;
using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories;
using MediatR;

namespace BancoDoacaoSangue.Application.Commands.CadastrarDoador
{
    public class CadastrarDoadorCommandHandler : IRequestHandler<CadastrarDoadorCommand, int>
    {
        private readonly IDoadorRepository _doadorRepository;
        private readonly IMapper _mapper;

        public CadastrarDoadorCommandHandler(IDoadorRepository doadorRepository, IMapper mapper)
        {
            _doadorRepository = doadorRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CadastrarDoadorCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Doador>(request);
            var result = await _doadorRepository.AddAsync(entity);
            return result.Id;
        }
    }
}
