using MediatR;
using Skill.Application.DTOs.SongDTOs;

namespace Skill.Application.Features.Commands.SongCommands.CreateSong
{
    public class CreateSongCommandRequest : IRequest<CreateSongCommandResponse>
    {
        public SongDto Model { get; set; }
    }
}
