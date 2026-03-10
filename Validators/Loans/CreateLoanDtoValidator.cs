using BookHive.DTOs.Loan.Request;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Loans;

public class CreateLoanDtoValidator : Validator<CreateLoanDto>
{
    public CreateLoanDtoValidator()
    {
        RuleFor(x => x.BookId)
            .GreaterThan(0);
        
        RuleFor(x => x.MemberId)
            .GreaterThan(0);
        
        RuleFor(x => x.LoanDate)
            .NotEmpty(); 

        RuleFor(x => x.DueDate)
            .NotEmpty()
            .Must((dto, dueDate) => dueDate > dto.LoanDate)
            .WithMessage("La date de retour doit être supérieure à la date d'emprunt.")
            .Must((dto, dueDate) =>
            {
                var dayOfWeek = dto.LoanDate.DayOfWeek;
                bool isWeekend = dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
                int maxDays = isWeekend ? 14 : 30;
                return dueDate <= dto.LoanDate.AddDays(maxDays);
            })
            .WithMessage("La durée max est de 14j (week-end) ou 30j (semaine).");
    }
}