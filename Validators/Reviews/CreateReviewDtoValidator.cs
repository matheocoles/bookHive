using BookHive.DTOs.Review.Request;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Reviews;

public class CreateReviewDtoValidator : Validator<CreateReviewDto>
{
    public CreateReviewDtoValidator()
    {
        RuleFor(x => x.MemberId)
            .GreaterThan(0);

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);

        When(x => x.Comment != null, () => {
            RuleFor(x => x.Comment).MaximumLength(1000);
            }); 
    }
}