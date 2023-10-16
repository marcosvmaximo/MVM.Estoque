using System;
namespace MVM.Estoque.Api.Entities.Common;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }

    public Entity()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.Now;
    }

    public abstract void Validar();
}

