using MediatR;

namespace Skill.Application.Features.Queries.ArtQueries.GetArtById
{
    public class GetArtByIdQueryRequest : IRequest<GetArtByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
