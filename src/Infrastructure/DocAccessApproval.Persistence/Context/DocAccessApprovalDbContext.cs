using DocAccessApproval.Application.Interfaces;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.AggregateModels.UserAggregate;
using DocAccessApproval.Domain.SeedWork;
using DocAccessApproval.Persistence.EntityConfigurations;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DocAccessApproval.Persistence.Context;

public class DocAccessApprovalDbContext:DbContext,IUnitOfWork
{
    private readonly IDomainEventDispatcher _dispatcher;
    public DbSet<Document> Documents { get; set; }
    public DbSet<AccessRequest> AccessRequests { get; set; }
    public DbSet<Decision> Decisions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public DocAccessApprovalDbContext(DbContextOptions<DocAccessApprovalDbContext> options, IDomainEventDispatcher dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DocumentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AccessRequestEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DecisionEntityConfiguration());

        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        int result = await base.SaveChangesAsync(cancellationToken);

        await _dispatcher.DispatchAsync(domainEvents, cancellationToken);


        foreach (var entity in ChangeTracker.Entries<AggregateRoot>())
        {
            entity.Entity.ClearDomainEvents();
        }

        return result;
    }
}
