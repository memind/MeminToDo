using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Application.Features.Commands.ArtCommands.UpdateArt
{
    public class UpdateArtCommandResponse
    {
        public GetOneResult<Art> Result { get; set; }
    }
}
