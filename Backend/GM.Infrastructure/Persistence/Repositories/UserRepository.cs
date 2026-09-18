using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using GM.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GM.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GMDbContext _context;

    public UserRepository(GMDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(
            user,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(
                user => user.Email == email,
                cancellationToken);
    }
}