
using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Queries.ArtQueries.GetUsersAllArts
{
    public class GetUsersAllArtsQueryHandler : IRequestHandler<GetUsersAllArtsQueryRequest, GetUsersAllArtsQueryResponse>
    {
        private IArtService _service;

        public GetUsersAllArtsQueryHandler(IArtService service)
        {
            _service = service;
        }

        public async Task<GetUsersAllArtsQueryResponse> Handle(GetUsersAllArtsQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _service.GetAllUsersArtsAsync(Guid.Parse(request.Id));
            return new()
            {
                Result = response
            };
        }
    }
}
