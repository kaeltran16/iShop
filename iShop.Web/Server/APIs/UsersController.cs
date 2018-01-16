using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using iShop.Web.Server.Commons.Extensions;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.Server.APIs
{
    public class UsersController: BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
 
        public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
        }
 
        //
        // GET:
        [Authorize(Roles = "SuperUser, User")]
        [HttpGet("/api/userinfo")]
        public async Task<IActionResult> Userinfo()
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


            return Ok(userResource);
        }
    }
}
