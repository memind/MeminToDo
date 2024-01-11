using Autofac.Core;
using Common.Messaging.RabbitMQ.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
using Skill.Persistance.Consts;
using System.ServiceModel.Channels;

namespace Skill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArtController : ControllerBase
    {
        private IMediator _mediator;
        private IFileService _fileService;
        private readonly IMessageConsumerService _message;

        public ArtController(IMediator mediator, IFileService fileService, IMessageConsumerService message)
        {
            _mediator = mediator;
            _fileService = fileService;
            _message = message;

            _message.PublishConnectedInfo(MessageConsts.ArtServiceName());
        }

        [HttpGet("/getOneArt")]
        [Authorize(Policy = "SkillRead")]
        public async Task<GetArtByIdQueryResponse> GetById([FromQuery] GetArtByIdQueryRequest request) => await _mediator.Send(request);

        [HttpGet]
        [Authorize(Policy = "SkillRead")]
        public async Task<GetAllArtsQueryResponse> GetAll([FromQuery] GetAllArtsQueryRequest request) => await _mediator.Send(request);

        [HttpGet("/getUsersAllArts")]
        [Authorize(Policy = "SkillRead")]
        public async Task<GetUsersAllArtsQueryResponse> GetUsersAll([FromQuery] GetUsersAllArtsQueryRequest request) => await _mediator.Send(request);

        [HttpPost]
        [Authorize(Policy = "SkillWrite")]
        public async Task<CreateArtCommandResponse> Create([FromQuery] CreateArtCommandRequest request) => await _mediator.Send(request);

        [HttpPut]
        [Authorize(Policy = "SkillWrite")]
        public async Task<UpdateArtCommandResponse> Update([FromQuery] UpdateArtCommandRequest request) => await _mediator.Send(request);

        [HttpDelete]
        [Authorize(Policy = "SkillWrite")]
        public async Task<DeleteArtCommandResponse> Delete([FromQuery] DeleteArtCommandRequest request) => await _mediator.Send(request);

        [HttpGet("/consumeBackup")]
        public void ConsumeBackUpInfo() => _message.ConsumeBackUpInfo();

        [HttpGet("/consumeTest")]
        public void ConsumeTestInfo() => _message.ConsumeStartTest();

        [HttpPost("/file/{fileId}")]
        [Authorize(Policy = "SkillWrite")]
        public async Task<BlobResponseDto?> File(IFormFile file, [FromQuery] string fileId) => await _fileService.UploadAsync(file, fileId);
        

        [HttpGet("/file/{fileId}")]
        [Authorize(Policy = "SkillRead")]
        public async Task<IActionResult> File(string fileId)
        {
            var result = await _fileService.DownloadAsync(fileId);
            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
