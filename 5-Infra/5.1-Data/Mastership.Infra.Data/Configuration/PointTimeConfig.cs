using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Domain.Entities;
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

            builder.Property(x => x.Sequential).ValueGeneratedOnAdd();
            builder.Property(x => x.Day).HasColumnType("date");
            builder.Property(x => x.Hour).HasColumnType("time");

        }
    }
}
