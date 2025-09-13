using FluentValidation;

namespace QuickShared.Application.Features.SaveFile;

public class SaveFileValidator : AbstractValidator<SaveFileCommand>
{
    public SaveFileValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required.")
            .Must(file => file != null && file.Length > 0).WithMessage("File must not be empty.");

        RuleFor(x => x.File.FileName)
            .NotEmpty().WithMessage("File name is required.")
            .When(x => x.File != null);

        RuleFor(x => x.File.ContentType)
            .NotEmpty().WithMessage("Content type is required.")
            .When(x => x.File != null);
    }
}
