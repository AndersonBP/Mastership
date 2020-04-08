using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Infra.Data.Entities;
using Mastership.Infra.Data.Configuration;

namespace Mastership.Database.Configuration
{
    public class CompanyConfig : BaseConfig<CompanyEntity>
    {
        public CompanyConfig() : base("Company") { }

        public override void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.DomainName).IsUnique();

            builder.HasIndex(x => x.CNPJ).IsUnique();
            builder.Property(x => x.CNPJ).HasMaxLength(18);

            builder.HasMany(e => e.Subsidiaries)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId);
        }
    }
}
