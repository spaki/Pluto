using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pluto.Domain.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> notifications = new List<DomainNotification>();

        public async Task Handle(DomainNotification message, CancellationToken cancellationToken) => notifications.Add(message);

        public virtual List<DomainNotification> GetNotifications() => notifications;

        public virtual bool HasNotifications() => GetNotifications().Any();

        public void Dispose() => notifications.Clear();
    }
}
