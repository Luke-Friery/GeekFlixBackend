using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Infrastructure.Caching.Commands;

namespace Application.UnitTests.TMDbApiTests
{
  public class CachingTests
  {
    private readonly PageCacheOrganiser _pageCaching;
    public CachingTests(PageCacheOrganiser pageCaching)
    {
      _pageCaching = pageCaching;
    }



    [Test]
    public void TestCache_NewCacheAddElement_TheCacheHasANewElement()
    {
      //arrange
      var path = System.IO.Directory.GetCurrentDirectory();
      string text = System.IO.File.ReadAllText(Path.Combine(path, @"../../../TMDbApiTests/MovieResults1.json"));
      var fromjsonfile = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieList>(text);

      //act (or is this line still arrange?)
      MovieListCache samplejson = _pageCaching.MapToListCache(fromjsonfile);

      //act 
      _pageCaching.CacheAddPage(samplejson);
      MovieListCache cachedData = _pageCaching.CacheGetPage(1);

      //assert
      cachedData.Should().NotBeNull();
      cachedData.page.Should().Equals(1);
      cachedData.references.Should().Equals(2);
      cachedData.total_results.Should().Equals(fromjsonfile.total_results);
      cachedData.total_pages.Should().Equals(fromjsonfile.total_pages);
      cachedData.results.Should().NotBeNull();
      //cachedData.results.Should().BeOfType(List<Result>)





      /*
      //Nunit Attempt 1:
      MovieListCache pageOneFromDb = _pageCaching.CacheGetPage(1);
      //how do I moq the internal _listService.TMDbGetPage(page) within this^

      pageOneFromDb.Should().NotBeNull();

      MovieListCache pageOneFromCache = _pageCaching.ReturnCacheEntry(1);
      pageOneFromCache.Should().NotBeNull();

      //error; no suitable constructor was found?
      */
    }

    [Test]
    public void TestCache_RemoveElementFromCacheWithOneElement_RemovesElement()
    {

    }

    [Test]
    public void TestCache_PruneCache_RemovesAllElementsThatAreAboveCacheMax()
    {

    }

    [Test]
    public void TestCache_CacheAdd_PruneFirstWhenCacheIsAboveMaxElements()
    {

    }

    [Test]
    public void TestCache_CacheAdd_IncrementElementPopularity()
    {

    }

    [Test]
    public void TestCache_ExistingElementsCacheOrdered_ReturnsTrueWhenOrderedDescPopularity()
    {

    }
  }
}