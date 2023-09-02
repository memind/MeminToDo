using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Commands.SongCommands.CreateSong
{
    public class CreateSongCommandHandler : IRequestHandler<CreateSongCommandRequest, CreateSongCommandResponse>
    {
        private ISongService _service;

        public CreateSongCommandHandler(ISongService service)
        {
            _service = service;
        }

        public async Task<CreateSongCommandResponse> Handle(CreateSongCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.CreateSongAsync(request.Model, request.Model.UserId.ToString());
            return new()
            {
                Result = result
            };
        }
    }
}
