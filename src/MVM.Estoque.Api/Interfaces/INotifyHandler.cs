using System;
using MVM.Estoque.Api.Notifications;

namespace MVM.Estoque.Api.Interfaces;

public interface INotifyHandler
{
    Task PublicarNotificacao(Notification notification);
    Task<IEnumerable<Notification>> ObterNotificacoes();
    Task<bool> PossuiNotificacao();
}

