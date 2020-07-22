using System.Net;
using Domain.Entities;

namespace Infrastructure.TMDbConnection.Commands
{
  public interface ITMDbListService
  {
    MovieList TMDbGetPage(int page);
    string DoWebRequest(HttpWebRequest apiRequest);
  }
}