using BancoDoacaoSangue.Core.DTOs;
using MediatR;

namespace BancoDoacaoSangue.Application.Queries.RelatorioEstoqueSangue
{
    public class RelatorioEstoqueSangueQuery : IRequest<List<RelatorioEstoqueSangueDto>>
    {
    }
}
