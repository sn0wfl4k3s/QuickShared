using FluentResults;
using Mediator;

namespace QuickShared.Application.Features.GetFile;

public record GetFileRequest(Guid FileId) : IRequest<Result<GetFileResponse>>;
