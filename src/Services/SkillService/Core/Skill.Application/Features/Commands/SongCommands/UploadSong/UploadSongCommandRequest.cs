using MediatR;

namespace Skill.Application.Features.Commands.SongCommands.UploadSong
{
    public class UploadSongCommandRequest : IRequest<UploadSongCommandResponse>
    {
        public string Path { get; set; }
        public string Id { get; set; }
    }
}
