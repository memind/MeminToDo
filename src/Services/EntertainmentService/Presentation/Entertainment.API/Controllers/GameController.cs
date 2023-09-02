using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.GameDTOs;
using Entertainment.Application.DTOs.ShowDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entertainment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _service;

        public GameController(IGameService service)
        {
            _service = service;
        }

        [HttpGet("/getGame")]
        public GameDto GetGame(string id)
        {
            return _service.GetGameById(id);
        }

        [HttpGet("/getGames")]
        public List<GameDto> GetAllGames()
        {
            return _service.GetAllGames();
        }

        [HttpGet("/getUsersGames")]
        public List<GameDto> GetUsersAllGames(string id)
        {
            return _service.GetUsersAllGames(id);
        }

        [HttpPost("/createGame")]
        public int CreateGame(GameDto model)
        {
            return _service.CreateGame(model);
        }

        [HttpPut("/updateGame")]
        public int UpdateGame(GameDto model)
        {
            return _service.UpdateGame(model);
        }

        [HttpDelete("/deleteGame")]
        public int DeleteGame(string id)
        {
            return _service.DeleteGame(id);
        }
    }
}
