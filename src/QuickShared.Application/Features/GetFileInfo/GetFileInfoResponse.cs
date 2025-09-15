namespace QuickShared.Application.Features.GetFileInfo;

public record GetFileInfoResponse(Guid FileId, string FileName, string ContentType, long Size, DateTime CreatedAt);
