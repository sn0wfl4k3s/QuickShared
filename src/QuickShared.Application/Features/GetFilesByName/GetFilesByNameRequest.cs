using Mediator;

namespace QuickShared.Application.Features.GetFilesByName;

public record GetFilesByNameRequest(string FileName) : IRequest<GetFilesByNameResponse>;
