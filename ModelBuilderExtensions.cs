using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.MappingExtension
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Extension method for ModelBuilder that adds mapping for a model.
        /// </summary>
        /// <typeparam name="TEntity">Class to register with the Entity method.</typeparam>
        /// <param name="modelBuilder">This instance of the ModelBuilder.</param>
        /// <param name="mapping">Instance of an object that inherits from ModelTypeConfiguration class.</param>
        /// <returns>ModelBuilder object.</returns>
        public static ModelBuilder AddMapping<TEntity>(this ModelBuilder modelBuilder, ModelTypeConfiguration<TEntity> mapping)
            where TEntity: class
        {
            return modelBuilder.Entity<TEntity>(mapping.Map);
        }
    }
}
