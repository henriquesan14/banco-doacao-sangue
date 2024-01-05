using BancoDoacaoSangue.Core.Entities;

namespace BancoDoacaoSangue.Application.ViewModels
{
    public class RelatorioDoacaoViewModel
    {
        public int Id { get; set; }
        public virtual Doador? Doador { get; set; }
        public int? QuantidadeMl { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
