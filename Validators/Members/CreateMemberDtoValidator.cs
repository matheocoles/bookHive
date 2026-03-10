using BookHive.DTOs.Member.Request;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Members;

public class CreateMemberDtoValidator : Validator<CreateMemberDto>
{
    public CreateMemberDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(2); 

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(2);

        RuleFor(x => x.MembershipDate)
            .NotEmpty()
            .Must(date => date <= DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("La date d'adhésion ne doit pas être dans le futur.");
    }
}