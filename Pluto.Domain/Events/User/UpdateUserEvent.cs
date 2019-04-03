﻿using Pluto.Domain.Enums;
using Pluto.Domain.Events.Common;
using System;

namespace Pluto.Domain.Events.User
{
    public class UpdateUserEvent : Event
    {
        public UpdateUserEvent(Guid id, string name, string email, UserProfile profile)
        {
            Id = id;
            Name = name;
            Email = email;
            Profile = profile;

            AggregateId = Id;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserProfile Profile { get; set; }
    }
}
