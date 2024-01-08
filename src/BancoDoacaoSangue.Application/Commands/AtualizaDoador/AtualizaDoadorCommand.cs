using MediatR;

namespace BancoDoacaoSangue.Application.Commands.AtualizaDoador
{
    public class AtualizaDoadorCommand : IRequest
    {
        public AtualizaDoadorCommand(int id, string? nomeCompleto, string? email, DateTime? dataNascimento,
            string? genero, decimal? peso, string? tipoSanguineo, string? fatorRh, string? cep)
        {
            Id = id;
            NomeCompleto = nomeCompleto;
            Email = email;
            DataNascimento = dataNascimento;
            Genero = genero;
            Peso = peso;
            TipoSanguineo = tipoSanguineo;
            FatorRh = fatorRh;
            Cep = cep;
        }

        public int Id { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Genero { get; set; }
        public decimal? Peso { get; set; }
        public string? TipoSanguineo { get; set; }
        public string? FatorRh { get; set; }

        public string? Cep { get; set; }
    }
}
