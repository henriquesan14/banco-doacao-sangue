using BancoDoacaoSangue.Application.ViewModels;
using MediatR;

namespace BancoDoacaoSangue.Application.Queries.BuscarDoador
{
    public class BuscarDoacoesPorDoadorQuery : IRequest<List<DoacaoViewModel>>
    {
        public BuscarDoacoesPorDoadorQuery(int doadorId)
        {
            DoadorId = doadorId;
        }

        public int DoadorId { get; set; }
    }
}
