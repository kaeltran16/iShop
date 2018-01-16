using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Commons.Exceptions.ExceptionMessages;

namespace iShop.Web.Server.Commons.Exceptions
{
    public class NotFoundException: ApplicationException
    {
        public NotFoundException(string message = ExceptionMessage.NotFound)
            : base(message, null)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
