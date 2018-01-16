using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Contracts;
using iShop.Web.Server.Core.Models;

namespace iShop.Web.Server.Persistent.Repositories.Contracts
{
    public interface IImagesRepository: IDataRepository<Image>
    {
        Task<IEnumerable<Image>> GetProductImages(Guid productId);
        Task<Image> Get(Guid id);
    }
}
