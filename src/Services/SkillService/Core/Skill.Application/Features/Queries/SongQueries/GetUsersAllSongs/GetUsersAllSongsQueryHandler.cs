using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Queries.SongQueries.GetUsersAllSongs
{
    public class GetUsersAllSongsQueryHandler : IRequestHandler<GetUsersAllSongsQueryRequest, GetUsersAllSongsQueryResponse>
    {
        private ISongService _service;

        public GetUsersAllSongsQueryHandler(ISongService service)
        {
            _service = service;
        }

        public async Task<GetUsersAllSongsQueryResponse> Handle(GetUsersAllSongsQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetUsersAllSongsAsync(Guid.Parse(request.Id));
            return new()
            {
                Result = result
            };
        }
    }
}
