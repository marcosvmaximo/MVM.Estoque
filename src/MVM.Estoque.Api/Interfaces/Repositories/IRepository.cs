using System;
using MVM.Estoque.Api.Entities;

namespace MVM.Estoque.Api.Interfaces.Repositories;

public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity?>> ObterTodos();
    Task<TEntity?> ObterPorId(Guid id);
    Task Adicionar(TEntity entity);
    Task Atualizar(TEntity entity);
    Task Deletar(TEntity entity);

}

