using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParentAggregate = Sample.Domain.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates.Parent
{
    public class ParentSchema : IEntityTypeConfiguration<ParentAggregate.Parent>
    {
        public void Configure(EntityTypeBuilder<ParentAggregate.Parent> builder)
        {
            builder.ToTable(nameof(ParentAggregate.Parent).ToLower(), SampleDbContext.DEFAULT_SCHEMA)
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName(nameof(ParentAggregate.Parent.Id).ToLower());
            builder.Property(x => x.Description).IsRequired()
                .HasColumnName(nameof(ParentAggregate.Parent.Description).ToLower());;
            builder.HasMany<ParentAggregate.Child>(x => x.Children)
                .WithOne()
                .HasForeignKey(x => x.ParentId)
                .IsRequired(true);
        }
    }
}