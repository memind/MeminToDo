using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.ShowDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Entertainment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShowController : ControllerBase
    {
        private readonly IShowService _service;

        public ShowController(IShowService service) => _service = service;
        

        [HttpGet("/{id}")]
        [Authorize(Policy = "EntertainmentRead")]
        public ShowDto GetShow(string id) => _service.GetShowById(id);

        [HttpGet]
        [Authorize(Policy = "EntertainmentRead")]
        public List<ShowDto> GetAllShows() => _service.GetAllShows();

        [HttpGet("/user/{id}")]
        [Authorize(Policy = "EntertainmentRead")]
        public List<ShowDto> GetUsersAllShows(string id) => _service.GetUsersAllShows(id);

        [HttpPost]
        [Authorize(Policy = "EntertainmentWrite")]
        public int CreateShow(ShowDto model) => _service.CreateShow(model);

        [HttpPut]
        [Authorize(Policy = "EntertainmentWrite")]
        public int UpdateShow(ShowDto model) => _service.UpdateShow(model);

        [HttpDelete("/{id}")]
        [Authorize(Policy = "EntertainmentWrite")]
        public int DeleteShow(string id) => _service.DeleteShow(id);

        [HttpGet("/consumeBackup")]
        [Authorize(Policy = "EntertainmentRead")]
        public void ConsumeBackUpInfo() => _service.ConsumeBackUpInfo();

        [HttpGet("/consumeTest")]
        [Authorize(Policy = "EntertainmentRead")]
        public void ConsumeTestInfo() => _service.ConsumeTestInfo();
    }
}
