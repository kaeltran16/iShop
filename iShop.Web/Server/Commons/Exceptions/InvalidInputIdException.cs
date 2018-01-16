using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Commons.Exceptions.ExceptionMessages;

namespace iShop.Web.Server.Commons.Exceptions
{
    public class InvalidInputIdException : ApplicationException
    {
        public InvalidInputIdException(string message = ExceptionMessage.InvalidId)
            : base(message, null)
        {
        }

        public InvalidInputIdException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
