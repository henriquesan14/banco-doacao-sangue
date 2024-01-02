using BancoDoacaoSangue.Core.Entities.Base;

namespace BancoDoacaoSangue.Core.Entities
{
    public class Doacao : EntityBase
    {
        public Doacao()
        {
            
        }
        public Doacao(Doador? doador, int doadorId, int? quantidadeMl)
        {
            Doador = doador;
            DoadorId = doadorId;
            QuantidadeMl = quantidadeMl;
        }

        public Doador? Doador { get; set; }
        public int DoadorId { get; set; }
        public int? QuantidadeMl { get; set; }
    }
}
