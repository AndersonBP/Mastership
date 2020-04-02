using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Infra.Data.Entities;
using Mastership.Infra.Data.Configuration;

namespace Mastership.Database.Configuration
{
    public class UserConfig : BaseConfig<UserEntity>
    {
        public UserConfig() : base("User") { }

        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            base.Configure(builder);

        }
    }
}
