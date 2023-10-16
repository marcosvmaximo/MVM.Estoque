using System;
using Microsoft.EntityFrameworkCore;
using MVM.Estoque.Api.Entities;
using MVM.Estoque.Api.Interfaces.Repositories;

namespace MVM.Estoque.Api.Data.Repositories;

public class FornecedorRepository : IFornecedorRepository
{
    private readonly DataContext _context;

    public FornecedorRepository(DataContext context)
    {
        _context = context;
    }

    public async Task Adicionar(Fornecedor fornecedor)
    {
        _context.Add(fornecedor);
        _context.SaveChanges();
    }

    public async Task Atualizar(Fornecedor fornecedor)
    {
        _context.Update(fornecedor);
        _context.SaveChanges();
    }

    public async Task Deletar(Fornecedor fornecedor)
    {
        _context.Remove(fornecedor);
        _context.SaveChanges();
    }

    public async Task<Fornecedor?> ObterPorCpf(string cpf)
    {
        return _context.Fornecedores.AsNoTracking().FirstOrDefault(x => x.Cpf == cpf);
    }

    public async Task<Fornecedor?> ObterPorId(Guid id)
    {
        var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(x => x.Id == id);

        if (fornecedor != null)
        {
            _context.Entry(fornecedor)
                .Collection(f => f.Produtos)
                .Load();
        }

        return fornecedor;
    }

    public async Task<IEnumerable<Fornecedor?>> ObterTodos()
    {
        return _context.Fornecedores.AsNoTracking().ToList();
    }
}

