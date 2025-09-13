namespace QuickShared.Domain.Services;

public interface IStorageService
{
    ValueTask<UploadFileResponse> SaveFileAsync(UploadFileCommand request, CancellationToken cancellationToken = default);
    ValueTask<byte[]> GetFileAsync(string fileUrl, CancellationToken cancellationToken = default);
}

public record UploadFileCommand(Stream FileStream, string FileName, string ContentType);
public record UploadFileResponse(Guid FileId, string FileUrl);