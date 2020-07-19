using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using CleanArchiTemplate.WebUI.Controllers;
using Domain.Entities;

namespace WebUI.Controllers
{
  public class TMDbApiListController : ApiController
  {
    [HttpGet("{page}")]
    public MovieList GetListPageNum(int page)
    {
    //if cache exists
      //use cached data

    //if cache doesn't exist
      //get from TMDb
      string apiKey = "f68c64ab26f5bb2a81e09f4af4dff582";
      HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/discover/movie?api_key=" + apiKey + "&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page="+page+"&with_genres=878") as HttpWebRequest;
      string apiResponse = DoWebRequest(apiRequest);
      var movieListings = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieList>(apiResponse);
      //insert functions to alter movieListings here (e.g. add emotions)
      //add data to cache?
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