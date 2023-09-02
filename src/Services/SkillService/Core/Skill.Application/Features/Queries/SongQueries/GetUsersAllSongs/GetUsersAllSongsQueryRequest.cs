using MediatR;

namespace Skill.Application.Features.Queries.SongQueries.GetUsersAllSongs
{
    public class GetUsersAllSongsQueryRequest : IRequest<GetUsersAllSongsQueryResponse>
    {
        public string Id { get; set; }
    }
}
