using ETradeAPI.Application.Repositories;
using ETradeAPI.Application.Repositories.File;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = ETradeAPI.Domain.Entities.File;

namespace ETradeAPI.Persistance.Repositories
{
    public class FileReadRepository : ReadRepository<File>, IFileReadRepository
    {
        public FileReadRepository(ETradeAPIDbContext dbContext) : base(dbContext)
        {
        }
    }
}
