using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Infra.Data.Entities;
using Mastership.Infra.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Mastership.Database.Configuration
{
    public class PointTimeConfig : BaseConfig<PointTimeEntity>
    {
        public PointTimeConfig() : base("PointTime") { }

        public override void Configure(EntityTypeBuilder<PointTimeEntity> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.DateTime)
                .HasColumnType("timestamp with time zone");

            builder.Property(x => x.Sequential).ValueGeneratedOnAdd();
        }
    }
}
