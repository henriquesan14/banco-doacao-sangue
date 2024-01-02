using MediatR;

namespace BancoDoacaoSangue.Application.Commands.CadastrarDoacao
{
    public class CadastrarDoacaoCommand : IRequest<int>
    {
        public CadastrarDoacaoCommand(int doadorId, int? quantidadeMl)
        {
            DoadorId = doadorId;
            QuantidadeMl = quantidadeMl;
        }

        public int DoadorId { get; set; }
        public int? QuantidadeMl { get; set; }
    }
}
