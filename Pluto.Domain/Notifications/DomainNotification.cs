using Pluto.Domain.Events.Common;

namespace Pluto.Domain.Notifications
{
    public class DomainNotification : Event
    {
        public string Message { get; private set; }

        public DomainNotification(string message)
        {
            Message = message;
        }
    }
}
