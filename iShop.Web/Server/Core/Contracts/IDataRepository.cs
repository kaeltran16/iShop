using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Contracts
{
    public interface IDataRepository { }

    public interface IDataRepository<T> : IDataRepository
        where T : class, new()
    {
        //Task AddAsync(T entity);
        //void Remove(T entity);
    }
}
