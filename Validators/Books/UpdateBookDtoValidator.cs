using BookHive.DTOs.Book.Request;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Books;

public class UpdateBookDtoValidator : Validator<UpdateBookDto>
{
    public UpdateBookDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("L'identifiant est invalide.");
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Le titre est obligatoire.")
            .MaximumLength(200)
            .WithMessage("Le titre ne doit pas dépasser 200 caractères.");

        RuleFor(x => x.Isbn)
            .NotEmpty()
            .Matches(@"^\d{13}$")
            .WithMessage("L'ISBN doit contenir 13 chiffres");

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