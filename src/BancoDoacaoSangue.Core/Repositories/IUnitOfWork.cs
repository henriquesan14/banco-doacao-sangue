namespace BancoDoacaoSangue.Core.Repositories
{
    public interface IUnitOfWork
    {
        IDoadorRepository Doadores { get; }
        IDoacaoRepository Doacoes { get; }
        IEstoqueSangueRepository EstoqueSangue {  get; }
        Task<int> CompleteAsync();
    }
}
