using System;
using MVM.Estoque.Api.Enum;
using System.ComponentModel.DataAnnotations;

namespace MVM.Estoque.Api.Dtos;

public class ProdutoDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve conter no máximo 100 caracteres.", MinimumLength = 2)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A categoria do produto é obrigatória.")]
    public ECategoriaProduto CategoriaProduto { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }
}

public class ProdutoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public ECategoriaProduto CategoriaProduto { get; set; }
    public decimal Preco { get; set; }
    public FornecedorViewModel Fornecedor { get; set; }
}

