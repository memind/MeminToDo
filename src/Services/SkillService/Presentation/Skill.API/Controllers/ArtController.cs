using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skill.Application.Abstractions.Services;
using Skill.Application.DTOs.ArtDTOs;
using Skill.Application.Features.Commands.ArtCommands.CreateArt;
using Skill.Application.Features.Commands.ArtCommands.DeleteArt;
using Skill.Application.Features.Commands.ArtCommands.UpdateArt;
using Skill.Application.Features.Queries.ArtQueries.GetAllArts;
using Skill.Application.Features.Queries.ArtQueries.GetArtById;
using Skill.Application.Features.Queries.ArtQueries.GetUsersAllArts;
using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtController : ControllerBase
    {
        private IArtService _service;
        private IMediator _mediator;

        public ArtController(IArtService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpGet("/getOneArt")]
        public async Task<GetArtByIdQueryResponse> GetById([FromQuery] GetArtByIdQueryRequest request)
        {
            GetArtByIdQueryResponse response = await _mediator.Send(request);
            return response;
        }

        [HttpGet("/getAllArts")]
        public async Task<GetAllArtsQueryResponse> GetAll([FromQuery] GetAllArtsQueryRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("/getUsersAllArts")]
        public async Task<GetUsersAllArtsQueryResponse> GetUsersAll([FromQuery] GetUsersAllArtsQueryRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<CreateArtCommandResponse> Create([FromQuery] CreateArtCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        public async Task<UpdateArtCommandResponse> Update([FromQuery] UpdateArtCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete]
        public async Task<DeleteArtCommandResponse> Delete([FromQuery] DeleteArtCommandRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
