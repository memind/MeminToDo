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

        public ShowController(IShowService service)
        {
            _service = service;
        }

        [HttpGet("/getShow")]
        public ShowDto GetShow(string id) 
        {
            return _service.GetShowById(id);
        }

        [HttpGet("/getShows")]
        public List<ShowDto> GetAllShows()
        {
            return _service.GetAllShows();
        }

        [HttpGet("/getUsersShows")]
        public List<ShowDto> GetUsersAllShows(string id)
        {
            return _service.GetUsersAllShows(id);
        }

        [HttpPost("/createShow")]
        public int CreateShow(ShowDto model)
        {
            return _service.CreateShow(model);
        }

        [HttpPut("/updateShow")]
        public int UpdateShow(ShowDto model)
        {
            return _service.UpdateShow(model);
        }

        [HttpDelete("/deleteShow")]
        public int DeleteShow(string id)
        {
            return _service.DeleteShow(id);
        }
    }
}
