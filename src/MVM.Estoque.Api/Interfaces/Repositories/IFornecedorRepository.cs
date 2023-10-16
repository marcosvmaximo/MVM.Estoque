using System;
using MVM.Estoque.Api.Entities;
using MVM.Estoque.Api.Entities.Common;

namespace MVM.Estoque.Api.Interfaces.Repositories;

public interface IFornecedorRepository : IRepository<Fornecedor>
{
    Task<Fornecedor?> ObterPorCpf(string cpf);

}

