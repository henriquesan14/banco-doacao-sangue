using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories;
using BancoDoacaoSangue.Infra.Persistence;
using BancoDoacaoSangue.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BancoDoacaoSangue.Infra.Repositories
{
    public class DoacaoRepository : RepositoryBase<Doacao>, IDoacaoRepository
    {
        public DoacaoRepository(DoacaoBancoSangueContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Doacao>> GetLastDoacaoByDoador(int doadorId)
        {
            return await DbContext.Set<Doacao>()
            .AsNoTracking()
            .Where(d => d.DoadorId == doadorId)
            .Take(1)
            .ToListAsync();
        }
    }
}
