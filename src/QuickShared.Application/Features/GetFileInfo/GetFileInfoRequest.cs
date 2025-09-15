using FluentResults;
using Mediator;

namespace QuickShared.Application.Features.GetFileInfo;

public record GetFileInfoRequest(Guid FileId) : IRequest<Result<GetFileInfoResponse>>;
