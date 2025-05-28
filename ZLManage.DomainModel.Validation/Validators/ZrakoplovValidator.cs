using FluentValidation;
using ZLManage.DomainModel.Models;
using ZLManage.DomainModel.Models.Requests;

namespace ZLManage.DomainModel.Validation.Validators;

public class ZrakoplovValidator : AbstractValidator<Zrakoplov>
{
    public ZrakoplovValidator()
    {
        RuleFor(x => x.Model)
            .NotEmpty()
            .WithMessage("Model is required.");

        RuleFor(x => x.Registracija)
            .NotEmpty()
            .WithMessage("Registracija is required.")
            .Matches("^[A-Z]{2}-[A-Z][A-Z0-9]+$")
            .WithMessage("Registracija must be in format “XX-Y123” (e.g. HR-E820, IT-ATR72).");
    }
}