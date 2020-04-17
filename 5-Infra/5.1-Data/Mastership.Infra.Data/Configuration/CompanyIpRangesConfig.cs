using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Infra.Data.Configuration;
using Mastership.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mastership.Database.Configuration
{
    public class CompanyIpRangesConfig : BaseConfig<CompanyIpRangesEntity>
    {
        public CompanyIpRangesConfig() : base("CompanyIpRanges") { }

        public override void Configure(EntityTypeBuilder<CompanyIpRangesEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Begin).IsRequired();
            builder.Property(x => x.End).IsRequired();
            builder.Property(x => x.Begin).HasColumnType("inet");
            builder.Property(x => x.End).HasColumnType("inet");

        }
    }
}
