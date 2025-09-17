using FluentResults;
using Mediator;
using Microsoft.Extensions.Logging;
using QuickShared.Domain.Repositories;

namespace QuickShared.Application.Features.GetFilesByName;

public class GetFilesByNameHandler(IManagerFileRepository managerFileRepository, ILogger<GetFilesByNameHandler> logger) : IRequestHandler<GetFilesByNameRequest, Result<GetFilesByNameResponse>>
{
    private readonly IManagerFileRepository _managerFileRepository = managerFileRepository;
    private readonly ILogger<GetFilesByNameHandler> _logger = logger;

    public async ValueTask<Result<GetFilesByNameResponse>> Handle(GetFilesByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var files = await _managerFileRepository.GetByNameAsync(request.FileName, cancellationToken);

            var infoFiles = files
                .Select(f => new FileInfoResponse(f.FileId, f.FileName, f.ContentType, f.Size, f.CreatedAt))
                .ToList()
                .AsReadOnly();

            var response = new GetFilesByNameResponse(infoFiles);

            return Result.Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting files by name: {FileName}", request.FileName);

            return Result.Fail(new Error("Error getting files by name").CausedBy(ex));
        }
    }
}
