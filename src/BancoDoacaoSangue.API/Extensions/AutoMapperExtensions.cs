using AutoMapper;
using BancoDoacaoSangue.Application.Mappers;

namespace BancoDoacaoSangue.API.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AutoMapperConfig(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DoacaoMapper>();
                cfg.AddProfile<DoadorMapper>();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
