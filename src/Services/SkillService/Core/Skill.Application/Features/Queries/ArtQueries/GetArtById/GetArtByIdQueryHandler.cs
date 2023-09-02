using MediatR;
using Skill.Application.Abstractions.Services;
using Skill.Application.Features.Queries.ArtQueries.GetAllArts;
using Skill.Application.Repositories.ArtRepositories;

namespace Skill.Application.Features.Queries.ArtQueries.GetArtById
{
    public class GetArtByIdQueryHandler : IRequestHandler<GetArtByIdQueryRequest, GetArtByIdQueryResponse>
    {
        readonly IArtService _service;

        public GetArtByIdQueryHandler(IArtService service)
        {
            _service = service;
        }

        public async Task<GetArtByIdQueryResponse> Handle(GetArtByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetArtByIdAsync(request.Id);
            return new()
            {
                Result = result
            };
        }

    }
}
