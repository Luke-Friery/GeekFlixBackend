using Domain.Entities;

namespace Application.Common.Interfaces
{
  public interface IPageCacheOrganiser
  {
    MovieListCache CacheGetPage(int page);
    bool CachePageExistsYesNo(int page);
    void CacheAddPage(int id, PageCache pageCache);
    bool CacheRemovePage();
    void PrunePageCache(int size);
    void PrunePageTimer();
    void SetPageCacheLimits();
  }
}