using QuickShared.Domain.Entities;
using QuickShared.Domain.Repositories;
using QuickShared.Infrastructure.Persistance.Abstractions;

namespace QuickShared.Infrastructure.Persistance.Repositories;

internal sealed class ManagerFileRepository(AppDbContext context) : Repository<ManagerFile>(context), IManagerFileRepository
{
}
