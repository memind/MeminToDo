using MediatR;

namespace Skill.Application.Features.Commands.ArtCommands.DeleteArt
{
    public class DeleteArtCommandRequest : IRequest<DeleteArtCommandResponse>
    {
        public string Id { get; set; }
    }
}
