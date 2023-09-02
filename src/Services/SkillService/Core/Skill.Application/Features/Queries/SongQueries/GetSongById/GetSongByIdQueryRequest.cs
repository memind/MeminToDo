
using MediatR;

namespace Skill.Application.Features.Queries.SongQueries.GetSongById
{
    public class GetSongByIdQueryRequest : IRequest<GetSongByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
