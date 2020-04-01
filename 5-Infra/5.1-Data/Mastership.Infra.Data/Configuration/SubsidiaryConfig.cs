using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Domain.Entities;
using Mastership.Infra.Data.Configuration;

namespace Mastership.Database.Configuration
{
    public class SubsidiaryConfig : BaseConfig<SubsidiaryEntity>
    {
        public SubsidiaryConfig() : base("Subsidiary") { }

        public override void Configure(EntityTypeBuilder<SubsidiaryEntity> builder)
        {
            base.Configure(builder);

            builder.HasMany(e => e.Employees)
           .WithOne(e => e.Subsidiary)
           .HasForeignKey(e => e.SubsidiaryId);
        }
    }
}
