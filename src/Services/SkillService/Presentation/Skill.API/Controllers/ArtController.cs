using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skill.Application.Abstractions.Services;
using Skill.Application.DTOs.ArtDTOs;
using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtController : ControllerBase
    {
        private IArtService _service;

        public ArtController(IArtService service)
        {
            _service = service;
        }

        [HttpGet("/getOneArt")]
        public GetOneResult<Art> GetById(string id)
        {
            return _service.GetArtById(id);
        }

        [HttpGet("/getAllArts")]
        public GetManyResult<Art> GetAll()
        {
            return _service.GetAllArts();
        }

        [HttpPost]
        public GetOneResult<Art> Create(ArtDto dto)
        {
            return _service.CreateArt(dto);
        }

        [HttpPut]
        public GetOneResult<Art> Update(string id, ArtDto dto)
        {
            return _service.UpdateArt(id, dto);
        }

        [HttpDelete]
        public void Delete(string id)
        {
            _service.DeleteArt(id);
        }
    }
}
