using BancoDoacaoSangue.Core.Entities;

namespace BancoDoacaoSangue.Application.ViewModels
{
    public class DoadorViewModel
    {
        public DoadorViewModel(string? nomeCompleto, string? email, DateTime? dataNascimento, string? genero, decimal? peso, string? tipoSanguineo, string? fatorRh, Endereco? endereco)
        {
            NomeCompleto = nomeCompleto;
            Email = email;
            DataNascimento = dataNascimento;
            Genero = genero;
            Peso = peso;
            TipoSanguineo = tipoSanguineo;
            FatorRh = fatorRh;
            Endereco = endereco;
        }

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
