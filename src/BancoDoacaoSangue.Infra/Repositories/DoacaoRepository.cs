using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories;
using BancoDoacaoSangue.Infra.Persistence;
using BancoDoacaoSangue.Infra.Repositories.Base;

namespace BancoDoacaoSangue.Infra.Repositories
{
    public class DoacaoRepository : RepositoryBase<Doacao>, IDoacaoRepository
    {
        public DoacaoRepository(DoacaoBancoSangueContext dbContext) : base(dbContext)
        {
        }
    }
}
