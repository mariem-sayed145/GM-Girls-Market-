using GM.Application.Abstractions.Persistence;
using GM.Domain.Entities;
using GM.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GM.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly GMDbContext _context;

    public ProjectRepository(GMDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Project project, CancellationToken cancellationToken = default)
    {
        await _context.Projects.AddAsync(project, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Members)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Members)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Project>> GetAssignedToUserAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Members)
            .Where(p => p.Members.Any(m => m.UserId == userId))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Project project, CancellationToken cancellationToken = default)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Project project, CancellationToken cancellationToken = default)
    {
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsMemberAsync(
        int projectId,
        int userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Where(p => p.Id == projectId)
            .SelectMany(p => p.Members)
            .AnyAsync(m => m.UserId == userId, cancellationToken);
    }
}
