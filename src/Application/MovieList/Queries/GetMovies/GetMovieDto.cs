using System;

namespace Application.MovieList.Queries.GetMovies
{
  public class GetMovieDto
  {
    public Boolean adult { get; set; }
    public string backdrop_path { get; set; }
    public object belongs_to_collection { get; set; }
    public float budget { get; set; }
    public object[] genres { get; set; }
    public string homepage { get; set; }
    public int id { get; set; }
    public string imdb_id { get; set; }
    public string origional_language { get; set; }
    public string origional_title { get; set; }
    public string overview { get; set; }
    public int popularity { get; set; }
    public string poster_path { get; set; }
    public object[] production_companies { get; set; }
    public string release_date { get; set; }
    public int runtime { get; set; }
    public string status { get; set; }
    public float vote_average { get; set; }
    public int vote_count { get; set; }
  }
}