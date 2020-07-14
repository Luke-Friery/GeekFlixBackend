using CleanArchiTemplate.Domain.Common;

namespace CleanArchiTemplate.Domain.Entities
{
  public class Movie : AuditableEntity
  {
    public int Id { get; set; }
    public string Url { get; set; }
    public int Popularity { get; set; }
    public string Emotion { get; set; }
    public MovieDetail MovieDetails { get; set; }

  }
}