using System.Collections.Generic;
using System;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.TMDbConnection.Commands;
using System.Linq;

namespace Infrastructure.Caching.Commands
{
  public class PageCacheOrganiser
  {
    Dictionary<int, MovieListCache> _cache = new Dictionary<int, MovieListCache>();
    private readonly ITMDbListService _listService;

    private int PruneMaxLimit { get; set; }
    private int PruneMinLimit { get; set; }
    private int PruneTimeMins { get; set; }

    public PageCacheOrganiser(ITMDbListService listService, int max = 50, int min = 10, int time = 15)
    {
      _listService = listService;
      PruneMaxLimit = max;
      PruneMinLimit = min;
      PruneTimeMins = time;
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
        MovieList parser = _listService.TMDbGetPage(page);
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
      if (_cache.Count >= PruneMaxLimit)
      {
        PrunePageCache();
      }
      _cache.Add(movieListCached.page, movieListCached);
    }

    public void PrunePageCache()
    {
      if (PruneMinLimit > 0 && PruneMinLimit < PruneMaxLimit)
      {
        var items = _cache.OrderByDescending(i => i.Value.references);
        int i = 0;
        foreach (var item in items)
        {
          if (i > PruneMinLimit)
          {
            _cache.Remove(item.Key);
          }
          i++;
        }
      }
    }

    public void PrunePageTimer()
    {

    }

    public void SetPageCacheLimits()
    {

    }

    public MovieListCache ReturnCacheEntry(int page)
    {
      return _cache[page];
    }
  }
}