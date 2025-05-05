using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocAccessApproval.Persistence.EntityConfigurations;

public class AccessRequestEntityConfiguration : IEntityTypeConfiguration<AccessRequest>
{
    public void Configure(EntityTypeBuilder<AccessRequest> builder)
    {
        builder.ToTable(nameof(DocAccessApprovalDbContext.AccessRequests));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.DocumentId)
            .IsRequired();

        builder.Property(x => x.RequestDate)
            .IsRequired();

        builder.Property(x => x.ExpireDate)
            .IsRequired();

        builder.Property(x => x.RequestStatus)
            .IsRequired();

        builder.Property(x => x.AccessType)
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasOne(x => x.Decision)
            .WithOne();
    }
}
