using Microsoft.AspNetCore.Http;

namespace ETradeAPI.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName,string path)>> UploadAsync(string path,IFormFileCollection files);
        Task<string> FileRenameAsync(string fileName);  
        Task<bool> CopyFile(string path,IFormFile file);
    }
}
