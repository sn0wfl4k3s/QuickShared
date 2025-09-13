using QuickShared.Domain.Abstractions;

namespace QuickShared.Domain.Entities;

public class ManagerFile : Entity
{
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long FileSize { get; set; }
}
