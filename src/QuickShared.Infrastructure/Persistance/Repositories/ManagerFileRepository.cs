using Microsoft.EntityFrameworkCore;
using QuickShared.Domain.Entities;
using QuickShared.Domain.Repositories;
using QuickShared.Infrastructure.Persistance.Abstractions;

namespace QuickShared.Infrastructure.Persistance.Repositories;

internal sealed class ManagerFileRepository(AppDbContext context) : Repository<ManagerFile>(context), IManagerFileRepository
{
    public async Task<IReadOnlyList<FileInfoResult>> GetByNameAsync(string fileName, CancellationToken cancellationToken)
    {
        return await _context.Set<ManagerFile>()
            .Where(f => EF.Functions.ILike(f.FileName, fileName))
            .Select(f => new FileInfoResult(f.Id, f.FileName, f.ContentType, f.FileSize, f.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}
