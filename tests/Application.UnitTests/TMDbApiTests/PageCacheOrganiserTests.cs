using NUnit.Framework;
using System.IO;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Caching.Commands;
using Moq;
using Infrastructure.TMDbConnection.Commands;
using System.Collections.Generic;
using System;

namespace Application.UnitTests.TMDbApiTests
{

  public class PageCacheOrganiserTests
  {
    [Test]
    public void TestCache_MappingMovieListToMovieListCache_ReturnsAMovieListCacheElement()
    {
      //arrange
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/MovieResults1.json"));
      MovieList fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieList>(text);
      var organiser = new PageCacheOrganiser(null);
      //act
      MovieListCache cachedFormat = organiser.MapToListCache(fromjsonfile);
      //assert
      cachedFormat.Should().NotBeNull();
      cachedFormat.page.Should().Equals(1);
      cachedFormat.references.Should().Equals(1);
      cachedFormat.total_results.Should().Equals(fromjsonfile.total_results);
      cachedFormat.total_pages.Should().Equals(fromjsonfile.total_pages);
      cachedFormat.results.Should().NotBeNull();
    }


    [Test]
    public void TestCache_NewCacheAddElement_TheCacheReturnsTheNewElement()
    {
      //arrange
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/MovieResults1.json"));
      var fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieList>(text);
      var organiser = new PageCacheOrganiser(null);
      MovieListCache samplejson = organiser.MapToListCache(fromjsonfile);
      //act
      organiser.CacheAddPage(samplejson);
      MovieListCache cachedData = organiser.ReturnCacheEntry(1);
      //assert
      cachedData.Should().NotBeNull();
      cachedData.page.Should().Be(1);
      cachedData.references.Should().Be(1);
      cachedData.total_results.Should().Be(fromjsonfile.total_results);
      cachedData.total_pages.Should().Be(fromjsonfile.total_pages);
      cachedData.results.Should().NotBeNull();
    }


    [Test]
    public void TestCache_NewCacheGetElement_GetsFromMoqThenFromCache()
    {
      //arrange
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/MovieResults1.json"));
      var fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieList>(text);
      int expectedId = 1;
      var serviceMock = CreateServiceMockForGetPage(fromjsonfile);
      var organiser = new PageCacheOrganiser(serviceMock.Object);
      //act 
      MovieListCache cachedData = organiser.CacheGetPage(expectedId); //no data in cache, should get from moq
      cachedData = organiser.CacheGetPage(expectedId); //data in cache, should get from cache
      //assert
      cachedData.Should().NotBeNull();
      cachedData.page.Should().Be(1);
      cachedData.references.Should().Be(2); //if this is 2 indicates it successfully grabbed from cache
      cachedData.total_results.Should().Be(fromjsonfile.total_results);
      cachedData.total_pages.Should().Be(fromjsonfile.total_pages);
      cachedData.results.Should().NotBeNull();
      //how to do this?
      //cachedData.results.Should().BeOfType(List<Result>)
    }

    private static Mock<ITMDbService> CreateServiceMockForGetPage(MovieList fromjsonfile)
    {
      var serviceMock = new Mock<ITMDbService>();
      serviceMock.Setup(m => m.TMDbGetPage(It.IsAny<int>())).Returns(fromjsonfile);
      return serviceMock;
    }

    [Test]
    public void TestCache_PruneCache_RemovesAllElementsThatAreAboveCacheMax()
    {
      //arrange
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/MovieResults1.json"));
      MovieList fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieList>(text);
      var serviceMock = CreateServiceMockForMultipleGetPage(fromjsonfile);
      var organiser = new PageCacheOrganiser(serviceMock.Object);
      //act
      organiser.SetPageCacheLimits(2, 4, 5);
      MovieListCache cachedData1 = organiser.CacheGetPage(1);
      MovieListCache cachedData2 = organiser.CacheGetPage(2);
      MovieListCache cachedData3 = organiser.CacheGetPage(3);
      MovieListCache cachedData4 = organiser.CacheGetPage(4);
      MovieListCache cachedData5 = organiser.CacheGetPage(5);
      MovieListCache cachedData6 = organiser.CacheGetPage(6);
      int itemsInCache = organiser.ReturnPageCacheCurrentSize();
      //assert
      itemsInCache.Should().Be(4);
    }

