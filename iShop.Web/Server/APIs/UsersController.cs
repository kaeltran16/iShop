using System;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using iShop.Web.Server.Commons.Extensions;
using iShop.Web.Server.Commons.Helpers;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using OpenIddict.Core;

namespace iShop.Web.Server.APIs
{
    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        //
        // GET:
        [Authorize]
        [HttpGet("/api/userinfo")]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());

            if (user == null)
            {
                return BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "The user profile is no longer available."
                });
            }

            var userResource = Mapper.Map<ApplicationUser, ApplicationUserResource>(user);

            var roles = await _userManager.GetRolesAsync(user);

            var userData = new { userInfo = userResource, roles = roles };


            return Ok(userData);
        }

        // POST:
        [HttpPost("/api/userInfo")]
        public async Task<IActionResult> Update(ApplicationUserResource resource)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());

            _mapper.Map(resource, user);

            var result = await _userManager.UpdateAsync(user);

            
            if (!result.Succeeded)
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ApplicationConstants.ControllerName.User, user.Id);
                return FailedToSave(user.Id);
            }

            user = await _userManager.FindByIdAsync(User.GetUserId().ToString());

            return Ok(_mapper.Map<ApplicationUser, ApplicationUserResource>(user));
        }

        [HttpPost("/api/changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordResource resource)
        {
            var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());

            var result = await _userManager.ChangePasswordAsync(user, resource.OldPassword, resource.NewPassword);

            if (!result.Succeeded)
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ApplicationConstants.ControllerName.User, user.Id);
                return FailedToSave(user.Id);
            }

            return Ok();
        }




    }
}
