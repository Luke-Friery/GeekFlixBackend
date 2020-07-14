using System.Threading;
using System.Threading.Tasks;
using CleanArchiTemplate.Application.Common.Interfaces;
using CleanArchiTemplate.Domain.Entities;

namespace Application.MovieList.Commands
{
  public class AddMovie
  {
    private readonly IApplicationDbContext _context;

    public AddMovie(IApplicationDbContext context)
    {
      _context = context;
    }

//     public async Task<int> Handle(NewMovieDTO data, CancellationToken cancellationToken)
//     {
//       var entity = new Movie
//       {
//         //Id = data.ListId,
//         Title = data.Title,
//         Url = data.Url,
//       };

//       _context.Movies.Add(entity);

//       await _context.SaveChangesAsync(cancellationToken);

//       return entity.Id;
//     }
//   }
  }
}