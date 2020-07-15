using System.Collections.Generic;

namespace Application.MovieList.Queries.GetMovies
{
  public class MoviesVm
  {
    public IList<MovieListAllDto> AllList { get; set; }
    public IList<MovieDetailsDto> Details { get; set; }
  }
}