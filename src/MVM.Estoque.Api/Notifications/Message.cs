using System;
namespace MVM.Estoque.Api.Notifications;

public abstract class Message
{
    public Guid Id { get; set; }
    public string Chave { get; set; }
    public string Messagem { get; set; }

    protected Message(string chave, string messagem)
    {
        Id = Guid.NewGuid();
        Chave = chave;
        Messagem = messagem;
    }
}

