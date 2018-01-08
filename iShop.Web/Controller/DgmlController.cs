using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Persistent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iShop.Web.Controller
{
 
        [Route("Dgml")]
        public class DgmlController : Microsoft.AspNetCore.Mvc.Controller
        {
            public ApplicationDbContext _context { get; }


            public DgmlController(ApplicationDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Creates a DGML class diagram of most of the entities in the project wher you go to localhost/dgml
            /// See https://github.com/ErikEJ/SqlCeToolbox/wiki/EF-Core-Power-Tools
            /// </summary>
            /// <returns>a DGML class diagram</returns>
            [HttpGet]
            public IActionResult Get()
            {

                System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "\\Entities.dgml",
                    _context.AsDgml(),
                    System.Text.Encoding.UTF8);

                var file = System.IO.File.OpenRead(Directory.GetCurrentDirectory() + "\\Entities.dgml");
                var response = File(file, "application/octet-stream", "Entities.dgml");
                return response;
            }
        }
    }

