using MediatR;
using Skill.Application.DTOs.ArtDTOs;

namespace Skill.Application.Features.Commands.ArtCommands.UpdateArt
{
    public class UpdateArtCommandRequest : IRequest<UpdateArtCommandResponse>
    {
        public string Id { get; set; }
        public ArtDto Model { get; set; }
    }
}
