using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Domain.Entities;
using Mastership.Infra.Data.Configuration;

namespace Mastership.Database.Configuration
{
    public class CompanyConfig : BaseConfig<CompanyEntity>
    {
        public CompanyConfig() : base("Company") { }

        public override void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.CNPJ).IsUnique();
            builder.Property(x => x.CNPJ).HasMaxLength(18);
            builder.HasMany(e => e.Employees)
               .WithOne(e => e.Company)
               .HasForeignKey(e => e.CompanyId);

        }
    }
}
