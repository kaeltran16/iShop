using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Commons.Helpers
{
    public static class ApplicationConstants
    {
        public static class Error
        {
            public const string NotFound = "not_found";
            public const string SaveFailed = "save_failed";
            public const string InvalidFormat = "invalid_format";
            public const string NullOrEmpty = "null_or_empty";
            public const string InvalidSize = "invalid_size";
            public const string UnSupportedType = "unsupported_type";
            public const string Unauthorized = "unthorized";
        }
    }
}
