using System.Threading;
using System.Threading.Tasks;
using CleanArchiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
  public interface IMovieDbContext
  {
    DbSet<Movie> Movies { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  }
}