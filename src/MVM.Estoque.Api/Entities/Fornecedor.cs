using System;
using MVM.Estoque.Api.Entities.Common;
using MVM.Estoque.Api.Exceptions;

namespace MVM.Estoque.Api.Entities;

public class Fornecedor : Entity
{
    private IList<Produto> _produtos;

    public Fornecedor()
    {
        _produtos = new List<Produto>();
    }

    public Fornecedor(string cpf, string nome, DateTime dataNascimento, Endereco endereco)
    {
        Cpf = cpf;
        Nome = nome;
        DataNascimento = dataNascimento;
        Endereco = endereco;
        Endereco.FornecedorId = Id;

        Validar();
    }

    public string Cpf { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public Endereco Endereco { get; set; }
    public IEnumerable<Produto> Produtos => _produtos;

    public void AdicionarProduto(Produto produto)
    {
        if (produto == null)
            throw new DomainException(nameof(Fornecedor), "Fornecedor inválido.");

        _produtos.Add(produto);
    }

    public void AdicionarProduto(IEnumerable<Produto> produto)
    {
        foreach (var p in produto)
        {
            AdicionarProduto(p);
        }
    }

    public override void Validar()
    {
        if (Endereco == null)
            throw new DomainException(nameof(Endereco), "Endereco inválido.");

        if (string.IsNullOrEmpty(Cpf))
            throw new DomainException(nameof(Cpf), "Cpf inválido.");

        if (string.IsNullOrEmpty(Nome))
            throw new DomainException(nameof(Nome), "Nome inválido.");

        if (DateTime.Now <= DataNascimento)
            throw new DomainException(nameof(DataNascimento), "DataNascimento inválido.");
    }
}

