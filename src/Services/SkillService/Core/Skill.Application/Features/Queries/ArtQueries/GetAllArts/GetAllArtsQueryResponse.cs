using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Application.Features.Queries.ArtQueries.GetAllArts
{
    public class GetAllArtsQueryResponse
    {
        public GetManyResult<Art> Result { get; set; }
    }
}
