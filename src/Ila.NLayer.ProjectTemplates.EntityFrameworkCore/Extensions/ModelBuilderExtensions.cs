using System;
using System.Linq;
using System.Reflection;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityWithDeletableBase;
using Microsoft.EntityFrameworkCore;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddGlobalSoftDeletableFilter(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder
                .Model
                .GetEntityTypes()
                .Where(type => typeof(IEntityWithDeletableBase).IsAssignableFrom(type.ClrType));

            foreach (var type in entityTypes)
            {
                modelBuilder.SetIEntityWithDeletableFilter(type.ClrType);
            }
        }

        public static void SetIEntityWithDeletableFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetIEntityWithDeletableMethod
                .MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        private static readonly MethodInfo SetIEntityWithDeletableMethod = typeof(ModelBuilderExtensions)
                   .GetMethods(BindingFlags.Public | BindingFlags.Static)
                   .Single(t => t.IsGenericMethod && t.Name == "SetIEntityWithDeletableFilter");

        public static void SetIEntityWithDeletableFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, IEntityWithDeletableBase
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.Deleted);
        }
    }
}