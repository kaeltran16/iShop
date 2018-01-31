using Newtonsoft.Json;

namespace iShop.Common.Helpers
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
