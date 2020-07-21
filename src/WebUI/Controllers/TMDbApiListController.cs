using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using CleanArchiTemplate.WebUI.Controllers;
using Domain.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Application.Common.Interfaces;

namespace WebUI.Controllers
{
  public class TMDbApiListController : ApiController
  {
    //private readonly ICacheContext _cache;
    public TMDbApiListController(/*ICacheContext cache*/)
    {
     // _cache = cache;
    }

    [HttpGet("{page}")]
    public ActionResult GetListPageNum(int page)
    {
      //the following is being abstracted
      string apiKey = "f68c64ab26f5bb2a81e09f4af4dff582";
      HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/discover/movie?api_key=" + apiKey + "&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=" + page + "&with_genres=878") as HttpWebRequest;
      string apiResponse = DoWebRequest(apiRequest);
      var movieListings = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieList>(apiResponse);
      //the abstraction ends here

      string serialData = Newtonsoft.Json.JsonConvert.SerializeObject(movieListings);
      //serialData = serialData.Replace("/", "\\/");

      return Ok(serialData);
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