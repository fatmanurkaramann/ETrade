﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETradeAPI.Infrastructure.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if(!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string filename, string path)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {

               //bool result = await CopyFile($"{uploadPath}\\{fileNewName}", file);
               // datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
               // results.Add(result);
            }
            if(results.TrueForAll(r=>r.Equals(true)))
            {
                return datas;
            }
            return null;
        }
    }
}
