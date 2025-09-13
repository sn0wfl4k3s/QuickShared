using FluentResults;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace QuickShared.Application.Features.SaveFile;

public record SaveFileCommand(IFormFile File) : IRequest<Result<SaveFileResponse>>;
