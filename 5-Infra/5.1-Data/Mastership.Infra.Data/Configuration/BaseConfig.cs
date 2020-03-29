using Mastership.Domain.Entities;
using Mastership.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mastership.Infra.Data.Configuration
{
    public abstract class BaseConfig<TType> : IDataContextConfiguration, IEntityTypeConfiguration<TType> where TType : BaseEntity
    {

        public BaseConfig(string tableName)
            => TableName = tableName;

        public string TableName { get; }

        public virtual void Configure(EntityTypeBuilder<TType> builder)
        {
            builder.ToTable(TableName);
            
            builder.HasKey(obj => obj.Id);
            builder.Property(obj => obj.Enable).HasDefaultValue(true);
            builder.Property(obj => obj.Deleted).HasDefaultValue(false);
            builder.Property(x => x.CreationDate).IsRequired();
        }
    }
}
