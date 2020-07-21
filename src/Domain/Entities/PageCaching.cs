using System.Collections.Generic;

namespace Domain.Entities
{
  public class PageCache
  {
    public int hits { get; set; }
    public int page { get; set; }
    public int total_results { get; set; }
    public int total_pages { get; set; }
    public List<Result> results { get; set; }
  }
}