using BancoDoacaoSangue.Application.ViewModels;
using MediatR;

namespace BancoDoacaoSangue.Application.Queries.RelatorioDoacoes
{
    public class RelatorioDoacoesQuery : IRequest<List<RelatorioDoacaoViewModel>>
    {
    }
}
