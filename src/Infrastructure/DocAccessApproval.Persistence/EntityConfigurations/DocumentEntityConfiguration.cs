using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocAccessApproval.Persistence.EntityConfigurations;

public class DocumentEntityConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable(nameof(DocAccessApprovalDbContext.Documents));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Content)
            .IsRequired();

        var navigation = builder.Metadata.FindNavigation(nameof(Document.AccessRequests));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.AccessRequests)
            .WithOne()
            .HasForeignKey(x => x.DocumentId);
    }
}
