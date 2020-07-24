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
  public class InfoCacheOrganiserTests
  {
    [Test]
    public void TestInfoCache_MappingMovieInfoToCache_ReturnsAMovieInfoCacheElement()
    {
      //arrange
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/InfoResult1.json"));
      MovieInfo fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieInfo>(text);
      var organiser = new InfoCacheOrganiser(null);
      //act
      MovieInfoCache cachedFormat = organiser.MapToInfoCache(fromjsonfile);
      //assert
      cachedFormat.movieInfo.id.Should().Be(1);
      cachedFormat.movieInfo.Should().Be(fromjsonfile);
      cachedFormat.references.Should().Be(1);
    }


    [Test]
    public void TestInfoCache_NewCacheAddElement_TheCacheReturnsTheNewElement()
    {
      //arrange
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/InfoResult1.json"));
      MovieInfo fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieInfo>(text);
      var organiser = new InfoCacheOrganiser(null);
      MovieInfoCache samplejson = organiser.MapToInfoCache(fromjsonfile);
      //act
      organiser.CacheAddInfo(samplejson);
      MovieInfoCache cachedData = organiser.ReturnInfoCacheEntry(1);
      //assert
      cachedData.movieInfo.id.Should().Equals(1);
      cachedData.movieInfo.Should().Be(fromjsonfile);
      cachedData.references.Should().Be(1);
    }


    [Test]
    public void TestCache_NewCacheGetElement_GetsFromMoqThenFromCache()
    {
      //arrange
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/InfoResult1.json"));
      MovieInfo fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieInfo>(text);
      int expectedId = 1;
      var serviceMock = CreateServiceMockForGetInfo(fromjsonfile);
      var organiser = new InfoCacheOrganiser(serviceMock.Object);
      //act 
      MovieInfoCache cachedData = organiser.CacheGetInfo(expectedId); //no data in cache, should get from moq
      cachedData = organiser.CacheGetInfo(expectedId); //data in cache, should get from cache
      //assert
      cachedData.movieInfo.id.Should().Be(1);
      cachedData.movieInfo.Should().Be(fromjsonfile);
      cachedData.references.Should().Be(2);
    }

    private static Mock<ITMDbService> CreateServiceMockForGetInfo(MovieInfo fromjsonfile)
    {
      var serviceMock = new Mock<ITMDbService>();
      serviceMock.Setup(m => m.TMDbGetInfo(It.IsAny<int>())).Returns(fromjsonfile);
      return serviceMock;
    }

    [Test]
    public void TestCache_PruneCache_RemovesAllElementsThatAreAboveCacheMax()
    {
      //arrange
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/InfoResult1.json"));
      MovieInfo fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieInfo>(text);
      var serviceMock = CreateServiceMockForMultipleGetInfo(fromjsonfile);
      var organiser = new InfoCacheOrganiser(serviceMock.Object);
      //act
      organiser.SetInfoCacheLimits(2, 4, 5); // 2 minimum, 4 maximum, 5minutetimer
      MovieInfoCache cachedData1 = organiser.CacheGetInfo(1);
      MovieInfoCache cachedData2 = organiser.CacheGetInfo(2);
      MovieInfoCache cachedData3 = organiser.CacheGetInfo(3);
      MovieInfoCache cachedData4 = organiser.CacheGetInfo(4);
      MovieInfoCache cachedData5 = organiser.CacheGetInfo(5);
      MovieInfoCache cachedData6 = organiser.CacheGetInfo(6);
      int itemsInCache = organiser.ReturnInfoCacheCurrentSize();
      //assert
      itemsInCache.Should().Be(4);
    }

    private static Mock<ITMDbService> CreateServiceMockForMultipleGetInfo(MovieInfo fromjsonfile)
    {
      MovieInfo fromjsonfile2 = new MovieInfo();
      fromjsonfile2 = CopyMapFromJsonFile(fromjsonfile, fromjsonfile2, 2);
      MovieInfo fromjsonfile3 = new MovieInfo();
      fromjsonfile3 = CopyMapFromJsonFile(fromjsonfile, fromjsonfile3, 3);
      MovieInfo fromjsonfile4 = new MovieInfo();
      fromjsonfile4 = CopyMapFromJsonFile(fromjsonfile, fromjsonfile4, 4);
      MovieInfo fromjsonfile5 = new MovieInfo();
      fromjsonfile5 = CopyMapFromJsonFile(fromjsonfile, fromjsonfile5, 5);
      MovieInfo fromjsonfile6 = new MovieInfo();
      fromjsonfile6 = CopyMapFromJsonFile(fromjsonfile, fromjsonfile6, 6);
      Queue<MovieInfo> resultque = new Queue<MovieInfo>(new MovieInfo[] { fromjsonfile, fromjsonfile2, fromjsonfile3, fromjsonfile4, fromjsonfile5, fromjsonfile6 });
      var serviceMock = new Mock<ITMDbService>();
      serviceMock.Setup(m => m.TMDbGetInfo(It.IsAny<int>())).Returns(() => resultque.Dequeue());
      return serviceMock;
    }

    private static MovieInfo CopyMapFromJsonFile(MovieInfo fromjsonfile, MovieInfo fromjsonfilex, int x)
    {
      fromjsonfilex.adult =fromjsonfile.adult;
      fromjsonfilex.backdrop_path = fromjsonfile.backdrop_path;
      fromjsonfilex.belongs_to_collection = fromjsonfile.belongs_to_collection;
      fromjsonfilex.budget = fromjsonfile.budget;
      fromjsonfilex.genres = fromjsonfile.genres;
      fromjsonfilex.homepage = fromjsonfile.homepage;
      fromjsonfilex.id = x;
      fromjsonfilex.imdb_id = fromjsonfile.imdb_id;
      fromjsonfilex.original_language = fromjsonfile.original_language;
      fromjsonfilex.original_title = fromjsonfile.original_title;
      fromjsonfilex.overview = fromjsonfile.overview;
      fromjsonfilex.popularity = fromjsonfile.popularity;
      fromjsonfilex.poster_path = fromjsonfile.poster_path;
      fromjsonfilex.production_companies = fromjsonfile.production_companies;
      fromjsonfilex.production_countries = fromjsonfile.production_countries;
      fromjsonfilex.release_date = fromjsonfile.release_date;
      fromjsonfilex.revenue = fromjsonfile.revenue;
      fromjsonfilex.runtime = fromjsonfile.runtime;
      fromjsonfilex.spoken_languages = fromjsonfile.spoken_languages;
      fromjsonfilex.status = fromjsonfile.status;
      fromjsonfilex.tagline = fromjsonfile.tagline;
      fromjsonfilex.title = fromjsonfile.title;
      fromjsonfilex.video = fromjsonfile.video;
      fromjsonfilex.vote_average = fromjsonfile.vote_average;
      fromjsonfilex.vote_count = fromjsonfile.vote_count;

      return fromjsonfilex;
    }

    [Test]
    public void TestCache_ExistingElementsCacheOrdered_ReturnsTrueWhenOrderedDescPopularity()
    {
      //arrange
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/MovieResults1.json"));
      MovieInfo fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieInfo>(text);
      var serviceMock = CreateServiceMockForMultipleGetInfo(fromjsonfile);
      var organiser = new InfoCacheOrganiser(serviceMock.Object);
      organiser.SetInfoCacheLimits(2, 4, 5);

      //act
      MovieInfoCache cachedData1 = organiser.CacheGetInfo(1);
      MovieInfoCache cachedData2 = organiser.CacheGetInfo(2);
      MovieInfoCache cachedData3 = organiser.CacheGetInfo(3);
      MovieInfoCache cachedData4 = organiser.CacheGetInfo(4);
      cachedData2 = organiser.CacheGetInfo(2); //artificially providing references to test pruning
      cachedData3 = organiser.CacheGetInfo(3);
      MovieInfoCache cachedData5 = organiser.CacheGetInfo(5);
      MovieInfoCache cachedData6 = organiser.CacheGetInfo(6);
      MovieInfoCache shouldexist = organiser.ReturnInfoCacheEntry(2); //most popular should still exist
      MovieInfoCache shouldexist2 = organiser.ReturnInfoCacheEntry(3);
      Action entry1 = () => organiser.ReturnInfoCacheEntry(1); //least popular should no longer exist
      Action entry4 = () => organiser.ReturnInfoCacheEntry(4);

      //assert
      entry1.Should().Throw<KeyNotFoundException>();
      entry4.Should().Throw<KeyNotFoundException>();
      shouldexist.Should().NotBeNull();
      shouldexist2.Should().NotBeNull();
    }
  }

  [SetUpFixture]
  public class InfoCacheOrganiserTestsSetup
  {
    [SetUp]
    public void GlobalSetup()
    {
      var organiser = new PageCacheOrganiser(null);
      organiser.ClearCache();
    }
  }
}