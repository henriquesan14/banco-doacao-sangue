using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories;
using BancoDoacaoSangue.Infra.Persistence;
using BancoDoacaoSangue.Infra.Repositories.Base;

namespace BancoDoacaoSangue.Infra.Repositories
{
    public class EstoqueSangueRepository : RepositoryBase<EstoqueSangue>, IEstoqueSangueRepository
    {
        public EstoqueSangueRepository(DoacaoBancoSangueContext dbContext) : base(dbContext)
        {
        }
    }
}
