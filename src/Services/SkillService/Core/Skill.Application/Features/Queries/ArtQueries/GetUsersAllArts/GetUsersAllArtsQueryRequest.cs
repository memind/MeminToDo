using MediatR;

namespace Skill.Application.Features.Queries.ArtQueries.GetUsersAllArts
{
    public class GetUsersAllArtsQueryRequest : IRequest<GetUsersAllArtsQueryResponse>
    {
        public string Id { get; set; }
    }
}