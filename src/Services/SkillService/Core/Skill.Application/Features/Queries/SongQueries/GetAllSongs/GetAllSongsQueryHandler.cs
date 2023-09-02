using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Queries.SongQueries.GetAllSongs
{
    public class GetAllSongsQueryHandler : IRequestHandler<GetAllSongsQueryRequest, GetAllSongsQueryResponse>
    {
        private ISongService _service;

        public GetAllSongsQueryHandler(ISongService service)
        {
            _service = service;
        }

        public async Task<GetAllSongsQueryResponse> Handle(GetAllSongsQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _service.GetAllSongsAsync();
            return new()
            {
                Result = response
            };
        }
    }
}
