using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.MappingExtension
{
    /// <summary>
    /// Base class from which other class will inherit. Similar to EntityTypeConfiguration in EF6.x
    /// </summary>
    /// <typeparam name="TEntity">Class that will be configured.</typeparam>
    public abstract class ModelTypeConfiguration<TEntity> where TEntity: class
    {
        /// <summary>
        /// Method that is invoked by the <see cref="ModelBuilderExtensions.AddMapping{TEntity}"/> to configure the entity.
        /// </summary>
        /// <param name="builder">An instance of the API for configuring the properties.</param>
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}