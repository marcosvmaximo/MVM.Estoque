using System;
using System.ComponentModel.DataAnnotations;

namespace MVM.Estoque.Api.Dtos;

public class EnderecoDto
{
    [Required(ErrorMessage = "O CEP é obrigatório.")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve conter exatamente 8 caracteres.")]
    public string Cep { get; set; }

    [Required(ErrorMessage = "O logradouro é obrigatório.")]
    public string Logradouro { get; set; }

    public string Complemento { get; set; }

    [Required(ErrorMessage = "O número é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O número deve ser maior que zero.")]
    public int Numero { get; set; }

    [Required(ErrorMessage = "O bairro é obrigatório.")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "A cidade é obrigatória.")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "A unidade federativa é obrigatória.")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "A unidade federativa deve conter exatamente 2 caracteres.")]
    public string UnidadeFederativa { get; set; }
}

public class EnderecoViewModel
{
    public string? Cep { get; set; }
    public string? Complemento { get; set; }
    public int Numero { get; set; }
}

