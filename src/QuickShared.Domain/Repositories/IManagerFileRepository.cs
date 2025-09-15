using QuickShared.Domain.Abstractions;
using QuickShared.Domain.Entities;

namespace QuickShared.Domain.Repositories;

public interface IManagerFileRepository : IRepository<ManagerFile>
{
    Task<IReadOnlyList<FileInfoResult>> GetByNameAsync(string fileName, CancellationToken cancellationToken);
}

public record FileInfoResult(Guid FileId, string FileName, string ContentType, long Size, DateTime CreatedAt);