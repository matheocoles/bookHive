using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookHive.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Reviews",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Books",
                newName: "Isbn");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReturnDate",
                table: "Loans",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "BirthDate", "FirstName", "LastName", "Nationality" },
                values: new object[,]
                {
                    { 1, null, new DateOnly(1913, 11, 7), "Albert", "Camus", "Française" },
                    { 2, null, new DateOnly(1920, 10, 8), "Frank", "Herbert", "Américaine" },
                    { 3, null, new DateOnly(1965, 7, 31), "J.K.", "Rowling", "Britannique" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "FirstName", "IsActive", "LastName", "MembershipDate" },
                values: new object[,]
                {
                    { 1, "jean.dupont@email.com", "Jean", true, "Dupont", new DateOnly(2025, 1, 10) },
                    { 2, "marie.curie@email.com", "Marie", true, "Curie", new DateOnly(2025, 2, 15) },
                    { 3, "lucas.bernard@email.com", "Lucas", true, "Bernard", new DateOnly(2025, 3, 1) },
                    { 4, "sophie.martin@email.com", "Sophie", false, "Martin", new DateOnly(2025, 1, 20) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Genre", "Isbn", "PageCount", "PublishedDate", "Summary", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Roman", "9782070360024", 184, new DateOnly(1942, 6, 15), null, "L'Étranger" },
                    { 2, 1, "Roman", "9782070360420", 352, new DateOnly(1947, 6, 10), null, "La Peste" },
                    { 3, 2, "Science-Fiction", "9782266233200", 712, new DateOnly(1965, 8, 1), null, "Dune" },
                    { 4, 2, "Science-Fiction", "9782266233217", 320, new DateOnly(1969, 1, 1), null, "Le Messie de Dune" },
                    { 5, 3, "Fantasy", "9782070518425", 305, new DateOnly(1997, 6, 26), null, "Harry Potter à l'école des sorciers" },
                    { 6, 3, "Fantasy", "9782070541295", 361, new DateOnly(1998, 7, 2), null, "Harry Potter et la Chambre des secrets" }
                });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "BookId", "Date", "DueDate", "LoanDate", "MemberId", "ReturnDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 2, 4), new DateOnly(2026, 1, 5), 1, new DateOnly(2026, 1, 20) },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 2, 9), new DateOnly(2026, 1, 10), 2, new DateOnly(2026, 2, 1) },
                    { 3, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 2, 28), new DateOnly(2026, 2, 1), 3, new DateOnly(2026, 2, 15) },
                    { 4, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 3, 30), new DateOnly(2026, 3, 1), 1, null },
                    { 5, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2026, 4, 4), new DateOnly(2026, 3, 5), 2, null }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Comment", "CreatedAt", "MemberId", "Rating" },
                values: new object[,]
                {
                    { 1, 1, "Un classique absolu.", new DateTime(2026, 3, 9, 15, 32, 16, 601, DateTimeKind.Local).AddTicks(4314), 1, 5 },
                    { 2, 3, "L'univers est incroyablement riche.", new DateTime(2026, 3, 9, 15, 32, 16, 601, DateTimeKind.Local).AddTicks(4318), 2, 5 },
                    { 3, 5, "Très bonne introduction à la magie.", new DateTime(2026, 3, 9, 15, 32, 16, 601, DateTimeKind.Local).AddTicks(4321), 3, 4 },
                    { 4, 2, "Une lecture poignante.", new DateTime(2026, 3, 9, 15, 32, 16, 601, DateTimeKind.Local).AddTicks(4324), 1, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId_MemberId",
                table: "Reviews",
                columns: new[] { "BookId", "MemberId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookId_MemberId",
                table: "Reviews");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reviews",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Isbn",
                table: "Books",
                newName: "ISBN");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReturnDate",
                table: "Loans",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");
        }
    }
}
