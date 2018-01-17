using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Commons;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace iShop.Web.Server.Persistent.Repositories.Commons
{
    public class ImageRepository: DataRepositoryBase<Image>, IImagesRepository
    {
        public ImageRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Image>> GetProductImages(Guid productId)
        {
            return await GetAllAsync(i => i.ProductId == productId);
        }

        public async Task<Image> Get(Guid id)
        {
            return await GetSingleAsync(i => i.Id == id);
        }
    }
}
