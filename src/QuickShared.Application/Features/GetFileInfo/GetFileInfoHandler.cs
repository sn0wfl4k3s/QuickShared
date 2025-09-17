using FluentResults;
using Mediator;
using Microsoft.Extensions.Logging;
using QuickShared.Domain.Repositories;

namespace QuickShared.Application.Features.GetFileInfo;

public sealed class GetFileInfoHandler(IManagerFileRepository repository, ILogger<GetFileInfoHandler> logger)
    : IRequestHandler<GetFileInfoRequest, Result<GetFileInfoResponse>>
{
    private readonly IManagerFileRepository _repository = repository;
    private readonly ILogger<GetFileInfoHandler> _logger = logger;

    public async ValueTask<Result<GetFileInfoResponse>> Handle(GetFileInfoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var managerFile = await _repository.GetByIdAsync(request.FileId);

            if (managerFile is null)
                return Result.Fail("File not found");

            var response = new GetFileInfoResponse(
                managerFile.Id,
                managerFile.FileName,
                managerFile.ContentType,
                managerFile.FileSize,
                managerFile.CreatedAt);

            return Result.Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving file info with ID {FileId}", request.FileId);

            return Result.Fail("Error retrieving file info");
        }
    }
}
