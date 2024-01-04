using BancoDoacaoSangue.Application.ViewModels;
using MediatR;

namespace BancoDoacaoSangue.Application.Queries.BuscarDoadores
{
    public class BuscarDoadoresQuery : IRequest<List<DoadorViewModel>>
    {
    }
}
