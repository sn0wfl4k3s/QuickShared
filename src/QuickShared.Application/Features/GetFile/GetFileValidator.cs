using FluentValidation;

namespace QuickShared.Application.Features.GetFile;

public class GetFileValidator : AbstractValidator<GetFileRequest>
{
    public GetFileValidator()
    {
        RuleFor(x => x.FileId)
            .NotEmpty().WithMessage("FileId is required.");
    }
}
