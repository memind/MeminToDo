using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Application.Features.Queries.SongQueries.GetUsersAllSongs
{
    public class GetUsersAllSongsQueryResponse
    {
        public GetManyResult<Song> Result { get; set; }
    }
}
