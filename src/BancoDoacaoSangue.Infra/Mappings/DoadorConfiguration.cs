using BancoDoacaoSangue.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoDoacaoSangue.Infra.Mappings
{
    public class DoadorConfiguration : IEntityTypeConfiguration<Doador>
    {
        public void Configure(EntityTypeBuilder<Doador> builder)
        {
            builder.ToTable("Doador");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.NomeCompleto)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(d => d.DataNascimento)
                .IsRequired();
            builder.Property(d => d.Genero)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(d => d.Peso)
                .IsRequired()
                .HasPrecision(5, 2);
            builder.Property(d => d.TipoSanguineo)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(d => d.FatorRh)
                .IsRequired()
                .HasMaxLength(10);
            builder.HasMany(d => d.Doacoes)
                .WithOne(d => d.Doador)
                .HasForeignKey(o => o.DoadorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(d => d.Endereco)
                .WithOne(e => e.Doador)
                .HasForeignKey<Endereco>(e => e.Id);
        }
    }
}
