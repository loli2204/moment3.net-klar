using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Books.Models;
namespace Books.Data;

// DB
public class BooksDbContext : DbContext {
    public BooksDbContext(DbContextOptions options) : base(options) {}

    public DbSet<Book> Books {get; set;}
    public DbSet<Author> Authors {get; set;}
    public DbSet<Loan> Loans {get; set;}

}