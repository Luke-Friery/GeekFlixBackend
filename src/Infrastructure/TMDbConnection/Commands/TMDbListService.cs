using System.IO;
using System.Net;
using Application.Common.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;

namespace Infrastructure.TMDbConnection.Commands
{
  public class TMDbListService : ITMDbListService
  {
    public MovieList TMDbGetPage(int page)
    {
      string apiKey = "f68c64ab26f5bb2a81e09f4af4dff582";
      HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/discover/movie?api_key=" + apiKey + "&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=" + page + "&with_genres=878") as HttpWebRequest;
      string apiResponse = DoWebRequest(apiRequest);
      var movieListings = JsonConvert.DeserializeObject<MovieList>(apiResponse);
      return movieListings;
    }

    private static string DoWebRequest(HttpWebRequest apiRequest)
    {
      string apiResponse;
      using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
      {
        StreamReader reader = new StreamReader(response.GetResponseStream());
        apiResponse = reader.ReadToEnd();
      }
      return apiResponse;
    }
  }
}