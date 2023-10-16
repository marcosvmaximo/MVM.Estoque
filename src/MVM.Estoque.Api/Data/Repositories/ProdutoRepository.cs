using System;
using MVM.Estoque.Api.Entities;
using MVM.Estoque.Api.Interfaces.Repositories;

namespace MVM.Estoque.Api.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly DataContext _context;

    public ProdutoRepository(DataContext context)
    {
        _context = context;
    }

    public async Task Adicionar(Produto entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }

    public Task Atualizar(Produto entity)
    {
        throw new NotImplementedException();
    }

    public Task Deletar(Produto entity)
    {
        throw new NotImplementedException();
    }

    public Task<Produto?> ObterPorId(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Produto?>> ObterTodos()
    {
        throw new NotImplementedException();
    }
}

