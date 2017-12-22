using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Persistent.UnitOfWork.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
    }

}
