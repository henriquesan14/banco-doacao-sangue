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

        public async Task<List<Doacao>> GetRelatorioDoacoes()
        {
            return await DbContext.Doacoes
                .AsNoTracking()
                .Include(d => d.Doador)
                .ThenInclude(d => d!.Endereco)
                .Where(d => d.CriadoEm.Date <= DateTime.Now.Date && d.CriadoEm.Date >= DateTime.Now.AddDays(-30).Date)
                .ToListAsync();
        }
    }
}
