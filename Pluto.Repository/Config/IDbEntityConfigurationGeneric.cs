using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pluto.Repository.Config
{
    public interface IDbEntityConfiguration<TEntity> : IDbEntityConfiguration where TEntity : class
    {
        void Configure(EntityTypeBuilder<TEntity> modelBuilder);
    }
}
