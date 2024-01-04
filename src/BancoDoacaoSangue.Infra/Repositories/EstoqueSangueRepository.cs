using BancoDoacaoSangue.Core.DTOs;
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

        public async Task<List<RelatorioEstoqueSangueDto>> GetByQuantidadePorTipoSanguineo()
        {
            var gruposPorTipoSanguineo = await DbContext.EstoqueSangue.AsNoTracking()
            .GroupBy(item => new { item.TipoSanguineo, item.FatorRh})
            .Select(grupo => new RelatorioEstoqueSangueDto
            {
                TipoSanguineo = grupo.Key.TipoSanguineo!,
                FatorRh = grupo.Key.FatorRh!,
                TotalMl = grupo.Sum(r => r.QuantidadeMl) // Aqui você pode usar outras funções de agregação se necessário
            }).ToListAsync();
            return gruposPorTipoSanguineo;
        }
    }
}
