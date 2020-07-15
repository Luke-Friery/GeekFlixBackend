using System.Threading;
using System.Threading.Tasks;
using CleanArchiTemplate.Application.Common.Interfaces;
using CleanArchiTemplate.Domain.Entities;
using MediatR;

namespace Application.MovieList.Commands.AddMovie
{
  public class AddMovieCommand : IRequest<int>
  {
    public string Title { get; set; }
    public string Url { get; set; }
    public string PhotoUrl { get; set; }
    public int ReleasedYear { get; set; }


  }
  public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, int>
  {
    private readonly IApplicationDbContext _context;
    public AddMovieCommandHandler(IApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<int> Handle(AddMovieCommand request, CancellationToken cancellationToken)
    {
      var entity = new Movie
      {
        Title = request.Title,
        Url = request.Url,
        PhotoUrl = request.PhotoUrl,
        ReleasedYear = request.ReleasedYear
      };

      _context.Movies.Add(entity);

      await _context.SaveChangesAsync(cancellationToken);

      return entity.Id;
    }
  }
}