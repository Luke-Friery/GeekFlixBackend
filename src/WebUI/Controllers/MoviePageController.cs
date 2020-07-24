using System.Threading.Tasks;
using Application.Common.Interfaces;
using CleanArchiTemplate.WebUI.Controllers;
using Domain.Entities;
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

    [HttpGet("string/{pageId}")]
    public string PassThroughSerializedMoviePage(int pageId)
    {
      return _cache.CacheGetSerializedPage(pageId);
    }

    [HttpGet("{pageId}")]
    public MovieListCache PassThroughMoviePage(int pageId)
    {
      return _cache.CacheGetPage(pageId);
    }

    [HttpPost("SetMinMax&Time/{min}/{max}/{mins}")]
    public IActionResult SetCacheLimits(int min, int max, int mins)
    {
      _cache.SetPageCacheLimits(min, max, mins);
      return Ok();
    }



  }
}