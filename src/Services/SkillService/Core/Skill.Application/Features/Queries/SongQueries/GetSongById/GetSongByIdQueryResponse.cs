using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Application.Features.Queries.SongQueries.GetSongById
{
    public class GetSongByIdQueryResponse
    {
        public GetOneResult<Song> Result { get; set; }
    }
}
