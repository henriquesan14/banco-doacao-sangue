using BancoDoacaoSangue.Core.Entities.Base;

namespace BancoDoacaoSangue.Core.Entities
{
    public class Doador : EntityBase
    {
        public Doador()
        {
            
        }
        public Doador(string? nomeCompleto, string? email, DateTime? dataNascimento, string? genero,
            decimal? peso, string? tipoSanguineo, string? fatorRh, IEnumerable<Doacao> doacoes, Endereco? endereco)
        {
            NomeCompleto = nomeCompleto;
            Email = email;
            DataNascimento = dataNascimento;
            Genero = genero;
            Peso = peso;
            TipoSanguineo = tipoSanguineo;
            FatorRh = fatorRh;
            Doacoes = doacoes;
            Endereco = endereco;
        }

        public string? NomeCompleto { get; set; }
        public string? Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Genero { get; set; }
        public decimal? Peso { get; set; }
        public string? TipoSanguineo { get; set; }
        public string? FatorRh { get; set; }
        public IEnumerable<Doacao> Doacoes { get; set; } = new List<Doacao>();
        public virtual Endereco? Endereco { get; set; }

        public bool MenorDeIdade {
            get
            {
                var dataHoje = DateTime.Now;
                int idade = (dataHoje.Year - DataNascimento!.Value.Year);
                if (dataHoje.Month < DataNascimento!.Value.Month || (dataHoje.Month == DataNascimento.Value.Month && dataHoje.Day < DataNascimento.Value.Day))
                {
                    idade--;
                }

                return idade < 18;
            }
        }
    }
}
