using BancoDoacaoSangue.Core.DTOs;
using Refit;

namespace BancoDoacaoSangue.Infra.Services
{
    public interface ICepService
    {
        [Get("/{cep}/json/")]
        Task<ResponseCepDto> BuscaCep(string cep);
    }
}