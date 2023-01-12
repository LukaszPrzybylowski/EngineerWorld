using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace EngineerWorld.Services
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        public Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}