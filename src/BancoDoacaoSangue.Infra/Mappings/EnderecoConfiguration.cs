using BancoDoacaoSangue.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoDoacaoSangue.Infra.Mappings
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");
            builder.HasKey(e => e.Id);
            builder.Property(d => d.Logradouro)
                .IsRequired();
            builder.Property(d => d.Cidade)
                .IsRequired();
            builder.Property(d => d.Estado)
                .IsRequired();
            builder.Property(d => d.Cep)
                .IsRequired();
            
            builder.HasOne(d => d.Doador)
            .WithOne(e => e.Endereco)
                .HasForeignKey<Doador>(e => e.EnderecoId)
                .IsRequired();
        }
    }
}
