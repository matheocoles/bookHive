using BookHive.DTOs.Authors.Request;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Authors;

public class UpdateAuthorDtoValidator : Validator<UpdateAuthorsDto>
{
    public UpdateAuthorDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("L'identifiant est invalide.");
        
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(2);
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(2);
        
        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .Must(d => d < DateOnly.FromDateTime(DateTime.Now));
        
        RuleFor(x => x.Nationality)
            .NotEmpty()
            .MaximumLength(60);
        
        When(x => x.Biography != null, () => {
            RuleFor(x => x.Biography)
                .MaximumLength(2000);
        });
    }
}