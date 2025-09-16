using Microsoft.EntityFrameworkCore;
using QuickShared.Domain.Entities;
using QuickShared.Domain.Repositories;
using QuickShared.Infrastructure.Persistance.Abstractions;

namespace QuickShared.Infrastructure.Persistance.Repositories;

internal sealed class ManagerFileRepository(AppDbContext context) : Repository<ManagerFile>(context), IManagerFileRepository
{
    public async Task<IReadOnlyList<FileInfoResult>> GetByNameAsync(string fileName, CancellationToken cancellationToken)
    {
        return await _context.ManagerFiles
            .AsNoTracking()
            .Where(f => EF.Functions.TrigramsSimilarity(
                    EF.Functions.Unaccent(f.FileName),
                    EF.Functions.Unaccent(fileName)
                ) > 0.25)
            .OrderByDescending(f => EF.Functions.TrigramsSimilarity(
                    EF.Functions.Unaccent(f.FileName),
                    EF.Functions.Unaccent(fileName)
                ))
            .ThenByDescending(f => f.CreatedAt)
            .Select(f => new FileInfoResult(f.Id, f.FileName, f.ContentType, f.FileSize, f.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}
