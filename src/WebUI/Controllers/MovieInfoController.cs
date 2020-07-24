using System.Collections.Generic;
using Application.Common.Interfaces;
using CleanArchiTemplate.WebUI.Controllers;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
  public class MovieInfoController : ApiController
  {
    private readonly IInfoCacheOrganiser _cache;
    public MovieInfoController(IInfoCacheOrganiser cache)
    {
      _cache = cache;
    }

    [HttpGet("string/{infoId}")]
    public ActionResult<string> PassThroughStringMovieInfo(int infoId)
    {
      return Ok(_cache.CacheGetSerializedInfo(infoId));
    }

    [HttpGet("{infoId}")]
    public ActionResult<MovieInfo> PassThroughMovieInfo(int infoId)
    {
      MovieInfo data = _cache.CacheGetInfo(infoId).movieInfo;
      return Ok(data);
    }

    [HttpPost("SetInfoCacheMinMax&Time/{min}/{max}/{mins}")]
    public IActionResult SetInfoCacheLimits(int min, int max, int mins)
    {
      _cache.SetInfoCacheLimits(min, max, mins);
      return Ok();
    }

    [HttpGet("ClearCache")]
    public void ClearCache()
    {
      _cache.ClearCache();
    }

    [HttpGet("GetCache")]
    public string GetCache()
    {
      return _cache.GetCache();
    }






  }
}