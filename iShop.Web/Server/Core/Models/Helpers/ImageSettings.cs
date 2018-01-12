using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models.Helpers
{
    public class ImageSettings
    {
        public int MaxByte { get; set; }
        public string[] AcceptedTypes { get; set; }

        public bool IsSupported(string fileName)
        {
            return AcceptedTypes.All(s => s != Path.GetExtension(fileName).ToLower());

        }
    }
}
