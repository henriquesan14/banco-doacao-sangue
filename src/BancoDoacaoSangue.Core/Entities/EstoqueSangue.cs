using BancoDoacaoSangue.Core.Entities.Base;

namespace BancoDoacaoSangue.Core.Entities
{
    public class EstoqueSangue : EntityBase
    {
        public EstoqueSangue(string? tipoSanguineo, string? fatorRh, int? quantidadeMl)
        {
            TipoSanguineo = tipoSanguineo;
            FatorRh = fatorRh;
            QuantidadeMl = quantidadeMl;
        }

        public string? TipoSanguineo { get; set; }
        public string? FatorRh { get; set; }
        public int? QuantidadeMl { get; set; }
    }
}
