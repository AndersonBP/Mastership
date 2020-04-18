using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Infra.Data.Configuration;
using Mastership.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mastership.Database.Configuration
{
    public class CompanySettingsConfig : BaseConfig<CompanySettingsEntity>
    {
        public CompanySettingsConfig() : base("CompanySettings") { }

        public override void Configure(EntityTypeBuilder<CompanySettingsEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CompanyId).IsRequired();
            builder.Property(x => x.AllowMobile).HasDefaultValueSql("false").ValueGeneratedOnAdd();
            builder.Property(x => x.UseIpFilter).HasDefaultValueSql("true").ValueGeneratedOnAdd();
        }
    }
}
