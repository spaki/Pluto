using Pluto.Domain.Models.Common;

namespace Pluto.Domain.Models
{
    public class User : Entity
    {
        // -> Empty contructor for EF
        public User()
        {

        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public virtual string Name { get; private set; }
        public virtual string Email { get; private set; }
        public virtual string Password { get; private set; }

        public void UpdateInfo(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public void ChangePassword(string password) => Password = password;
    }
}
