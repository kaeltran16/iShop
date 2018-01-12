using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Commons.Helpers;
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
                new ErrorMessage { Code = 500, Message = "Error occured, please check your input ID" }
                    .ToString());
        }

        public IActionResult UnAuthorized()
        {
            return StatusCode(401,
                new ErrorMessage { Code = 500, Message = "Unauthorized attempt!!" }
                    .ToString());
        }

        public IActionResult NullOrEmpty(string itemName)
        {
            return BadRequest(new ErrorMessage {Code = 400, Message = "Error occured, please check your " + itemName}
                .ToString());
        }

        public IActionResult Oversized(string itemName, int maxSize)
        {
            return BadRequest(new ErrorMessage
                {
                    Code = 400,
                    Message = "Error occured, " + itemName + " has maxmimum size of " + maxSize
                }
                .ToString());
        }

        public IActionResult UnSupportedType(string itemName, string[] supportedType)
        {
            return BadRequest(new ErrorMessage
                {
                    Code = 400,
                    Message = itemName + " has invalid type. The support types are " + supportedType
                }
                .ToString());
        }



    }
}
