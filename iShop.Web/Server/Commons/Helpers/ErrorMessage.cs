using Newtonsoft.Json;

namespace iShop.Web.Server.Commons.Helpers
{
    public class ErrorMessage
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
