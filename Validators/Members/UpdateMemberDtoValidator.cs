using BookHive.DTOs.Member.Request;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Members;

public class UpdateMemberDtoValidator : Validator<UpdateMemberDto>
{
    public UpdateMemberDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("L'identifiant est invalide.");
        
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
    }
}