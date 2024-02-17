using ETradeAPI.Application.Abstraction.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETradeAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
           => File.Delete($"{pathOrContainerName}\\{fileName}");

        public List<string> GetAllAsync(string pathOrContainerName)
        {
           DirectoryInfo directory = new DirectoryInfo(pathOrContainerName);
           return directory.GetFiles().Select(f=>f.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
           => File.Exists($"{pathOrContainerName}\\{fileName}");
        private async Task<bool> CopyFile(string path, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None,
                    1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public async Task<List<(string fileName, string path)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainerName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string filename, string path)> datas = new();
            foreach (IFormFile file in files)
            {

                bool result = await CopyFile($"{uploadPath}\\{file.Name}", file);
                datas.Add((file.Name, $"{uploadPath}\\{file.Name}"));
            }
            return null;
        }
    }
}
