using Newtonsoft.Json;
using Pluto.Domain.Events.Common;
using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Models;
using Pluto.Repository.Contexts;
using Pluto.Repository.Repositories.Common;
using System.Threading.Tasks;

namespace Pluto.Repository.Repositories
{
    public class StoredEventRepository : CrudRepository<StoredEvent>, IStoredEventRepository
    {
        public StoredEventRepository(EventStoreSQLContext context) : base(context)
        {
            
        }

        public async Task AddEventAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            await AddAsync(new StoredEvent(@event, JsonConvert.SerializeObject(@event), null));
            await context.SaveChangesAsync();
        }
    }
}
