using System.IO;
using System.Linq;

namespace iShop.Web.Server.Commons.Helpers
{
    public class ImageSettings
    {
        public int MaxByte { get; set; }
        public string[] AcceptedTypes { get; set; }

        public bool IsSupported(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return AcceptedTypes.Any(s => s == extension.ToLower());
        }
    }
}
