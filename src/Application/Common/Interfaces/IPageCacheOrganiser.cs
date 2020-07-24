using Domain.Entities;

namespace Application.Common.Interfaces
{
  public interface IPageCacheOrganiser
  {
    string CacheGetSerializedPage(int page);
    MovieListCache CacheGetPage(int page);
    MovieListCache MapToListCache(MovieList parser);
    void CacheAddPage(MovieListCache movieListCached);
    void PrunePageCache();
    void PrunePageTimer();
    void SetPageCacheLimits(int min, int max, int minutes);
    MovieListCache ReturnCacheEntry(int page);
    int ReturnPageCacheCurrentSize();
    void ClearCache();
  }
}