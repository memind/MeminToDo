﻿using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.GameDTOs;
using Entertainment.Application.DTOs.ShowDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entertainment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService _service;

        public GameController(IGameService service)
        {
            _service = service;
        }

        [HttpGet("/getGame")]
        [Authorize(Policy = "EntertainmentRead")]
        public GameDto GetGame(string id) => _service.GetGameById(id);

        [HttpGet("/getGames")]
        [Authorize(Policy = "EntertainmentRead")]
        public List<GameDto> GetAllGames() => _service.GetAllGames();


        [HttpGet("/getUsersGames")]
        [Authorize(Policy = "EntertainmentRead")]
        public List<GameDto> GetUsersAllGames(string id) => _service.GetUsersAllGames(id);

        [HttpPost("/createGame")]
        [Authorize(Policy = "EntertainmentWrite")]
        public int CreateGame(GameDto model) => _service.CreateGame(model);

        [HttpPut("/updateGame")]
        [Authorize(Policy = "EntertainmentWrite")]
        public int UpdateGame(GameDto model) => _service.UpdateGame(model);

        [HttpDelete("/deleteGame")]
        [Authorize(Policy = "EntertainmentWrite")]
        public int DeleteGame(string id) => _service.DeleteGame(id);
    }
}
