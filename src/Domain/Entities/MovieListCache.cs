using System.Collections.Generic;

namespace Domain.Entities
{
  public class MovieListCache
  {
    public int references { get; set; }
    public int page { get; set; }
    public int total_results { get; set; }
    public int total_pages { get; set; }
    public List<Result> results { get; set; }
  }

}