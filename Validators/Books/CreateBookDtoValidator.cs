using BookHive.DTOs.Book.Request;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Books;

public class CreateBookDtoValidator : Validator<CreateBookDto>
{
    public CreateBookDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Le titre est obligatoire.")
            .MaximumLength(200)
            .WithMessage("Le titre ne doit pas dépasser 200 caractères.");

        RuleFor(x => x.Isbn)!.IsValidISBN13();

        RuleFor(x => x.PageCount)
            .GreaterThan(0);

        RuleFor(x => x.PublishedDate)
            .NotEmpty()
            .Must(d => d <= DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("La date de publication ne peut pas être dans le futur");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0);
    }
}