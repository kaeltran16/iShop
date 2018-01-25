using System;
using iShop.Web.Server.Commons.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.Server.APIs
{
    /// <summary>
    /// The base class for every Controllers. Contains bunch of return results
    /// NOTE: As the application grows, these return messages should be separated into another class/module
    /// </summary>
    public class BaseController : Microsoft.AspNetCore.Mvc.Controller
    {
        protected IActionResult NotFound(Guid itemId)
        {
            return NotFound(
                new ApplicationError
                {
                    Error = ApplicationConstants.Error.NotFound,
                    ErrorDescription = "The item with ID: " + itemId + " is not found."
                }.ToString());
        }

        protected IActionResult FailedToSave(Guid itemId)
        {
            return StatusCode(500,
                new ApplicationError
                {
                    Error = ApplicationConstants.Error.SaveFailed,
                    ErrorDescription = "The item with ID: " + itemId + " failed to save."
                }.ToString());
        }

        protected IActionResult InvalidId(string itemId)
        {
            return StatusCode(500,
                new ApplicationError
                {
                    Error = ApplicationConstants.Error.InvalidFormat,
                    ErrorDescription = "The input ID is in valid format."
                }.ToString());
        }

        protected IActionResult UnAuthorized()
        {
            return StatusCode(401,
                new ApplicationError
                {
                    Error = ApplicationConstants.Error.Unauthorized,
                    ErrorDescription = "The action cannot be completed due to unthorization."
                }.ToString());
        }

        protected IActionResult NullOrEmpty()
        {
            return BadRequest(
                new ApplicationError
                {
                    Error = ApplicationConstants.Error.NullOrEmpty,
                    ErrorDescription = "The input is null or empty."
                }.ToString());
        }

        protected IActionResult InvalidSize(string itemName, int maxSize)
        {
            return BadRequest(
                new ApplicationError
                {
                    Error = ApplicationConstants.Error.InvalidSize,
                    ErrorDescription = "The item has maximum size of " + maxSize + "."
                }.ToString());

        }

        protected IActionResult UnSupportedType(string[] supportedType)
        {
            return BadRequest(
                new ApplicationError
                {
                    Error = ApplicationConstants.Error.UnSupportedType,
                    ErrorDescription = "The type is not supported."
                }.ToString());
        }
    }
}
