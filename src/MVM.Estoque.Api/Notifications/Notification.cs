using System;
namespace MVM.Estoque.Api.Notifications;

public class Notification : Message
{
    public Notification(string chave, string messagem) : base(chave, messagem)
    {
    }
}

