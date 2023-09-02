using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Commands.SongCommands.UpdateSong
{
    public class UpdateSongCommandHandler : IRequestHandler<UpdateSongCommandRequest, UpdateSongCommandResponse>
    {
        private ISongService _service;

        public UpdateSongCommandHandler(ISongService service)
        {
            _service = service;
        }

        public async Task<UpdateSongCommandResponse> Handle(UpdateSongCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateSongAsync(request.Id, request.Model);
            return new()
            {
                Result = result
            };
        }
    }
}
