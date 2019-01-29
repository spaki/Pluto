using Microsoft.EntityFrameworkCore;
using Pluto.Domain.Interfaces.Repositories.Common;
using Pluto.Repository.Contexts;
using System;
using System.Linq;

namespace Pluto.Repository.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public UnitOfWork(MainDbContext context) => this.context = context;

        public void Commit()
        {
            if (context.ChangeTracker.Entries().Any(e => new[] { EntityState.Added, EntityState.Deleted, EntityState.Modified }.Contains(e.State)))
                context.SaveChanges();
        }

        public void Dispose() => context.Dispose();
    }
}
