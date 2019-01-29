using Pluto.Domain.Events.Common;

namespace Pluto.Domain.Events.Interfaces
{
    public interface IHandler<in T> where T : Message
    {
        void Handle(T message);
    }
}
