using Pluto.Domain.Events.Common;
using System;

namespace Pluto.Domain.Events.User
{
    public class ChangeUserPasswordEvent : Event
    {
        public ChangeUserPasswordEvent(Guid id, string password)
        {
            Id = id;
            Password = password;

            AggregateId = Id;
        }

        public Guid Id { get; set; }
        public string Password { get; private set; }
    }
}
