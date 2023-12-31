using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories;
using BancoDoacaoSangue.Infra.Persistence;
using BancoDoacaoSangue.Infra.Repositories.Base;

namespace BancoDoacaoSangue.Infra.Repositories
{
    public class DoadorRepository : RepositoryBase<Doador>, IDoadorRepository
    {
        public DoadorRepository(DoacaoBancoSangueContext dbContext) : base(dbContext)
        {
        }
    }
}
