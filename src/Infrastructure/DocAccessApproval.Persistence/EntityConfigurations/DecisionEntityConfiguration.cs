using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocAccessApproval.Persistence.EntityConfigurations;

public class DecisionEntityConfiguration : IEntityTypeConfiguration<Decision>
{
    public void Configure(EntityTypeBuilder<Decision> builder)
    {
        builder.ToTable(nameof(DocAccessApprovalDbContext.Decisions));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Comment)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.AccessRequestId)
            .IsRequired();

        builder.Property(x => x.DecisionDate)
            .IsRequired();
    }
}
