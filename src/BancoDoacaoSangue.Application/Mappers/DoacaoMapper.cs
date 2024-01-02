using AutoMapper;
using BancoDoacaoSangue.Application.Commands.CadastrarDoacao;
using BancoDoacaoSangue.Core.Entities;

namespace BancoDoacaoSangue.Application.Mappers
{
    public class DoacaoMapper : Profile
    {
        public DoacaoMapper()
        {
            CreateMap<CadastrarDoacaoCommand, Doacao>().ReverseMap();
        }
    }
}
