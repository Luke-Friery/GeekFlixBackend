using System;

namespace Domain.Entities
{
  public class TheMovieDb
  {
    public Boolean adult { get; set; }
    public object[] genres { get; set; }
    public int id { get; set; }
    public string imdb_id { get; set; }
    public string overview { get; set; }
    public int popularity { get; set; }
    public string poster_path { get; set; }
    public object[] production_companies { get; set; }
    public string release_date { get; set; }
    public int runtime { get; set; }
    public object[] spoken_languages { get; set; }
    public string status { get; set; }
    public string title { get; set; }
    public float vote_average { get; set; }
    public int vote_count { get; set; }



  }
}