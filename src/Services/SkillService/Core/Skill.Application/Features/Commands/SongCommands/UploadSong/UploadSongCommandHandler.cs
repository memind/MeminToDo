using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Commands.SongCommands.UploadSong
{
    public class UploadSongCommandHandler : IRequestHandler<UploadSongCommandRequest, UploadSongCommandResponse>
    {
        private ISongService _service;

        public UploadSongCommandHandler(ISongService service)
        {
            _service = service;
        }

        public async Task<UploadSongCommandResponse> Handle(UploadSongCommandRequest request, CancellationToken cancellationToken)
        {
            await _service.UploadSongAsync(Guid.Parse(request.Id), request.Path);
            return new()
            {
                Result = true
            };
        }
    }
}
