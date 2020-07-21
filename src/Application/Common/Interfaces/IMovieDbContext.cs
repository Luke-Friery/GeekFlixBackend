using System.Threading;
using System.Threading.Tasks;
using CleanArchiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
  public interface IMovieDbContext
  {
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  }
}