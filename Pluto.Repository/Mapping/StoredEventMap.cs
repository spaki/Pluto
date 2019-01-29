using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pluto.Domain.Models;
using Pluto.Repository.Config;
using Pluto.Repository.Mapping.Common;

namespace Pluto.Repository.Mapping
{
    internal class StoredEventMap : DbEntityConfiguration<StoredEvent>, IEventMap
    {
        public override void Configure(EntityTypeBuilder<StoredEvent> entity)
        {
            entity.Property(c => c.Id).HasColumnName("Id");
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
