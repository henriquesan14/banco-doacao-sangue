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
            builder.Property(e => e.QuantidadeMl)
                .IsRequired()
                .HasMaxLength(3);
            builder.Property(e => e.FatorRh)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(e => e.TipoSanguineo)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
