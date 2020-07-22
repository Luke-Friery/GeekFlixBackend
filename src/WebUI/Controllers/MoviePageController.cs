using System.Threading.Tasks;
using Application.Common.Interfaces;
using CleanArchiTemplate.WebUI.Controllers;
using Infrastructure.Caching.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
  public class MoviePageController : ApiController
  {
    private readonly IPageCacheOrganiser _cache;
    public MoviePageController(IPageCacheOrganiser cache)
    {
      _cache = cache;
    }

    [HttpGet("{pageId}")]
    public string PassThroughMoviePage(int pageId)
    {
      int i = 1;
      i = i++;
      return _cache.CacheGetSerializedPage(pageId);
    }

    [HttpPost("SetMinMax&Time/{min}/{max}/{mins}")]
    public IActionResult SetCacheLimits(int min, int max, int mins)
    {
      _cache.SetPageCacheLimits(min, max, mins);
      return Ok();
    }



  }
}