using GM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GM.Infrastructure.Persistence.Context;

public class GMDbContext : DbContext
{
    public GMDbContext(DbContextOptions<GMDbContext> options)
        : base(options)
    {
    }

    public DbSet<ContactInquiry> ContactInquiries => Set<ContactInquiry>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Service> Services => Set<Service>();

    public DbSet<User> Users => Set<User>();

    public DbSet<CustomerRequest> CustomerRequests => Set<CustomerRequest>();

    public DbSet<Document> Documents => Set<Document>();

    public DbSet<Client> Clients => Set<Client>();

    public DbSet<Project> Projects => Set<Project>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(GMDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
