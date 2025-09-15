using FluentValidation;

namespace QuickShared.Application.Features.GetFilesByName;

public class GetFilesByNameValidator : AbstractValidator<GetFilesByNameRequest>
{
    public GetFilesByNameValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name is required.");
    }
}
