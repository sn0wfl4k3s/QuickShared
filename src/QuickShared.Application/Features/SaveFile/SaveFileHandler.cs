using FluentResults;
using Mediator;
using Microsoft.Extensions.Logging;
using QuickShared.Domain.Entities;
using QuickShared.Domain.Repositories;
using QuickShared.Domain.Services;

namespace QuickShared.Application.Features.SaveFile;

internal sealed class SaveFileHandler(
    IStorageService storageService,
    IManagerFileRepository managerFileRepository,
    ILogger<SaveFileHandler> logger)
    : IRequestHandler<SaveFileCommand, Result<SaveFileResponse>>
{
    private readonly IStorageService _storageService = storageService;
    private readonly IManagerFileRepository _repository = managerFileRepository;
    private readonly ILogger<SaveFileHandler> _logger = logger;

    public async ValueTask<Result<SaveFileResponse>> Handle(SaveFileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var uploadCommand = new UploadFileCommand(
                request.File.OpenReadStream(),
                request.File.FileName,
                request.File.ContentType);

            var resultUpload = await _storageService.SaveFileAsync(uploadCommand, cancellationToken);

            var managerFile = new ManagerFile
            {
                Id = resultUpload.FileId,
                FileSize = request.File.Length,
                FileUrl = resultUpload.FileUrl,
                FileName = request.File.FileName,
                FileExtension = Path.GetExtension(request.File.FileName),
                ContentType = request.File.ContentType,
            };

            await _repository.AddAsync(managerFile, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Ok(new SaveFileResponse(resultUpload.FileId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving file");

            return Result.Fail(ex.Message);
        }
    }
}