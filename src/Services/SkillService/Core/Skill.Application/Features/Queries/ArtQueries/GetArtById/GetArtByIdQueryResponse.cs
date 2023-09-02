using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Application.Features.Queries.ArtQueries.GetArtById
{
    public class GetArtByIdQueryResponse
    {
        public GetOneResult<Art> Result { get; set; }
    }
}
