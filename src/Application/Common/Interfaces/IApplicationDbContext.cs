using CleanArchiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchiTemplate.Application.Common.Interfaces
{
  public interface IApplicationDbContext
  {
    DbSet<Movie> Movies { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  }
}
