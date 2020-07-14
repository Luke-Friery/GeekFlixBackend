using CleanArchiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchiTemplate.Infrastructure.Persistence
{
  public class MovieDbContext : DbContext
  {
    public MovieDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieDetail> MovieDetails { get; set; }
  }
}