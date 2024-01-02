using BancoDoacaoSangue.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoDoacaoSangue.Infra.Mappings
{
    public class EstoqueSangueConfiguration : IEntityTypeConfiguration<EstoqueSangue>
    {
        public void Configure(EntityTypeBuilder<EstoqueSangue> builder)
        {
            builder.ToTable("EstoqueSangue");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.QuantidadeMl).IsRequired();
            builder.Property(e => e.FatorRh).IsRequired();
            builder.Property(e => e.TipoSanguineo).IsRequired();
        }
    }
}
