using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using GM.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GM.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly GMDbContext _context;

    public ProductRepository(GMDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(
            product,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(
                p => p.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        _context.Products.Remove(product);

        await _context.SaveChangesAsync(cancellationToken);
    }
}