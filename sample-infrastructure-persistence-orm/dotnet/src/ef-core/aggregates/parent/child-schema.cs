using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentAggregate = Sample.Domain.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates.Parent
{
    public class ChildSchema : IEntityTypeConfiguration<ParentAggregate.Child>
    {
        public void Configure(EntityTypeBuilder<ParentAggregate.Child> builder)
        {
            builder.ToTable("Child", SampleDbContext.DEFAULT_SCHEMA)
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Description).IsRequired();
            builder.HasOne<ParentAggregate.Parent>()
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .IsRequired(true);
        }
    }
}