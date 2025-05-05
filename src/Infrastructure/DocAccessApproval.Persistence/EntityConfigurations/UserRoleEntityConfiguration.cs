using DocAccessApproval.Domain.AggregateModels.UserAggregate;
using DocAccessApproval.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DocAccessApproval.Persistence.EntityConfigurations;

public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(nameof(DocAccessApprovalDbContext.UserRoles));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.RoleId)
        .IsRequired();

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }

}
