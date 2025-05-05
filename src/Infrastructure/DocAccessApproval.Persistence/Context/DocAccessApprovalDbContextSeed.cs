using DocAccessApproval.Application.Security.Hashing;
using DocAccessApproval.Domain.AggregateModels.UserAggregate;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;

namespace DocAccessApproval.Persistence.Context;

public class DocAccessApprovalDbContextSeed
{
    private readonly List<Role> roles = new()
    {
        new Role("admin"),
        new Role("approver"),
        new Role("user")
    };

    public async Task SeedAsync(DocAccessApprovalDbContext dbContext)
    {
        var policy = CreatePolicy();

        await policy.ExecuteAsync(async () =>
        {
            if (!dbContext.Roles.Any())
            {
                dbContext.Roles.AddRange(roles);
            }

            if (!dbContext.Users.Any())
            {
                User user = new User(Guid.NewGuid(), "Admin", "Admin", "a.aghayeva.dev@gmail.com", "admin");
                HashingHelper.CreatePasswordHash("admin", out byte[] passwordHash, out byte[] passwordSalt);
                user.SetPasswords(passwordSalt, passwordHash);

                var adminRole = roles.FirstOrDefault(x => x.Name == "admin");
                user.AddUserRole(adminRole);

                dbContext.Users.Add(user);

            }

            await dbContext.SaveChangesAsync();
        });

    }

    private AsyncRetryPolicy CreatePolicy()
    {
        return Policy.Handle<SqliteException>()
            .RetryAsync(retryCount: 3);
    }
}
