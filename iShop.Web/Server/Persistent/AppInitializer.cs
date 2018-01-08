using iShop.Web.Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Persistent
{
    // This class will responsible for seeding data in the database
    public class AppInitializer
    {
        private readonly ApplicationDbContext _context;

        public AppInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {

            // SAMPLE
            if (!_context.Images.Any())
            {
                var images = new List<Image>()
                {
                    new Image() {FileName = "this is test"},
                    new Image() {FileName = "This is TEST"}
                };
                await _context.AddRangeAsync(images);
                await _context.SaveChangesAsync();
            }
           

        }
    }
}
