using BookHive.DTOs.Book.Response;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Books;

public class GetBookDtoValidator : Validator<GetBookDto>
{
    public GetBookDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Isbn)
            .NotEmpty()
            .Matches(@"^\d{13}$");

        RuleFor(x => x.Genre).NotEmpty();

    }
}