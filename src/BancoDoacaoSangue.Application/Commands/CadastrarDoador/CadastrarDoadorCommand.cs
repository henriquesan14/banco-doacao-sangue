using MediatR;

namespace BancoDoacaoSangue.Application.Commands.CadastrarDoador
{
    public class CadastrarDoadorCommand : IRequest<int>
    {
        public CadastrarDoadorCommand(string? nomeCompleto, string? email, DateTime? dataNascimento, string? genero,
            decimal? peso, string? tipoSanguineo, string? fatorRh, string? logradouro, string? cidade, string? estado, string? cep)
        {
            NomeCompleto = nomeCompleto;
            Email = email;
            DataNascimento = dataNascimento;
            Genero = genero;
            Peso = peso;
            TipoSanguineo = tipoSanguineo;
            FatorRh = fatorRh;
            Logradouro = logradouro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
        }

        public string? NomeCompleto { get; set; }
        public string? Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Genero { get; set; }
        public decimal? Peso { get; set; }
        public string? TipoSanguineo { get; set; }
        public string? FatorRh { get; set; }
        public string? Logradouro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Cep { get; set; }
    }
}
