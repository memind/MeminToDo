using Amazon.Runtime.Internal;
using MediatR;

namespace Skill.Application.Features.Queries.ArtQueries.GetAllArts
{
    public class GetAllArtsQueryRequest : IRequest<GetAllArtsQueryResponse>
    {
    }
}
