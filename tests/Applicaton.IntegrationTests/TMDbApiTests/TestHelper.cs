using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Domain.Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Applicaton.IntegrationTests.TMDbApiTests
{

  using static Testing;

  public class TestHelper
  {
    [Test]
    public void TestGetText()
    {
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/MovieResults1.json"));
      //TestContext.Out.WriteLine("******LOOK HERE******: \n" + text);

      string apiKey = "f68c64ab26f5bb2a81e09f4af4dff582";
      HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/discover/movie?api_key=" + apiKey + "&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&with_genres=878") as HttpWebRequest;

      string apiResponse = DoWebRequest(apiRequest);

      var fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieList>(text);
      var fromwebrequest = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieList>(apiResponse);
      
      Assert.NotNull(fromwebrequest.results);
      Assert.NotNull(fromwebrequest);
      Assert.IsInstanceOf(typeof(MovieList), fromwebrequest);
      Assert.IsInstanceOf(typeof(List<Result>), fromwebrequest.results);

      Assert.NotNull(fromjsonfile.results);
      Assert.NotNull(fromjsonfile);
      Assert.IsInstanceOf(typeof(MovieList), fromjsonfile);
      Assert.IsInstanceOf(typeof(List<Result>), fromjsonfile.results);
      //how do I properly compare the json and webrequest objects? (e.g. make sure each field is correctly filled)
      
    }









    //below method is duplicted from TMDbApiController.cs
    private string DoWebRequest(HttpWebRequest apiRequest)
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