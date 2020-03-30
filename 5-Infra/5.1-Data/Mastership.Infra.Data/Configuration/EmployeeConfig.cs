using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Domain.Entities;
using Mastership.Infra.Data.Configuration;

namespace Mastership.Database.Configuration
{
    public class EmployeeConfig : BaseConfig<EmployeeEntity>
    {
        public EmployeeConfig() : base("Employee") { }

        public override void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            base.Configure(builder);

        }
    }
}
