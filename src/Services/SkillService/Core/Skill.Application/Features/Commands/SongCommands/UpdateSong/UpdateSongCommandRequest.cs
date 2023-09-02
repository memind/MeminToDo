using MediatR;
using Skill.Application.DTOs.SongDTOs;

namespace Skill.Application.Features.Commands.SongCommands.UpdateSong
{
    public class UpdateSongCommandRequest : IRequest<UpdateSongCommandResponse>
    {
        public string Id { get; set; }
        public SongDto Model { get; set; }
    }
}
