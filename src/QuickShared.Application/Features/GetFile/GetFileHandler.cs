using FluentResults;
using Mediator;
using Microsoft.Extensions.Logging;
using QuickShared.Domain.Repositories;
using QuickShared.Domain.Services;

namespace QuickShared.Application.Features.GetFile;

public sealed class GetFileHandler(
    IManagerFileRepository repository,
    ILogger<GetFileHandler> logger,
    IStorageService storage) : IRequestHandler<GetFileRequest, Result<GetFileResponse>>
{
    private readonly IManagerFileRepository _repository = repository;
    private readonly IStorageService _storage = storage;
    private readonly ILogger<GetFileHandler> _logger = logger;

    public async ValueTask<Result<GetFileResponse>> Handle(GetFileRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var managerFile = await _repository.GetByIdAsync(request.FileId);

            if (managerFile is null)
                return Result.Fail("File not found");

            byte[] fileBytes = await _storage.GetFileAsync(managerFile.FileUrl, cancellationToken);

            var response = new GetFileResponse(fileBytes, managerFile.ContentType, managerFile.FileName);

            return Result.Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving file with ID {FileId}", request.FileId);

            return Result.Fail("Error retrieving file");
        }
    }
}
