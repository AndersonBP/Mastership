using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Domain.Entities;
using Mastership.Infra.Data.Configuration;

namespace Mastership.Database.Configuration
{
    public class PointTimeConfig : BaseConfig<PointTimeEntity>
    {
        public PointTimeConfig() : base("PointTime") { }

        public override void Configure(EntityTypeBuilder<PointTimeEntity> builder)
        {
            base.Configure(builder);

        }
    }
}
