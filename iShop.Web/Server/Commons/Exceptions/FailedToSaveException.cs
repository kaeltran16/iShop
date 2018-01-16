using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Commons.Exceptions.ExceptionMessages;

namespace iShop.Web.Server.Commons.Exceptions
{
    public class FailedToSaveException: ApplicationException
    {
        public FailedToSaveException(string message=ExceptionMessage.FailedToSave)
            : base(message, null)
        {
        }

        public FailedToSaveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