    private static Mock<ITMDbService> CreateServiceMockForMultipleGetPage(MovieList fromjsonfile)
    {
      fromjsonfile = CopyMapFromJsonFile(fromjsonfile, fromjsonfile, 1);
      MovieList fromjsonfile2 = new MovieList();
      fromjsonfile2 = CopyMapFromJsonFile(fromjsonfile, fromjsonfile2, 2);
      MovieList fromjsonfile3 = new MovieList();
      fromjsonfile3 = CopyMapFromJsonFile(fromjsonfile, fromjsonfile3, 3);
      MovieList fromjsonfile4 = new MovieList();
      fromjsonfile4 = CopyMapFromJsonFile(fromjsonfile, fromjsonfile4, 4);
      MovieList fromjsonfile5 = new MovieList();
      fromjsonfile5 = CopyMapFromJsonFile(fromjsonfile, fromjsonfile5, 5);
      MovieList fromjsonfile6 = new MovieList();
      fromjsonfile6 = CopyMapFromJsonFile(fromjsonfile, fromjsonfile6, 6);
      Queue<MovieList> resultque = new Queue<MovieList>(new MovieList[] { fromjsonfile, fromjsonfile2, fromjsonfile3, fromjsonfile4, fromjsonfile5, fromjsonfile6 });
      var serviceMock = new Mock<ITMDbService>();
      serviceMock.Setup(m => m.TMDbGetPage(It.IsAny<int>())).Returns(() => resultque.Dequeue());
      return serviceMock;
    }

    private static MovieList CopyMapFromJsonFile(MovieList fromjsonfile, MovieList fromjsonfilex, int x)
    {
      fromjsonfilex.page = x;
      fromjsonfilex.results = fromjsonfile.results;
      fromjsonfilex.total_results = fromjsonfile.total_results;
      fromjsonfilex.total_pages = fromjsonfile.total_pages;
      return fromjsonfilex;
    }

    [Test]
    public void TestCache_ExistingElementsCacheOrdered_ReturnsTrueWhenOrderedDescPopularity()
    {
      //arrange
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/MovieResults1.json"));
      MovieList fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieList>(text);
      var serviceMock = CreateServiceMockForMultipleGetPage(fromjsonfile);
      var organiser = new PageCacheOrganiser(serviceMock.Object);
      organiser.SetPageCacheLimits(2, 4, 5);

      //act
      MovieListCache cachedData1 = organiser.CacheGetPage(1);
      MovieListCache cachedData2 = organiser.CacheGetPage(2);
      MovieListCache cachedData3 = organiser.CacheGetPage(3);
      MovieListCache cachedData4 = organiser.CacheGetPage(4);
      cachedData2 = organiser.CacheGetPage(2); //artificially providing references to test pruning
      cachedData3 = organiser.CacheGetPage(3);
      MovieListCache cachedData5 = organiser.CacheGetPage(5);
      MovieListCache cachedData6 = organiser.CacheGetPage(6);
      MovieListCache shouldexist = organiser.ReturnCacheEntry(2); //most popular should still exist
      MovieListCache shouldexist2 = organiser.ReturnCacheEntry(3);
      Action entry1 = () => organiser.ReturnCacheEntry(1); //least popular should no longer exist
      Action entry4 = () => organiser.ReturnCacheEntry(4);

      //assert
      entry1.Should().Throw<KeyNotFoundException>();
      entry4.Should().Throw<KeyNotFoundException>();
      shouldexist.Should().NotBeNull();
      shouldexist2.Should().NotBeNull();
    }
  }

[SetUpFixture]
  public class PageCacheOrganiserTestsSetup
  {
    [SetUp]
    public void GlobalSetup()
    {
      var organiser = new PageCacheOrganiser(null);
      organiser.ClearCache();
    }
  }
}