namespace QuickShared.Application.Features.GetFile;

public record GetFileResponse(byte[] FileBytes, string ContentType, string FileName);
