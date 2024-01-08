using BancoDoacaoSangue.Application.ViewModels;
using MediatR;

namespace BancoDoacaoSangue.Application.Queries.BuscarDoadorPorId
{
    public class BuscarDoadorPorIdQuery : IRequest<DoadorViewModel>
    {
        public BuscarDoadorPorIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
