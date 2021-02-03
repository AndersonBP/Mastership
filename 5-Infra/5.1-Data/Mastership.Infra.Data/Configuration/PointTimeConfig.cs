using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Infra.Data.Entities;
using Mastership.Infra.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Mastership.Database.Configuration
{
    public class PointTimeConfig : BaseConfig<PointTimeEntity>
    {
        public PointTimeConfig() : base("PointTime") { }

        public override void Configure(EntityTypeBuilder<PointTimeEntity> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.DateTime)
                .HasColumnType("timestamp with time zone");

            builder.HasIndex(x => new { x.Sequential, x.SubsidiaryId }).IsUnique()
                .HasName("UN_Senquential")
                //Filtro de indici criado para corrigir bug de duplicidade NSR
                .HasFilter(@"((""SubsidiaryId"" <> 'a88c24f4-d6c9-4eba-8c86-67d515c3979f'::uuid) or ""Sequential""> 118802)");

            builder.Property(x => x.Sequential).ValueGeneratedOnAdd();
        }
    }
}
