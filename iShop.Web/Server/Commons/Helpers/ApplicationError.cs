using Newtonsoft.Json;

namespace iShop.Web.Server.Commons.Helpers
{
    public class ApplicationError
    {
        public string Error { get; set; }
        public string ErrorDescription { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
