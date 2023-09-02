using AutoMapper;
using MongoDB.Bson;
using Skill.Application.Abstractions.Services;
using Skill.Application.DTOs.ArtDTOs;
using Skill.Application.Repositories.ArtRepositories;
using Skill.Domain.Entities;
using Skill.Domain.Entities.Common;

namespace Skill.Persistance.Concretes.Services
{
    public class ArtService : IArtService
    {
        private readonly IArtReadRepository _read;
        private readonly IArtWriteRepository _write;
        private readonly IMapper _mapper;

        public ArtService(IArtWriteRepository write, IArtReadRepository read, IMapper mapper)
        {
            _write = write;
            _read = read;
            _mapper = mapper;
        }

        public GetOneResult<Art> CreateArt(ArtDto newArt, string id)
        {
            newArt.Id = ObjectId.GenerateNewId();
            newArt.UserId = Guid.Parse(id);
            var map = _mapper.Map<Art>(newArt);
            var result = _write.InsertOne(map);

            return result;
        }

        public async Task<GetOneResult<Art>> CreateArtAsync(ArtDto newArt, string id)
        {
            newArt.Id = ObjectId.GenerateNewId();
            newArt.UserId = Guid.Parse(id);
            var map = _mapper.Map<Art>(newArt);
            var result = await _write.InsertOneAsync(map);

            return result;
        }

        public void DeleteArt(string id)
        {
            _write.DeleteById(id);
        }

        public async Task DeleteArtAsync(string id)
        {
            await _write.DeleteByIdAsync(id);
        }

        public GetManyResult<Art> GetAllArts()
        {
            return _read.GetAll();
        }

        public async Task<GetManyResult<Art>> GetAllArtsAsync()
        {
            return await _read.GetAllAsync();
        }

        public GetManyResult<Art> GetUsersAllArts(Guid id)
        {
            return _read.GetFiltered(x => x.UserId == id);
        }

        public async Task<GetManyResult<Art>> GetAllUsersArtsAsync(Guid id)
        {
            return await _read.GetFilteredAsync(x => x.UserId == id);
        }

        public GetOneResult<Art> GetArtById(string id)
        {
            return _read.GetById(id);
        }

        public async Task<GetOneResult<Art>> GetArtByIdAsync(string id)
        {
            return await _read.GetByIdAsync(id);
        }

        public GetOneResult<Art> UpdateArt(string id, ArtDto dto)
        {
            var map = _mapper.Map<Art>(dto);
            map.Id = ObjectId.Parse(id);
            var result = _write.ReplaceOne(map, id);
            return result;
        }

        public async Task<GetOneResult<Art>> UpdateArtAsync(string id, ArtDto dto)
        {
            var map = _mapper.Map<Art>(dto);
            map.Id = ObjectId.Parse(id);
            var result = await _write.ReplaceOneAsync(map, id);
            return result;
        }
    }
}
