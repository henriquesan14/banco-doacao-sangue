using BancoDoacaoSangue.Core.Repositories;
using BancoDoacaoSangue.Core.Repositories.Base;
using BancoDoacaoSangue.Infra.Repositories;
using BancoDoacaoSangue.Infra.Repositories.Base;

namespace BancoDoacaoSangue.API.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddTransient<IDoadorRepository, DoadorRepository>();
            services.AddTransient<IDoacaoRepository, DoacaoRepository>();
        

            return services;
        }
    }
}
