using System;
using Microsoft.EntityFrameworkCore;
using MVM.Estoque.Api.Entities;

namespace MVM.Estoque.Api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> opt) : base(opt)
    {
    }

    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.ToTable("Fornecedor"); // Nome da tabela no SQL
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Id).HasColumnName("Id").HasColumnType("char(36)");
            entity.Property(f => f.Cpf).HasColumnName("Cpf").HasMaxLength(11);
            entity.Property(f => f.Nome).HasColumnName("Nome").HasMaxLength(100).IsRequired();
            entity.Property(f => f.DataNascimento).HasColumnName("Data_nascimento").HasColumnType("date").IsRequired();

            entity.OwnsOne(f => f.Endereco, endereco =>
            {
                endereco.Property(e => e.Logradouro).HasColumnName("Logradouro").HasMaxLength(100);
                endereco.Property(e => e.Complemento).HasColumnName("Complemento").HasMaxLength(100);
                endereco.Property(e => e.Cep).HasColumnName("Cep").HasMaxLength(9);
                endereco.Property(e => e.Bairro).HasColumnName("Bairro").HasMaxLength(100);
                endereco.Property(e => e.Cidade).HasColumnName("Cidade").HasMaxLength(100);
                endereco.Property(e => e.UnidadeFederativa).HasColumnName("Uf").HasMaxLength(2);
                endereco.Property(e => e.Numero).HasColumnName("Numero").IsRequired();
                endereco.Ignore(e => e.DataCriacao);
                endereco.Ignore(e => e.Id);
            });

            entity.Ignore(x => x.DataCriacao);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.ToTable("Produto"); // Nome da tabela no SQL
            entity.Property(p => p.Id).HasColumnName("Id").HasColumnType("char(36)");
            entity.Property(p => p.Categoria).HasColumnName("Categoria").IsRequired();
            entity.Property(p => p.Nome).HasColumnName("Nome").HasMaxLength(100).IsRequired();
            entity.Property(p => p.Preco).HasColumnName("Preco").HasColumnType("decimal(8,2)").IsRequired();
            entity.Property(p => p.FornecedorId).HasColumnName("fk_Fornecedor_Id").HasColumnType("CHAR(36)").IsRequired();
            //entity.Property(p => p.Forn).HasColumnName("fk_Fornecedor_Cpf").HasColumnType("varchar(11)").IsRequired();
            entity.Ignore(x => x.DataCriacao);

            entity.HasOne(p => p.Fornecedor)
                .WithMany(f => f.Produtos)
                .HasForeignKey(x => x.FornecedorId) // Chave estrangeira composta
                .OnDelete(DeleteBehavior.Cascade);

            // Estava definindo duas chaves estrangeiras com o mesmo nome
        });
    }
}

