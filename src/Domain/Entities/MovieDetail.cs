using CleanArchiTemplate.Domain.Common;

namespace CleanArchiTemplate.Domain.Entities
{
  public class MovieDetail : AuditableEntity
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
    public Movie Movies { get; set; }
  }
}