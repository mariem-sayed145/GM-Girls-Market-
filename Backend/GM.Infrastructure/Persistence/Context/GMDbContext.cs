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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(GMDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
} 