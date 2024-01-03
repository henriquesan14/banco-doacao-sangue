using BancoDoacaoSangue.Core.Repositories;
using BancoDoacaoSangue.Infra.Persistence;

namespace BancoDoacaoSangue.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DoacaoBancoSangueContext _dbContext;

        public UnitOfWork(DoacaoBancoSangueContext dbContext, IDoadorRepository doadores, IDoacaoRepository doacoes, IEstoqueSangueRepository estoque)
        {
            _dbContext = dbContext;
            Doadores = doadores;
            Doacoes = doacoes;
            EstoqueSangue = estoque;
        }

        public IDoadorRepository Doadores { get; }

        public IDoacaoRepository Doacoes { get; }

        public IEstoqueSangueRepository EstoqueSangue { get; }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            IsDisposing(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void IsDisposing(bool disposing) {
            if(disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
