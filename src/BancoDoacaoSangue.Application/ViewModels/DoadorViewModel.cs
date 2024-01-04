using BancoDoacaoSangue.Core.Entities;

namespace BancoDoacaoSangue.Application.ViewModels
{
    public class DoadorViewModel
    {
        public int Id { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Genero { get; set; }
        public decimal? Peso { get; set; }
        public string? TipoSanguineo { get; set; }
        public string? FatorRh { get; set; }
        public virtual Endereco? Endereco { get; set; }
    }
}
