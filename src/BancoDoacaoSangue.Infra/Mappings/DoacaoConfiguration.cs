using BancoDoacaoSangue.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoDoacaoSangue.Infra.Mappings
{
    public class DoacaoConfiguration : IEntityTypeConfiguration<Doacao>
    {
        public void Configure(EntityTypeBuilder<Doacao> builder)
        {
            builder.ToTable("Doacao");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.QuantidadeMl)
                .IsRequired()
                .HasMaxLength(3);
            builder.HasOne(d => d.Doador)
                .WithMany(d => d.Doacoes)
                .HasForeignKey(d => d.DoadorId);
        }
    }
}
