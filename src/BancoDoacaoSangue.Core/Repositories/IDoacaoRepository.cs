using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories.Base;

namespace BancoDoacaoSangue.Core.Repositories
{
    public interface IDoacaoRepository : IAsyncRepository<Doacao>
    {
        Task<List<Doacao>> GetLastDoacaoByDoador(int doadorId);
    }
}
