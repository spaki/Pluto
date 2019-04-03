using Pluto.Domain.Enums;
using Pluto.Domain.Models.Common;
using Pluto.Utils.Cryptography;

namespace Pluto.Domain.Models
{
    public class User : Entity
    {
        // -> Empty contructor for EF
        public User()
        {

        }

        public User(string name, string email, string password, UserProfile profile)
        {
            Name = name;
            Email = email;
            Profile = profile;
            Password = password.Encrypt();
        }

        public virtual string Name { get; private set; }
        public virtual string Email { get; private set; }
        public virtual string Password { get; private set; }
        public virtual UserProfile Profile { get; private set; }

        public void UpdateInfo(string name, string email, UserProfile profile)
        {
            Name = name;
            Email = email;
            Profile = profile;
        }

        public void ChangePassword(string password) => Password = password.Encrypt();

        public bool CheckPassword(string password) => password.Encrypt() == Password;
    }
}
