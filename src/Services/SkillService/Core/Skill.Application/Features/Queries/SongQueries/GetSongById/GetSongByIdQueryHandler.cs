using MediatR;
using Skill.Application.Abstractions.Services;

namespace Skill.Application.Features.Queries.SongQueries.GetSongById
{
    public class GetSongByIdQueryHandler : IRequestHandler<GetSongByIdQueryRequest, GetSongByIdQueryResponse>
    {
        private ISongService _service;

        public GetSongByIdQueryHandler(ISongService service)
        {
            _service = service;
        }

        public async Task<GetSongByIdQueryResponse> Handle(GetSongByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetSongByIdAsync(request.Id);
            return new()
            {
                Result = result
            };
        }
    }
}
