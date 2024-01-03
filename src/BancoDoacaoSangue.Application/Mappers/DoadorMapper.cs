using AutoMapper;
using BancoDoacaoSangue.Application.Commands.CadastrarDoador;
using BancoDoacaoSangue.Core.Entities;

namespace BancoDoacaoSangue.Application.Mappers
{
    public class DoadorMapper : Profile
    {
        public DoadorMapper()
        {
            CreateMap<CadastrarDoadorCommand, Doador>()
            .ForMember(dest => dest.Endereco, opt =>
                opt.MapFrom(src => new Endereco
                {
                    Logradouro = src.Logradouro,
                    Cidade = src.Cidade,
                    Estado = src.Estado,
                    Cep = src.Cep,
                })).ReverseMap();
        }
    }
}
