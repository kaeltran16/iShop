using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Core.Entities;
using iShop.Infrastructure.Data;

namespace iShop.Infrastructure.Persistent.Repositories.Contracts
{
    public interface IImagesRepository: IDataRepository<Image>
    {
        Task<IEnumerable<Image>> GetProductImages(Guid productId);
        Task<Image> Get(Guid id);
    }
}
