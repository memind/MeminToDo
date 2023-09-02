using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Application.Features.Commands.SongCommands.UpdateSong
{
    public class UpdateSongCommandResponse
    {
        public GetOneResult<Song> Result { get; set; }
    }
}
