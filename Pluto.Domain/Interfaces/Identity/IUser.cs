using System;
using System.Collections.Generic;
using System.Text;

namespace Pluto.Domain.Interfaces.Identity
{
    public interface IUser
    {
        string Id { get; }
        string Name { get; }
        string Email { get; }
    }
}
