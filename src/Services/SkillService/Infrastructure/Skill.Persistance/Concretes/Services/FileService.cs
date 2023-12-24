using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Skill.Application.Abstractions.Services;
using Skill.Application.DTOs.ArtDTOs;
using Skill.Persistance.Configurations;

namespace Skill.Persistance.Concretes.Services
{
    public class FileService : IFileService
    {
        private readonly BlobContainerClient _fileContainer;
        private readonly ArtConfigurations _artConfigs;

        public FileService(IOptions<ArtConfigurations> artConfig)
        {
            _artConfigs = artConfig.Value;

            var credential = new StorageSharedKeyCredential(_artConfigs.StorageAccount, _artConfigs.Key);
            var blobServiceClient = new BlobServiceClient(new Uri(_artConfigs.BlobUri), credential);

            _fileContainer = blobServiceClient.GetBlobContainerClient(_artConfigs.BlobContainerName);
        }

        public async Task<BlobResponseDto> UploadAsync(IFormFile blob, string fileId)
        {
            BlobResponseDto response = new();
            BlobClient client = _fileContainer.GetBlobClient(blob.FileName);

            await using (Stream? data = blob.OpenReadStream()) 
            {
                var stream = System.IO.File.Create(fileId);

                await data.CopyToAsync(stream);
                await client.UploadAsync(data);
            }

            response.Status = "Uploaded";
            response.Error = false;
            response.Blob.Uri = client.Uri.AbsoluteUri;
            response.Blob.Name = client.Name;

            return response;
        }

        public async Task<BlobDto?> DownloadAsync(string blobFileName)
        {
            BlobClient file = _fileContainer.GetBlobClient(blobFileName);

            if(await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                Stream blobContent = data;

                var content = await file.DownloadContentAsync();

                string name = blobFileName;
                string contentType = content.Value.Details.ContentType;

                return new()
                {
                    Content = blobContent,
                    Name = name,
                    ContentType = contentType
                };
            }

            return null;
        }
    }
}
