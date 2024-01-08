using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories.Base;

namespace BancoDoacaoSangue.Core.Repositories
{
    public interface IDoadorRepository : IAsyncRepository<Doador>
    {
        Task<Doador> GetByEmail(string email);
        Task<Doador> GetByIdIncludeEndereco(int id);
    }
}
