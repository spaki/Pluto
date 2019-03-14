using Pluto.Domain.Events.Common;
using System;

namespace Pluto.Domain.Events.User
{
    public class DeleteUserEvent : Event
    {
        public DeleteUserEvent(Guid id)
        {
            Id = id;

            AggregateId = Id;
        }

        public Guid Id { get; set; }
    }
}
