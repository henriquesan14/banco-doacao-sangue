namespace BancoDoacaoSangue.Core.Repositories
{
    public interface IUnitOfWork
    {
        IDoadorRepository Doadores { get; }
        IDoacaoRepository Doacoes { get; }
        IEstoqueSangueRepository Estoque {  get; }
        Task<int> CompleteAsync();
    }
}
