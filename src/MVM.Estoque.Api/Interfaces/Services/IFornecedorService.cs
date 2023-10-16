using System;
using MVM.Estoque.Api.Dtos;

namespace MVM.Estoque.Api.Interfaces.Services;

public interface IFornecedorService
{
    Task<FornecedorViewModel?> CadastrarFornecedor(FornecedorDto model);
    Task<FornecedorViewModel?> AtualizarFornecedor(Guid id, FornecedorDto model);
    Task DeletarFornecedor(Guid id);
    Task<FornecedorViewModel> AdicionarProduto(Guid idFornecedor, ProdutoDto model);
}

