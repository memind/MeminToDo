using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Commands.SongCommands.DeleteSong
{
    public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommandRequest, DeleteSongCommandResponse>
    {
        private ISongService _service;

        public DeleteSongCommandHandler(ISongService service)
        {
            _service = service;
        }

        public async Task<DeleteSongCommandResponse> Handle(DeleteSongCommandRequest request, CancellationToken cancellationToken)
        {
            await _service.DeleteSongAsync(request.Id);
            return new();
        }
    }
}
