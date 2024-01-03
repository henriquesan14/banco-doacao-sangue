using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories;
using BancoDoacaoSangue.Infra.Persistence;
using BancoDoacaoSangue.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BancoDoacaoSangue.Infra.Repositories
{
    public class EstoqueSangueRepository : RepositoryBase<EstoqueSangue>, IEstoqueSangueRepository
    {
        public EstoqueSangueRepository(DoacaoBancoSangueContext dbContext) : base(dbContext)
        {
        }

        public async Task<EstoqueSangue> GetByTipoSanguineoFatorRh(string tipoSanguineo, string fatorRh)
        {
            return await DbContext.EstoqueSangue
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.TipoSanguineo == tipoSanguineo && u.FatorRh == fatorRh);
        }
    }
}
