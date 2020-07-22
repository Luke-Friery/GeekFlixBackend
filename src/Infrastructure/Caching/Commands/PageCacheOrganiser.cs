using System.Collections.Generic;
using System;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.TMDbConnection.Commands;
using System.Linq;

namespace Infrastructure.Caching.Commands
{
  public class PageCacheOrganiser : IPageCacheOrganiser
  {
    static Dictionary<int, MovieListCache> _cache = new Dictionary<int, MovieListCache>();
    private readonly ITMDbListService _listService;

    private int PrunePageMaxLimit { get; set; }
    private int PrunePageMinLimit { get; set; }
    private int PrunePageTimeMins { get; set; }

    public PageCacheOrganiser(ITMDbListService listService)
    {
      _listService = listService;
      PrunePageMaxLimit = 10;
      PrunePageMinLimit = 4;
      PrunePageTimeMins = 15;
    }

    public string CacheGetSerializedPage(int page)
    {
      MovieListCache reqPageObj = CacheGetPage(page);
      string reqPageString = Newtonsoft.Json.JsonConvert.SerializeObject(reqPageObj);
      return reqPageString;
    }

    public MovieListCache CacheGetPage(int page)
    {
      if (_cache.ContainsKey(page))
      {
        _cache[page].references++;
        return _cache[page];
      }
      else
      {
        MovieList parser = _listService.TMDbGetPage(page); // THIS IS WHERE IT FAILS (First Pass). How do I fix the moq?
        MovieListCache movieListCached = MapToListCache(parser);
        CacheAddPage(movieListCached);
        return movieListCached;
      }
    }

    public MovieListCache MapToListCache(MovieList parser)
    {
      MovieListCache parsedTo = new MovieListCache();
      parsedTo.references = 1;
      parsedTo.page = parser.page;
      parsedTo.total_results = parser.total_results;
      parsedTo.total_pages = parser.total_pages;
      parsedTo.results = parser.results;
      return parsedTo;
    }

    public void CacheAddPage(MovieListCache movieListCached)
    {
      if (_cache.Count >= PrunePageMaxLimit)
      {
        PrunePageCache();
      }
      _cache.Add(movieListCached.page, movieListCached);
    }

    public void PrunePageCache()
    {
      var items = _cache.OrderByDescending(i => i.Value.references).Skip(PrunePageMinLimit);
      foreach (var item in items)
      {
        _cache.Remove(item.Key);
      }
    }

    public void PrunePageTimer()
    {
      //TODO
    }

    public void SetPageCacheLimits(int min, int max, int minutes)
    {
      if (min > 0 && max > min)
      {
        PrunePageMinLimit = min;
        PrunePageMaxLimit = max;
      }

      if(minutes > 1)
        PrunePageTimeMins = minutes;
    }

    public MovieListCache ReturnCacheEntry(int page)
    {
      return _cache[page];
    }

    public int ReturnCacheCurrentSize()
    {
      return _cache.Count();
    }
  }
}