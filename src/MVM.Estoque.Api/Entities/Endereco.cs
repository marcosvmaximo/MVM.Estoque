using System;
using MVM.Estoque.Api.Entities.Common;
using MVM.Estoque.Api.Exceptions;

namespace MVM.Estoque.Api.Entities;

public class Endereco : Entity
{
    public Endereco()
    {
    }

    public Endereco(string cep,
                    string logradouro,
                    string complemento,
                    int numero,
                    string bairro,
                    string cidade,
                    string unidadeFederativa)
    {
        Cep = cep;
        Logradouro = logradouro;
        Complemento = complemento;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        UnidadeFederativa = unidadeFederativa;

        Validar();
    }

    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Complemento { get; set; }
    public int Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string UnidadeFederativa { get; set; }
    public Guid FornecedorId { get; set; }

    public override void Validar()
    {
        if (Cep == null || Cep.Length != 8)
            throw new DomainException(nameof(Cep), "Cep inválido.");

        if (Logradouro == null || Logradouro.Length > 100 || Logradouro.Length < 2)
            throw new DomainException(nameof(Logradouro), "Logradouro inválido.");

        if (Complemento == null || Complemento.Length > 100 || Complemento.Length < 2)
            throw new DomainException(nameof(Complemento), "Complemento inválido.");

        if (Numero <= 0)
            throw new DomainException(nameof(Numero), "Numero inválido.");

        if (Bairro == null || Bairro.Length > 100 || Bairro.Length < 2)
            throw new DomainException(nameof(Bairro), "Bairro inválido.");

        if (Cidade == null || Cidade.Length > 100 || Cidade.Length < 2)
            throw new DomainException(nameof(Cidade), "Cidade inválido.");

        if (UnidadeFederativa == null || UnidadeFederativa.Length != 2)
            throw new DomainException(nameof(UnidadeFederativa), "UnidadeFederativa inválido.");

        //if (FornecedorId == Guid.Empty)
        //    throw new DomainException(nameof(FornecedorId), "Fornecedor inválido.");

    }
}

