using Pluto.Domain.Commands.Common;
using Pluto.Domain.Enums;

namespace Pluto.Domain.Commands.User
{
    public class CreateUserCommand : Command
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public UserProfile Profile { get; set; }
    }
}
