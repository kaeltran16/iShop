using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Commons.Exceptions.ExceptionMessages
{
    public class ExceptionMessage
    {
        public const string InvalidId = "Error occured. The input Id is invalid!";
        public const string NotFound = "The item is not found!";
        public const string FailedToSave = "Error occured. The item can not be saved to the database";
    }
}
