using System.Net;
using Domain.Entities;

namespace Infrastructure.TMDbConnection.Commands
{
  public interface ITMDbService
  {
    MovieList TMDbGetPage(int page);
    MovieInfo TMDbGetInfo(int id);
    string DoWebRequest(HttpWebRequest apiRequest);
  }
}