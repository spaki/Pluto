using Pluto.Domain.Commands.Common;
using Pluto.Domain.Enums;
using System;

namespace Pluto.Domain.Commands.User
{
    public class UpdateUserCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserProfile Profile { get; set; }
    }
}
