using NUnit.Framework;
using System.IO;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Caching.Commands;
using Moq;
using Infrastructure.TMDbConnection.Commands;

namespace Application.UnitTests.TMDbApiTests
{

  public class PageCacheOrganiserTest
  {
    [Test]
    public void TestCache_NewCacheAddElement_TheCacheHasANewElement()
    {
      //arrange
      var organiser = new PageCacheOrganiser(null);
      

      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/MovieResults1.json"));
      var fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieList>(text);

      int expectedId = 1;

      var serviceMock = new Mock<ITMDbListService>();

      serviceMock
        .Setup(m=>m.TMDbGetPage(expectedId))
        .Returns(fromjsonfile);

      //act (or maybe this line is still arrange?)
      MovieListCache samplejson = organiser.MapToListCache(fromjsonfile);

      //act 
      organiser.CacheAddPage(samplejson);
      MovieListCache cachedData = organiser.CacheGetPage(1);

      //sample assertions for testing purposes
      samplejson.references.Should().Equals(1);
      samplejson.results.Should().NotBeNull();
      //assert
      cachedData.Should().NotBeNull();
      cachedData.page.Should().Equals(1);
      cachedData.references.Should().Equals(2);
      cachedData.total_results.Should().Equals(fromjsonfile.total_results);
      cachedData.total_pages.Should().Equals(fromjsonfile.total_pages);
      cachedData.results.Should().NotBeNull();

      //how to do this?
      //cachedData.results.Should().BeOfType(List<Result>)
    }

    [Test]
    public void TestCache_RemoveElementFromCacheWithOneElement_RemovesElement()
    {

    }

    // [Test]
    // public void TestCache_PruneCache_RemovesAllElementsThatAreAboveCacheMax()
    // {

    // }

    // [Test]
    // public void TestCache_CacheAdd_PruneFirstWhenCacheIsAboveMaxElements()
    // {

    // }

    // [Test]
    // public void TestCache_CacheAdd_IncrementElementPopularity()
    // {

    // }

    // [Test]
    // public void TestCache_ExistingElementsCacheOrdered_ReturnsTrueWhenOrderedDescPopularity()
    // {

    // }
  }
}