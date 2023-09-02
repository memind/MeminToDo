using Skill.Application.DTOs.ArtDTOs;
using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;
using System.Threading;

namespace Skill.Application.Abstractions.Services
{
    public interface IArtService
    {
        public GetManyResult<Art> GetAllArts();
        public Task<GetManyResult<Art>> GetAllArtsAsync();

        public GetOneResult<Art> GetArtById(string id);
        public Task<GetOneResult<Art>> GetArtByIdAsync(string id);

        public GetManyResult<Art> GetUsersAllArts(Guid id);
        public Task<GetManyResult<Art>> GetAllUsersArtsAsync(Guid id);

        public GetOneResult<Art> CreateArt(ArtDto newArt, string id);
        public Task<GetOneResult<Art>> CreateArtAsync(ArtDto newArt, string id);

        public void DeleteArt(string id);
        public Task DeleteArtAsync(string id);

        public GetOneResult<Art> UpdateArt(string id, ArtDto dto);
        public Task<GetOneResult<Art>> UpdateArtAsync(string id, ArtDto dto);
    }
}
