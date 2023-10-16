using System;
using System.ComponentModel.DataAnnotations;
using MVM.Estoque.Api.Entities;

namespace MVM.Estoque.Api.Dtos;

public class FornecedorDto
{
    [Required(ErrorMessage = "O Cpf é obrigatório.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A Data de Nascimento é obrigatória.")]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O Endereco é obrigatório.")]
    public EnderecoDto Endereco { get; set; }
}

public class FornecedorViewModel
{
    public string? Cpf { get; set; }
    public string? Nome { get; set; }
    public int Idade { get; set; }
    public EnderecoViewModel Endereco { get; set; }
    public IEnumerable<ProdutoViewModel> Produtos { get; set; }
}

