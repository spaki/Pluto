using Pluto.Domain.Events.Common;
using Pluto.Domain.Interfaces.Repositories.Common;
using Pluto.Domain.Models;
using System.Threading.Tasks;

namespace Pluto.Domain.Interfaces.Repositories
{
    public interface IStoredEventRepository : ICrudRepository<StoredEvent>
    {
        Task AddEventAsync<TEvent>(TEvent @event) where TEvent : Event;
    }
}
