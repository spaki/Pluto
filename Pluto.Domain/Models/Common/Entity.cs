using System;

namespace Pluto.Domain.Models.Common
{
    public abstract class Entity
    {
        public virtual Guid Id { get; set; }
    }
}
