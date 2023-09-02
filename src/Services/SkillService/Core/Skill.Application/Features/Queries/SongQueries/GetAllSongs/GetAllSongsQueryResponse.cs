using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Application.Features.Queries.SongQueries.GetAllSongs
{
    public class GetAllSongsQueryResponse
    {
        public GetManyResult<Song> Result { get; set; }
    }
}
