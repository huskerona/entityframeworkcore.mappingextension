# EF Core Mapping Extensions
Extension method for mapping entities like it was possible with EF6.x and EntityTypeConfiguration&lt;TEntity&gt; class.

## How to use it
Once you download it from GitHub or get it from [Nuget](https://www.nuget.org/packages/EntityFrameworkCore.MappingExtension/2.0.0) you will need 
to create a new class that inherits from `ModelTypeConfiguration<TEntity>` class and implement `Map()` method. With the mapping in place, all that 
is left is to call `AddMapping<TEntity>()` method on an instance of `ModelBuilder` object.

## Example
Creating a class that inherits from `ModelTypeConfiguration` that would target, for example, SQL Server:

    using EntityFrameworkCore.MappingExtension;
    using Framework.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace DataAccess.EFCore.SqlServer.Mappings
    {
        public class LanguageModelTypeConfiguration: ModelTypeConfiguration<Language>
        {
            #region Overrides of ModelTypeConfiguration<Language>

            public override void Map(EntityTypeBuilder<Language> builder)
            {
                // Primary keys
                builder.HasKey(p => p.Id);

                // Columns definitions
                builder.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                builder.Property(p => p.Name)
                    .IsRequired()
                    .ForSqlServerHasColumnType("nvarchar(150)");

                builder.Property(p => p.Code)
                    .IsRequired()
                    .ForSqlServerHasColumnType("nvarchar(2)");

                builder.Property(p => p.IsActive)
                    .IsRequired()
                    .ForSqlServerHasDefaultValue(false);

                // Table and columns mappings
                builder.ForSqlServerToTable("Languages", "local");
                builder.Property(p => p.Id).ForSqlServerHasColumnName("LanguageID");
                builder.Property(p => p.Name).ForSqlServerHasColumnName("Language");
                builder.Property(p => p.Code).ForSqlServerHasColumnName("LanguageCode");
                builder.Property(p => p.IsActive).ForSqlServerHasColumnName("Active");
            }

            #endregion
        }
    } 

> NOTE: In case `ForSqlServer` methods do not work. Fall back to the common methods by dropping `ForSqlServer` from the name.

The last thing is to call `AddMapping` in the override of `OnModelCreating` within the `DbContext`:

    using DataAccess.EFCore.SqlServer.Mappings;
    using EntityFrameworkCore.MappingExtension;
    using Framework.Models;
    using Microsoft.EntityFrameworkCore;

    namespace DataAccess.EFCore.SqlServer.Contexts
    {
        public class SampleContext: Microsoft.EntityFrameworkCore.DbContext
        {
            public SampleContext(DbContextOptions<SampleContext> options): base(options)
            {

            }

            public DbSet<Language> Languages { get; set; }

            #region Overrides of DbContext

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Call to AddMapping
                modelBuilder.AddMapping(new LanguageModelTypeConfiguration());
            }

            #endregion
        }
    }
    
