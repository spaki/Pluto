using Pluto.Domain.Commands.Common;
using System;

namespace Pluto.Domain.Commands.User
{
    public class DeleteUserCommand : Command
    {
        public DeleteUserCommand(Guid id) => this.Id = id;

        public Guid Id { get; set; }
    }
}
