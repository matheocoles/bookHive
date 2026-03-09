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

    protected override void OnModelCreating(ModelBuilder modelBuilder) {}
}