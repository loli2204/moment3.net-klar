using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace Books.Models;
public class Loan
{
    // Primärnyckel (PK) i databasen, används för att unikt identifiera varje lån
    public int Id { get; set; }

    // Främmande nyckel (FK) för den lånade boken
    public int? BookId { get; set; }
    
    // Bokobjektet som är kopplat till lånet, navigeringsegenskap för relation
    public Book? Book { get; set; }

    // Låntagare (användare), obligatoriskt fält med valideringsregler för längd
    [Required(ErrorMessage = "Ange namn på den som lånar!")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Färst 3 tecken, max 100 tecken!")]
    public string? Borrower { get; set; }

    // Datum när boken lånades, obligatoriskt fält
    [Required(ErrorMessage = "Välj ett datum när boken lånades ut!")]
    public DateOnly Borrowed { get; set; }
}
