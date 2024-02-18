using ETradeAPI.Application.Abstraction.Storage.Local;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistance.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace ETradeAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ETradeAPIDbContext _dbContext;

        public LocalStorage(IWebHostEnvironment webHostEnvironment, ETradeAPIDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
           => System.IO.File.Delete($"{pathOrContainerName}\\{fileName}");

        public List<string> GetAllAsync(string pathOrContainerName)
        {
           DirectoryInfo directory = new DirectoryInfo(pathOrContainerName);
           return directory.GetFiles().Select(f=>f.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
           => System.IO.File.Exists($"{pathOrContainerName}\\{fileName}");
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
                using (var memoryStream = new MemoryStream())
                {
                    bool result = await CopyFile($"{uploadPath}\\{file.Name}", file);
                    datas.Add((file.Name, $"{uploadPath}\\{file.Name}"));
                    await file.CopyToAsync(memoryStream);
                    var uploadedFile = new ETradeAPI.Domain.Entities.File
                    {
                        FileName = file.Name,
                        Path = Convert.ToBase64String(memoryStream.ToArray())

                    };
                    _dbContext.Files.Add(uploadedFile);
                    await _dbContext.SaveChangesAsync();
                }
            }
            return null;
        }
    }
    
}
