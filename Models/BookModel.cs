using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace Books.Models;
public class Book
{
    // Bok-id, används som primärnyckel i databasen
    public int Id { get; set; }

    // Indikerar om boken är utlånad, standardvärdet är false (ej utlånad)
    public bool Borrowed { get; set; } = false;

    // Boknamn, detta fält är obligatoriskt och har valideringsregler för längd
    [Required(ErrorMessage = "Ange ett boknamn!")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Färst 3 tecken, max 100 tecken!")]
    public string? Name { get; set; }

    // Författar-id, används som främmande nyckel (FK) för att koppla en bok till en författare
    public int? AuthorId { get; set; }
    
    // Författarobjektet kopplat till boken, navigeringsegenskap för relation
    public Author? Author { get; set; }
}
