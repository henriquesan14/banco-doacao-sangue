using BancoDoacaoSangue.Core.Entities.Base;

namespace BancoDoacaoSangue.Core.Entities
{
    public class Endereco : EntityBase
    {
        public Endereco(string? logradouro, string? cidade, string? estado, string? cep, Doador? doador, int doadorId)
        {
            Logradouro = logradouro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
            Doador = doador;
            DoadorId = doadorId;
        }

        public string? Logradouro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Cep { get; set; }
        public Doador? Doador { get; set; }
        public int DoadorId { get; set; }
    }
}
