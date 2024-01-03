using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories.Base;

namespace BancoDoacaoSangue.Core.Repositories
{
    public interface IEstoqueSangueRepository : IAsyncRepository<EstoqueSangue>
    {
        Task<EstoqueSangue> GetByTipoSanguineoFatorRh(string tipoSanguineo, string fatorRh);
    }
}
