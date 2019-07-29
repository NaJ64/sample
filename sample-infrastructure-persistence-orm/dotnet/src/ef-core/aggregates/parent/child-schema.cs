using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentAggregate = Sample.Domain.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates.Parent
{
    public class ChildSchema : IEntityTypeConfiguration<ParentAggregate.Child>
    {
        public void Configure(EntityTypeBuilder<ParentAggregate.Child> builder)
        {
            builder.ToTable(nameof(ParentAggregate.Child).ToLowerInvariant(), SampleDbContext.DEFAULT_SCHEMA)
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName(nameof(ParentAggregate.Child.Id).ToLower());
            builder.Property(x => x.Description).IsRequired()
                .HasColumnName(nameof(ParentAggregate.Child.Description).ToLower());
            builder.Property(x => x.ParentId)
                .HasColumnName(nameof(ParentAggregate.Child.ParentId).ToLower());
            builder.HasOne<ParentAggregate.Parent>()
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .IsRequired(true);
        }
    }
}