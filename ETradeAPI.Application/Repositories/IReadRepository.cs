using ETradeAPI.Domain.Entities.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Application.Repositories
{
    //read reponun Tsi otmatik IReoısitorynin Tsine gidecek
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        //IQuerybale bir liste verir T ise tekildir
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T,bool>> method,bool tracking=true); //şarta uygun olan birden fazla veri elde edilsin
       //şarta uygun olan ilkini getiren metot
        Task<T> GetSingleAsync(Expression<Func<T,bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
