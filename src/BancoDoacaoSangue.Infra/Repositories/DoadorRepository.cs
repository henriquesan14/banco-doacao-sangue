using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories;
using BancoDoacaoSangue.Infra.Persistence;
using BancoDoacaoSangue.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BancoDoacaoSangue.Infra.Repositories
{
    public class DoadorRepository : RepositoryBase<Doador>, IDoadorRepository
    {
        public DoadorRepository(DoacaoBancoSangueContext dbContext) : base(dbContext)
        {
        }

        public async Task<Doador> GetByEmail(string email)
        {
            return await DbContext.Doadores
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Doador> GetByIdIncludeEndereco(int id)
        {
            return await DbContext.Doadores
                .AsNoTracking()
                .Include(d => d.Endereco)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
