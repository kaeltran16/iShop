using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Server.Commons.Helpers;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.Server.APIs
{
    [Authorize]
    [Route("/api/[controller]")]
    public class AccountsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountsController> _logger;
        private readonly IMapper _mapper;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            ILogger<AccountsController> logger, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterResource model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUser = _mapper.Map<RegisterResource, ApplicationUser>(model);
            currentUser.UserName = model.Email;

            var createResult = await _userManager.CreateAsync(currentUser, model.Password);
            var roleResult = await _userManager.AddToRoleAsync(currentUser, ApplicationConstants.RoleName.User);

            var claimResult =
                await _userManager.AddClaimAsync(currentUser, new Claim(ApplicationConstants.RoleName.User, "true"));

            if (createResult.Succeeded && roleResult.Succeeded && claimResult.Succeeded)
            {
                _logger.LogInformation(LoggingEvents.Success, model.Email + " created");
                return Ok();
            }
            _logger.LogWarning(LoggingEvents.Fail, model.Email + " failed to create");

            return BadRequest(createResult);
        }
    }
}

