using System.Collections.Generic;
using Domain.Entities;

namespace Application.Common.Interfaces
{
  public interface IInfoCacheOrganiser
  {
    string CacheGetSerializedInfo(int id);
    MovieInfoCache CacheGetInfo(int id);
    MovieInfoCache MapToInfoCache(MovieInfo parser);
    void CacheAddInfo(MovieInfoCache movieInfoCached);
    void PruneInfoCache();
    void PruneInfoTimer();
    void SetInfoCacheLimits(int min, int max, int minutes);
    MovieInfoCache ReturnInfoCacheEntry(int id);
    int ReturnInfoCacheCurrentSize();
    void ClearCache();
    string GetCache();
  }

}