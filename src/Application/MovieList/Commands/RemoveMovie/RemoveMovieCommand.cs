using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchiTemplate.Application.Common.Exceptions;
using CleanArchiTemplate.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MovieList.Commands.RemoveMovie
{
  public class RemoveMovieCommand : IRequest
  {
    public int Id { get; set; }
  }

  public class RemoveMovieCommandHandler : IRequestHandler<RemoveMovieCommand>
  {
    private readonly IApplicationDbContext _context;

    public RemoveMovieCommandHandler(IApplicationDbContext context)
    {
      _context = context;
    }



    public async Task<Unit> Handle(RemoveMovieCommand request, CancellationToken cancellationToken)
    {
      var entity = await _context.Movies.Where(l =>l.Id == request.Id).SingleOrDefaultAsync(cancellationToken);
        
      if (entity == null)
      {
        throw new NotFoundException(nameof(MovieList), request.Id);
      }

      _context.Movies.Remove(entity);

      await _context.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }
  }
}