using System;
using System.Collections.Generic;
using CleanArchiTemplate.Domain.Common;

namespace CleanArchiTemplate.Domain.Entities
{
  public class Movie : AuditableEntity
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Content { get; set; }
    public int Popularity { get; set; }
    public string Emotion { get; set; }
    public int Rating { get; set; }
    public string PhotoUrl { get; set; }
    public int ReleasedYear { get; set; }
  }
}