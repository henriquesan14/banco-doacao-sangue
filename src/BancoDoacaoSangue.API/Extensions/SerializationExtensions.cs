using System.Text.Json.Serialization;

namespace BancoDoacaoSangue.API.Extensions
{
    public static class SerializationExtensions
    {
        public static IServiceCollection JsonSerializationConfig(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            return services;
        }
    }
}
