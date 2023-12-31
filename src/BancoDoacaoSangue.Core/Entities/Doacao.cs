using BancoDoacaoSangue.Core.Entities.Base;

namespace BancoDoacaoSangue.Core.Entities
{
    public class Doacao : EntityBase
    {
        public Doacao(Doador? doador, int doadorId, DateTime? dataDoacao, int? quantidadeMl)
        {
            Doador = doador;
            DoadorId = doadorId;
            DataDoacao = dataDoacao;
            QuantidadeMl = quantidadeMl;
        }

        public Doador? Doador { get; set; }
        public int DoadorId { get; set; }
        public DateTime? DataDoacao { get; set; }
        public int? QuantidadeMl { get; set; }
    }
}
