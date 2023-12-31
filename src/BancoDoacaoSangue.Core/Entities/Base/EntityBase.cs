namespace BancoDoacaoSangue.Core.Entities.Base
{
    public class EntityBase
    {
        public int Id { get; protected set; }
        public string? CriadoPor { get; set; }
        public DateTime CriadoEm { get; set; }
        public string? AtualizadoPor { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
