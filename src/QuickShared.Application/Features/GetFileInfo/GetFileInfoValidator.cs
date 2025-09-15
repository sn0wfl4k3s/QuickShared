using FluentValidation;

namespace QuickShared.Application.Features.GetFileInfo;

public class GetFileInfoValidator : AbstractValidator<GetFileInfoRequest>
{
    public GetFileInfoValidator()
    {
        RuleFor(x => x.FileId)
            .NotEmpty().WithMessage("FileId is required.");
    }
}
