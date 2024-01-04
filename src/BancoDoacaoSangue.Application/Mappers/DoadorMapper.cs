using AutoMapper;
using BancoDoacaoSangue.Application.Commands.CadastrarDoador;
using BancoDoacaoSangue.Application.ViewModels;
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
                    Cep = src.Cep,
                })).ReverseMap();
            CreateMap<Doador, DoadorViewModel>();
        }
    }
}
