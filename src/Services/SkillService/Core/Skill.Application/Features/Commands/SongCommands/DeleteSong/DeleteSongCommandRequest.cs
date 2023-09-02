using MediatR;

namespace Skill.Application.Features.Commands.SongCommands.DeleteSong
{
    public class DeleteSongCommandRequest : IRequest<DeleteSongCommandResponse>
    {
        public string Id { get; set; }
    }
}
