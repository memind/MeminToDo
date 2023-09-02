using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Application.Features.Commands.ArtCommands.CreateArt
{
    public class CreateArtCommandResponse
    {
        public GetOneResult<Art> Result { get; set; }
    }
}
