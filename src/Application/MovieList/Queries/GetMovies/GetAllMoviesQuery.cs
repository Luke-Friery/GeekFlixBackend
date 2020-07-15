using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchiTemplate.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MovieList.Queries.GetMovies
{
  public class GetAllMoviesQuery : IRequest
  {
  }


  // public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, MoviesVm>
  // {
  //   private readonly IApplicationDbContext _context;
  //   private readonly IMapper _mapper;
  //   public GetAllMoviesQueryHandler(IApplicationDbContext context, IMapper mapper)
  //   {
  //     _context = context;
  //     _mapper = mapper;
  //   }



  //   public async Task<MoviesVm> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
  //   {
  //     return new MoviesVm
  //     {
  //       Lists = await _context.Movies
  //         .ProjectTo<MovieListAllDto>(_mapper.ConfigurationProvider)
  //         .OrderBy(t => t.Title)
  //         .ToListAsync(cancellationToken)
  //     };
  //   }
  // }
}