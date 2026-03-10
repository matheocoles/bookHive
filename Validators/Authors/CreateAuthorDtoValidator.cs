using BookHive.DTOs.Authors.Request;
using FastEndpoints;
using FluentValidation;
using FluentValidation.Validators;

namespace BookHive.Validators.Authors;

public class CreateAuthorDtoValidator : Validator<CreateAuthorDto>
{
    public CreateAuthorDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Le prénom est obligatoire")
            .MinimumLength(2)
            .MaximumLength(100);
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Le nom est obligatoire")
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage("La date de naissance est obligatoire");

        RuleFor(x => x.Nationality)
            .NotEmpty()
            .MaximumLength(60);

        When(x => x.Biography != null, () =>
        {
            RuleFor(x => x.Biography)
                .MaximumLength(2000);
        });
    }
}