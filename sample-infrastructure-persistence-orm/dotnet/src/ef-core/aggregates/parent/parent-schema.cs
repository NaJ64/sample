using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentAggregate = Sample.Domain.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates.Parent
{
    public class ParentSchema : IEntityTypeConfiguration<ParentAggregate.Parent>
    {
        public void Configure(EntityTypeBuilder<ParentAggregate.Parent> builder)
        {
            builder.ToTable("Parent", SampleDbContext.DEFAULT_SCHEMA)
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Description).IsRequired();
            builder.HasMany<ParentAggregate.Child>(x => x.Children)
                .WithOne()
                .HasForeignKey(x => x.ParentId)
                .IsRequired(true);
        }
    }
}