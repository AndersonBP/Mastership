using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Domain.Entities;
using Mastership.Infra.Data.Configuration;

namespace Mastership.Database.Configuration
{
    public class BillingCustomerConfig : BaseConfig<BillingCustomerEntity>
    {
        public BillingCustomerConfig() : base("BillingCustomer") { }

        public override void Configure(EntityTypeBuilder<BillingCustomerEntity> builder)
        {
            base.Configure(builder);

            builder.HasMany(e => e.Companies)
           .WithOne(e => e.BillingCustomer)
           .HasForeignKey(e => e.BillingCustomerId);

        }
    }
}
