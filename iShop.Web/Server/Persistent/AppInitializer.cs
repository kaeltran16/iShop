using System;
using System.Collections.Generic;
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
            // Sample
            //if (!_context.GenreSet.Any())
            //{
            //    var genres = new List<Genre>()
            //    {  
            //        new Genre() {Name = "Romantic"},
            //        new Genre() {Name = "Advanture"}
            //    };
            //
            //    _context.AddRange(genres);
            //    await _context.SaveChangesAsync();
            //}
        }
    }
}
