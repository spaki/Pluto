using Newtonsoft.Json;
using Pluto.Domain.Events.Common;
using Pluto.Domain.Interfaces.Identity;
using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Models;
using Pluto.Repository.Contexts;
using Pluto.Repository.Repositories.Common;
using System.Threading.Tasks;

namespace Pluto.Repository.Repositories
{
    public class StoredEventRepository : CrudRepository<StoredEvent>, IStoredEventRepository
    {
        private readonly IUser user;

        public StoredEventRepository(EventStoreSQLContext context, IUser user) : base(context) => this.user = user;

        public async Task AddEventAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            await AddAsync(new StoredEvent(@event, JsonConvert.SerializeObject(@event), user?.Name));
            await context.SaveChangesAsync();
        }
    }
}
