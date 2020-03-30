using Mastership.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mastership.Infra.Data.Context
{
    public class DataContext: DbContext, IDataContext
    {
        protected DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureAllEntityTypes(modelBuilder);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.SeedModel();
        }

        public void Clear()
        {
            var changedEntries = ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached)
                .ToList();

            foreach (var entry in changedEntries)
                entry.State = EntityState.Detached;
        }

        protected void ConfigureAllEntityTypes(ModelBuilder modelBuilder)
        {
            var applyConfigMethod = typeof(ModelBuilder).GetMethods().Where(e => e.Name == "ApplyConfiguration" && e.GetParameters().Single().ParameterType.Name == typeof(IEntityTypeConfiguration<>).Name).Single();
            var configs = GetAllConfigs();

            foreach (var item in configs)
            {
                var entityType = item.ImplementedInterfaces.First(x => x.Name == typeof(IEntityTypeConfiguration<>).Name).GenericTypeArguments.Single();
                var applyConfigGenericMethod = applyConfigMethod.MakeGenericMethod(entityType);
                applyConfigGenericMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(item) });
            }
        }

        protected IList<TypeInfo> GetAllConfigs()
            => Assembly.GetExecutingAssembly().DefinedTypes
                .Where(t => t.ImplementedInterfaces.Any(i => i.Name == typeof(IDataContextConfiguration).Name))
                .Where(i => i.IsClass && !i.IsAbstract && !i.IsNested)
                .ToList();
    }
}
