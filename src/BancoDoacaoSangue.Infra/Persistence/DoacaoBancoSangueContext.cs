﻿using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BancoDoacaoSangue.Infra.Persistence
{
    public class DoacaoBancoSangueContext : DbContext
    {
        public DoacaoBancoSangueContext(DbContextOptions options) : base (options)
        {
            
        }

        public DbSet<Doador> Doadores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Doacao> Doacoes { get; set; }
        public DbSet<EstoqueSangue> EstoqueSangue { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CriadoEm = DateTime.Now;
                        entry.Entity.CriadoPor = "swn";
                        break;

                    case EntityState.Modified:
                        entry.Entity.AtualizadoEm = DateTime.Now;
                        entry.Entity.AtualizadoPor = "swn";
                        break;
                }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
