using BookHive.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Data;

public class BookHiveDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=romaric-thibault.fr;" +
            "Database=matheo_BookHive;" +
            "User Id=matheo;" +
            "Password=Onto9-Cage-Afflicted;" +
            "TrustServerCertificate=true;"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Configuration des contraintes d'unicité (Rappel Étape 2)
    modelBuilder.Entity<Review>()
        .HasIndex(r => new { r.BookId, r.MemberId });

    // --- SEED DATA (Étape 6) ---

    modelBuilder.Entity<Author>().HasData(
        new Author { Id = 1, FirstName = "Albert", LastName = "Camus", BirthDate = new DateOnly(1913, 11, 7), Nationality = "Française" },
        new Author { Id = 2, FirstName = "Frank", LastName = "Herbert", BirthDate = new DateOnly(1920, 10, 8), Nationality = "Américaine" },
        new Author { Id = 3, FirstName = "J.K.", LastName = "Rowling", BirthDate = new DateOnly(1965, 7, 31), Nationality = "Britannique" }
    );

    modelBuilder.Entity<Book>().HasData(
        new Book { Id = 1, Title = "L'Étranger", Isbn = "9782070360024", PageCount = 184, PublishedDate = new DateOnly(1942, 6, 15), Genre = "Roman", AuthorId = 1 },
        new Book { Id = 2, Title = "La Peste", Isbn = "9782070360420", PageCount = 352, PublishedDate = new DateOnly(1947, 6, 10), Genre = "Roman", AuthorId = 1 },
        new Book { Id = 3, Title = "Dune", Isbn = "9782266233200", PageCount = 712, PublishedDate = new DateOnly(1965, 8, 1), Genre = "Science-Fiction", AuthorId = 2 },
        new Book { Id = 4, Title = "Le Messie de Dune", Isbn = "9782266233217", PageCount = 320, PublishedDate = new DateOnly(1969, 1, 1), Genre = "Science-Fiction", AuthorId = 2 },
        new Book { Id = 5, Title = "Harry Potter à l'école des sorciers", Isbn = "9782070518425", PageCount = 305, PublishedDate = new DateOnly(1997, 6, 26), Genre = "Fantasy", AuthorId = 3 },
        new Book { Id = 6, Title = "Harry Potter et la Chambre des secrets", Isbn = "9782070541295", PageCount = 361, PublishedDate = new DateOnly(1998, 7, 2), Genre = "Fantasy", AuthorId = 3 }
    );

    modelBuilder.Entity<Member>().HasData(
        new Member { Id = 1, Email = "jean.dupont@email.com", FirstName = "Jean", LastName = "Dupont", MembershipDate = new DateOnly(2025, 1, 10), IsActive = true },
        new Member { Id = 2, Email = "marie.curie@email.com", FirstName = "Marie", LastName = "Curie", MembershipDate = new DateOnly(2025, 2, 15), IsActive = true },
        new Member { Id = 3, Email = "lucas.bernard@email.com", FirstName = "Lucas", LastName = "Bernard", MembershipDate = new DateOnly(2025, 3, 01), IsActive = true },
        new Member { Id = 4, Email = "sophie.martin@email.com", FirstName = "Sophie", LastName = "Martin", MembershipDate = new DateOnly(2025, 1, 20), IsActive = false } // Membre inactif [cite: 60]
    );

    modelBuilder.Entity<Loan>().HasData(
        // Emprunts terminés
        new Loan { Id = 1, BookId = 1, MemberId = 1, LoanDate = new DateOnly(2026, 1, 5), DueDate = new DateOnly(2026, 2, 4), ReturnDate = new DateOnly(2026, 1, 20) },
        new Loan { Id = 2, BookId = 2, MemberId = 2, LoanDate = new DateOnly(2026, 1, 10), DueDate = new DateOnly(2026, 2, 9), ReturnDate = new DateOnly(2026, 2, 1) },
        new Loan { Id = 3, BookId = 5, MemberId = 3, LoanDate = new DateOnly(2026, 2, 1), DueDate = new DateOnly(2026, 2, 28), ReturnDate = new DateOnly(2026, 2, 15) },
        new Loan { Id = 4, BookId = 3, MemberId = 1, LoanDate = new DateOnly(2026, 3, 1), DueDate = new DateOnly(2026, 3, 30), ReturnDate = null },
        new Loan { Id = 5, BookId = 6, MemberId = 2, LoanDate = new DateOnly(2026, 3, 5), DueDate = new DateOnly(2026, 4, 4), ReturnDate = null }
    );

    modelBuilder.Entity<Review>().HasData(
        new Review { Id = 1, BookId = 1, MemberId = 1, Rating = 5, Comment = "Un classique absolu.", CreatedAt = DateTime.Now },
        new Review { Id = 2, BookId = 3, MemberId = 2, Rating = 5, Comment = "L'univers est incroyablement riche.", CreatedAt = DateTime.Now },
        new Review { Id = 3, BookId = 5, MemberId = 3, Rating = 4, Comment = "Très bonne introduction à la magie.", CreatedAt = DateTime.Now },
        new Review { Id = 4, BookId = 2, MemberId = 1, Rating = 4, Comment = "Une lecture poignante.", CreatedAt = DateTime.Now }
    );
}
}