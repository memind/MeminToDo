using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Application.Features.Commands.SongCommands.CreateSong
{
    public class CreateSongCommandResponse
    {
        public GetOneResult<Song> Result { get; set; }
    }
}
