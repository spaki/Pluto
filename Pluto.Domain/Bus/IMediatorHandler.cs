using Pluto.Domain.Commands.Common;
using Pluto.Domain.Events.Common;
using System.Threading.Tasks;

namespace Pluto.Domain.Bus
{
    public interface IMediatorHandler
    {
        Task SendAsync<T>(T command) where T : Command;
        Task InvokeAsync<T>(T @event) where T : Event;
        Task InvokeDomainNotificationAsync(string message);
    }
}
