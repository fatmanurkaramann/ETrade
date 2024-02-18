﻿using ETradeAPI.Application.Abstraction.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get =>_storage.GetType().Name ; }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
           => await _storage.DeleteAsync(pathOrContainerName, fileName);

        public List<string> GetAllAsync(string pathOrContainerName)
            => _storage.GetAllAsync(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
            => _storage.HasFile(pathOrContainerName, fileName);

        public async Task<List<(string fileName, string path)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
         => await _storage.UploadAsync(pathOrContainerName, files);
    }
}
