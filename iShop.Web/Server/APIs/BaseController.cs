using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.Server.APIs
{
    public class BaseController: Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult NotFound(string itemName, Guid itemId)
        {
            return NotFound(
                new ErrorMessage { Code = 404, Message = itemName + " with id " + itemId + " not existed" }.ToString());
        }

        public IActionResult FailedToSave(string itemName, Guid itemId)
        {
            return StatusCode(500,
                new ErrorMessage { Code = 500, Message = itemName + " with id " + itemId + " failed to saved" }
                    .ToString());
        }

        public IActionResult InvalidId(string itemId)
        {
            return StatusCode(500,
                new ErrorMessage { Code = 500, Message = "Error accured, please check your input ID" }
                    .ToString());
        }

        public IActionResult UnAuthorized()
        {
            return StatusCode(401,
                new ErrorMessage { Code = 500, Message = "Unauthorized attempt!!" }
                    .ToString());
        }



    }
}
