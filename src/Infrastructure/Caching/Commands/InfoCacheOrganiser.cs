using System.Collections.Generic;
using System;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.TMDbConnection.Commands;
using System.Linq;

namespace Infrastructure.Caching.Commands
{
  public class InfoCacheOrganiser : IInfoCacheOrganiser
  {
    static Dictionary<int, MovieInfoCache> _cache = new Dictionary<int, MovieInfoCache>();
    private readonly ITMDbService _infoService;
    private int PruneInfoMaxLimit { get; set; }
    private int PruneInfoMinLimit { get; set; }
    private int PruneInfoTimeMins { get; set; }

    public InfoCacheOrganiser(ITMDbService infoService)
    {
      _infoService = infoService;
      PruneInfoMaxLimit = 10;
      PruneInfoMinLimit = 4;
      PruneInfoTimeMins = 15;
    }

    public string CacheGetSerializedInfo(int id)
    {
      MovieInfoCache reqInfoObj = CacheGetInfo(id);
      string reqInfoString = Newtonsoft.Json.JsonConvert.SerializeObject(reqInfoObj.movieInfo);
      return reqInfoString;
    }

    public MovieInfoCache CacheGetInfo(int id)
    {
      if (_cache.ContainsKey(id))
      {
        _cache[id].references++;
        return _cache[id];
      }
      else
      {
        MovieInfo parser = _infoService.TMDbGetInfo(id); 
        MovieInfoCache movieInfoCached = MapToInfoCache(parser);
        CacheAddInfo(movieInfoCached);
        return movieInfoCached;
      }
    }

    public MovieInfoCache MapToInfoCache(MovieInfo parser)  //TODO REWRITE
    {
      MovieInfoCache parsedTo = new MovieInfoCache();
      parsedTo.references = 1;
      parsedTo.movieInfo = parser;
      return parsedTo;
    }

    public void CacheAddInfo(MovieInfoCache movieInfoCached)
    {
      if (_cache.Count >= PruneInfoMaxLimit)
      {
        PruneInfoCache();
      }
      _cache.Add(movieInfoCached.movieInfo.id, movieInfoCached);
    }

    public void PruneInfoCache()
    {
      var items = _cache.OrderByDescending(i => i.Value.references).Skip(PruneInfoMinLimit);
      foreach (var item in items)
      {
        _cache.Remove(item.Key);
      }
    }

    public void PruneInfoTimer()
    {
      //TODO
    }

    public void SetInfoCacheLimits(int min, int max, int minutes)
    {
      if (min > 0 && max > min)
      {
        PruneInfoMinLimit = min;
        PruneInfoMaxLimit = max;
      }

      if(minutes > 1)
        PruneInfoTimeMins = minutes;
    }

    public MovieInfoCache ReturnInfoCacheEntry(int id)
    {
      return _cache[id];
    }

    public int ReturnInfoCacheCurrentSize()
    {
      return _cache.Count();
    }

    public void ClearCache()
    {
      _cache.Clear();
    }


    public string GetCache()
    {

      string cache = Newtonsoft.Json.JsonConvert.SerializeObject(_cache);
      return cache;
    } 
  }
}