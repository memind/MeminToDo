using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skill.Application.Abstractions.Services;
using Skill.Application.DTOs.SongDTOs;
using Skill.Domain.Entities.Common;
using Skill.Domain.Entities;

namespace Skill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private ISongService _service;

        public SongController(ISongService service)
        {
            _service = service;
        }

        [HttpGet("/getOneSong")]
        public GetOneResult<Song> GetById(string id)
        {
            return _service.GetSongById(id);
        }

        [HttpGet("/getAllSongs")]
        public GetManyResult<Song> GetAll()
        {
            return _service.GetAllSongs();
        }

        [HttpPost]
        public GetOneResult<Song> Create(SongDto dto)
        {
            return _service.CreateSong(dto);
        }

        [HttpPut]
        public GetOneResult<Song> Update(string id, SongDto dto)
        {
            return _service.UpdateSong(id, dto);
        }

        [HttpDelete]
        public void Delete(string id)
        {
            _service.DeleteSong(id);
        }
    }
}
