using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Infra.Data.Entities;
using Mastership.Infra.Data.Configuration;

namespace Mastership.Database.Configuration
{
    public class EmployeeConfig : BaseConfig<EmployeeEntity>
    {
        public EmployeeConfig() : base("Employee") { }

        public override void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => x.CPF).IsUnique();
            builder.Property(x => x.CPF).HasMaxLength(15);

            builder.HasIndex(x => new { x.SubsidiaryId, x.Registration }).IsUnique();

            builder.HasMany(e => e.PointsTime)
               .WithOne(e => e.Employee)
               .HasForeignKey(e => e.EmployeeId);
        }
    }
}
