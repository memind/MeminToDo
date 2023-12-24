using Microsoft.AspNetCore.Http;
using Skill.Application.DTOs.ArtDTOs;

namespace Skill.Application.Abstractions.Services
{
    public interface IFileService
    {
        public Task<BlobResponseDto> UploadAsync(IFormFile blob, string fileId);
        public Task<BlobDto?> DownloadAsync(string blobFileName);

    }
}
