namespace CatDogLoverManagement.Repository
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}