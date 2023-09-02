using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Application.Features.Queries.ArtQueries.GetUsersAllArts
{
    public class GetUsersAllArtsQueryResponse
    {
        public GetManyResult<Art> Result { get; set; }
    }
}