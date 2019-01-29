using System;

namespace Pluto.Domain.Interfaces.Repositories.Common
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
