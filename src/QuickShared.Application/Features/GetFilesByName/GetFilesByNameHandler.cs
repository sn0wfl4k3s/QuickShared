using Mediator;
using QuickShared.Domain.Repositories;

namespace QuickShared.Application.Features.GetFilesByName;

public class GetFilesByNameHandler(IManagerFileRepository managerFileRepository) : IRequestHandler<GetFilesByNameRequest, GetFilesByNameResponse>
{
    private readonly IManagerFileRepository _managerFileRepository = managerFileRepository;

    public async ValueTask<GetFilesByNameResponse> Handle(GetFilesByNameRequest request, CancellationToken cancellationToken)
    {
        var files = await _managerFileRepository.GetByNameAsync(request.FileName, cancellationToken);

        var infoFiles = files
            .Select(f => new FileInfoResponse(f.FileId, f.FileName, f.ContentType, f.Size, f.CreatedAt))
            .ToList()
            .AsReadOnly();

        var response = new GetFilesByNameResponse(infoFiles);

        return response;
    }
}
