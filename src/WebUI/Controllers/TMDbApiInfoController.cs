using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using CleanArchiTemplate.WebUI.Controllers;
using Domain.Entities;

namespace WebUI.Controllers
{
  public class TMDbApiInfoController : ApiController
  {
    [HttpGet("{movieId}")]
    public string GetListPageNum(int movieId)
    {
    //if cache exists
      //use cached data

    //if cache doesn't exist
      //get from TMDb
      string apiKey = "f68c64ab26f5bb2a81e09f4af4dff582";
      HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/movie/"+movieId+"?api_key=" +apiKey+ "&language=en-US") as HttpWebRequest;
      string apiResponse = DoWebRequest(apiRequest);
      var movieInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieInfo>(apiResponse);
      //insert functions to alter movieListings here (e.g. add emotions)
      //add data to cache?

      string SerialData = Newtonsoft.Json.JsonConvert.SerializeObject(movieInfo);
      return SerialData;
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