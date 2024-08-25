using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace Books.Models;
public class Author
{
    // Primärnyckel (PK) i databasen, används för att unikt identifiera varje författare
    public int Id { get; set; }

    // Författarens namn, detta fält är obligatoriskt och har valideringsregler för längd
    [Required(ErrorMessage = "Ange ett författarnamn!")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Färst 3 tecken, max 100 tecken!")]
    public string? AuthorName { get; set; }
}
