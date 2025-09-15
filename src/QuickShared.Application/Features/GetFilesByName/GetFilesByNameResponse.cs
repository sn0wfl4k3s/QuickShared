using QuickShared.Application.Features.GetFileInfo;

namespace QuickShared.Application.Features.GetFilesByName;

public record GetFilesByNameResponse(IReadOnlyList<FileInfoResponse> Files);

public record FileInfoResponse(Guid FileId, string FileName, string ContentType, long Size, DateTime CreatedAt);