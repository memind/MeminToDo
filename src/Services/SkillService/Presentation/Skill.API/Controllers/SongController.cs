﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skill.Application.Abstractions.Services;
using Skill.Application.DTOs.SongDTOs;
using Skill.Domain.Entities.Common;
using Skill.Domain.Entities;
using Skill.Application.Features.Queries.SongQueries.GetAllSongs;
using Skill.Application.Features.Queries.SongQueries.GetSongById;
using Skill.Application.Features.Queries.SongQueries.GetUsersAllSongs;
using Skill.Application.Features.Commands.SongCommands.CreateSong;
using Skill.Application.Features.Commands.SongCommands.UpdateSong;
using Skill.Application.Features.Commands.SongCommands.DeleteSong;
using Microsoft.AspNetCore.Authorization;

namespace Skill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SongController : ControllerBase
    {
        private ISongService _service;
        private IMediator _mediator;

        public SongController(ISongService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpGet("/getOneSong")]
        [Authorize(Policy = "SkillRead")]
        public async Task<GetSongByIdQueryResponse> GetById([FromQuery] GetSongByIdQueryRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("/getAllSongs")]
        [Authorize(Policy = "SkillRead")]
        public async Task<GetAllSongsQueryResponse> GetAll([FromQuery] GetAllSongsQueryRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("/getUsersAllSongs")]
        [Authorize(Policy = "SkillRead")]
        public async Task<GetUsersAllSongsQueryResponse> GetUsersAll([FromQuery] GetUsersAllSongsQueryRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        [Authorize(Policy = "SkillWrite")]
        public async Task<CreateSongCommandResponse> Create([FromQuery] CreateSongCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        [Authorize(Policy = "SkillWrite")]
        public async Task<UpdateSongCommandResponse> Update([FromQuery] UpdateSongCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete]
        [Authorize(Policy = "SkillWrite")]
        public async Task<DeleteSongCommandResponse> Delete([FromQuery] DeleteSongCommandRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
