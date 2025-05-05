using DocAccessApproval.Domain.AggregateModels.UserAggregate;
using DocAccessApproval.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocAccessApproval.Persistence.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(DocAccessApprovalDbContext.Users));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.PasswordSalt)
            .IsRequired();


        var navigation = builder.Metadata.FindNavigation(nameof(User.UserRoles));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);


        builder.HasMany(x=>x.UserRoles)
            .WithOne()
            .HasForeignKey(x => x.UserId);

    }

}
