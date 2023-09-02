using MediatR;
using Skill.Application.DTOs.ArtDTOs;

namespace Skill.Application.Features.Commands.ArtCommands.CreateArt
{
    public class CreateArtCommandRequest : IRequest<CreateArtCommandResponse>
    {
        public ArtDto Model { get; set; }
    }
}
