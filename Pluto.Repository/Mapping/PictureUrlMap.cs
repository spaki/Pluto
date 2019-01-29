using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pluto.Domain.Models;
using Pluto.Repository.Config;
using Pluto.Repository.Mapping.Common;

namespace Pluto.Repository.Mapping
{
    internal class PictureUrlMap : DbEntityConfiguration<PictureUrl>, IEntityMap
    {
        public override void Configure(EntityTypeBuilder<PictureUrl> entity)
        {
            entity.Property(c => c.Id).HasColumnName("Id");
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
