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
using Amazon.Runtime.Internal;
using Amazon.S3.Model;
using Amazon.S3;
using Skill.Application.Features.Commands.SongCommands.UploadSong;
using Skill.Persistance.Configurations;
using Microsoft.Extensions.Options;

namespace Skill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SongController : ControllerBase
    {
        private IMediator _mediator;
        private IAmazonS3 _s3;
        private readonly SongConfigurations _songConfig;

        public SongController(IMediator mediator, IAmazonS3 s3, IOptions<SongConfigurations> songConfig)
        {
            _mediator = mediator;
            _s3 = s3;
            _songConfig = songConfig.Value;
        }

        [HttpGet("/getOneSong")]
        [Authorize(Policy = "SkillRead")]
        public async Task<GetSongByIdQueryResponse> GetById([FromQuery] GetSongByIdQueryRequest request) => await _mediator.Send(request);

        [HttpGet]
        [Authorize(Policy = "SkillRead")]
        public async Task<GetAllSongsQueryResponse> GetAll([FromQuery] GetAllSongsQueryRequest request) => await _mediator.Send(request);

        [HttpGet("/getUsersAllSongs")]
        [Authorize(Policy = "SkillRead")]
        public async Task<GetUsersAllSongsQueryResponse> GetUsersAll([FromQuery] GetUsersAllSongsQueryRequest request) => await _mediator.Send(request);

        [HttpPost]
        [Authorize(Policy = "SkillWrite")]
        public async Task<CreateSongCommandResponse> Create([FromQuery] CreateSongCommandRequest request) => await _mediator.Send(request);

        [HttpPut]
        [Authorize(Policy = "SkillWrite")]
        public async Task<UpdateSongCommandResponse> Update([FromQuery] UpdateSongCommandRequest request) => await _mediator.Send(request);

        [HttpDelete]
        [Authorize(Policy = "SkillWrite")]
        public async Task<DeleteSongCommandResponse> Delete([FromQuery] DeleteSongCommandRequest request) => await _mediator.Send(request);

        [HttpPost("/upload")]
        public async Task UploadSong([FromQuery] UploadSongCommandRequest request) => await _mediator.Send(request);

        [HttpGet("/download/{fileId}")]
        public async Task<IActionResult> DownloadSong(string fileId)
        {
            GetObjectResponse response = await _s3.GetObjectAsync(_songConfig.BucketName, fileId);
            return File(response.ResponseStream, response.Headers.ContentType);
        }
    }
}
