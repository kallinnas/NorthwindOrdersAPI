namespace NorthwindOrdersAPI.Services.Interfaces
{
    public interface IUploadFileService
    {
        Task<bool> UploadFileAsync(IFormFile file, int id);
    }
}
