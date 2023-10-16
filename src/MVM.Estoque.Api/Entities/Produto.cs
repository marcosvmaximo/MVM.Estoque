using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MVM.Estoque.Api.Entities.Common;
using MVM.Estoque.Api.Enum;
using MVM.Estoque.Api.Exceptions;

namespace MVM.Estoque.Api.Entities;

public class Produto : Entity
{
    public Produto()
    {
    }

    public Produto(Guid fornecedorId, string nome, ECategoriaProduto categoriaProduto, decimal preco)
    {
        FornecedorId = fornecedorId;
        Nome = nome;
        Categoria = categoriaProduto;
        Preco = preco;

        Validar();
    }

    [JsonIgnore]
    public Fornecedor Fornecedor { get; set; }
    [Key]
    public Guid FornecedorId { get; set; }
    public string Nome { get; set; }
    public ECategoriaProduto Categoria { get; set; }
    public decimal Preco { get; set; }

    public override void Validar()
    {
        if (FornecedorId == Guid.Empty)
            throw new DomainException(nameof(FornecedorId), "Fornecedor Id inválido");

        if (Nome == null)
            throw new DomainException(nameof(Nome), "Nome inválido");

        if (Preco <= 0)
            throw new DomainException(nameof(Preco), "Preco inválido");
    }
}

