using System;
using MVM.Estoque.Api.Interfaces;

namespace MVM.Estoque.Api.Notifications;

public class NotififyHandler : INotifyHandler
{
    private IList<Notification> _notifications;

    public NotififyHandler()
    {
        _notifications = new List<Notification>();
    }

    public async Task<IEnumerable<Notification>> ObterNotificacoes()
    {
        return _notifications.ToList();
    }

    public async Task<bool> PossuiNotificacao()
    {
        return _notifications.Any();
    }

    public async Task PublicarNotificacao(Notification notification)
    {
        _notifications.Add(notification);
    }

    public async Task PublicarNotificacao(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }
}

