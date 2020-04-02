using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Infra.Data.Entities;
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

            builder.HasOne(a => a.User)
            .WithOne(b => b.BillingCustomer)
              .HasForeignKey<UserEntity>(e => e.BillingCustomerId);

        }
    }
}
