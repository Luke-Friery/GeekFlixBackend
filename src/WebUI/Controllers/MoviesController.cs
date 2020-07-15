using Application.MovieList.Commands.AddMovie;
using Application.MovieList.Commands.RemoveMovie;
using CleanArchiTemplate.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
  public class MoviesController : ApiController
  {
    [HttpPost]
    public async Task<ActionResult<int>> Create(AddMovieCommand command)
    {
      return await Mediator.Send(command);
    }

    //     [HttpPut("{id}")]
    //     public async Task<ActionResult> Update(int id, EditMovieCommand command)
    //     {
    //       if (id != command.Id)
    //       {
    //         return BadRequest();
    //       }

    //       await Mediator.Send(command);

    //       return NoContent();
    //     }

    //     [HttpPut("[action]")]
    //     public async Task<ActionResult> UpdateItemDetails(int id, UpdateTodoItemDetailCommand command)
    //     {
    //       if (id != command.Id)
    //       {
    //         return BadRequest();
    //       }

    //       await Mediator.Send(command);

    //       return NoContent();
    //     }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      await Mediator.Send(new RemoveMovieCommand { Id = id });

      return NoContent();
    }
  }
}