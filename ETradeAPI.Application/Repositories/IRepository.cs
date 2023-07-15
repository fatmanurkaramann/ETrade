using ETradeAPI.Domain.Entities.Comman;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Application.Repositories
{
    //evrensel olduğu için T entity parametresi verdik(product,order..)
    public interface IRepository<T>
        where T : BaseEntity
    {
        DbSet<T> Table { get;}
    }
}
