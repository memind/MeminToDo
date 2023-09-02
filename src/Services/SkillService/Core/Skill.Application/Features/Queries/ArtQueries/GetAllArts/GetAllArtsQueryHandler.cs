using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Queries.ArtQueries.GetAllArts
{
    public class GetAllArtsQueryHandler : IRequestHandler<GetAllArtsQueryRequest, GetAllArtsQueryResponse>
    {
        readonly IArtService _service;

        public GetAllArtsQueryHandler(IArtService artService)
        {
            _service = artService;
        }

        public async Task<GetAllArtsQueryResponse> Handle(GetAllArtsQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllArtsAsync();
            return new()
            {
                Result = result
            };
        }
    }
}
