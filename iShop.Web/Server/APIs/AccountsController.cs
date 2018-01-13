using System.Threading.Tasks;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterResource model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            var currentUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                Ward = model.Ward,
                District = model.District
            };

            var result = await _userManager.CreateAsync(currentUser, model.Password);
            if (result.Succeeded)
            {
                //// Add to roles
                //var roleAddResult = await _userManager.AddToRoleAsync(currentUser, "User");
                //if (roleAddResult.Succeeded)
                //{
                    _logger.LogInformation(LoggingEvents.Success, model.Email + " created");
                return Ok(model.Email);
                // NOTE:
                // call the email services for confirm the account or do whatever you wanna do here
                //}
            }
            _logger.LogWarning(LoggingEvents.Fail, model.Email + " failed to create");
            return BadRequest(model.Email);
        }

        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                _logger.LogInformation(LoggingEvents.Success, userId + " confirm email successfully");
                return Ok();
            }

            _logger.LogWarning(userId + " failed to confirm email");
            return BadRequest();
        }

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var currentUser = await _userManager.FindByNameAsync(model.Email);
            if (currentUser == null || !(await _userManager.IsEmailConfirmedAsync(currentUser)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return NoContent();
            }
           
            // Call the email service to generate token for reseting the password
            
            return NoContent(); 
        }

        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok("Reset confirmed");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation(LoggingEvents.Success, model.Email + " reset password");
                return Ok(); ;
            }
            
            return BadRequest();
        }


        [HttpGet("SendCode")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return BadRequest();
            }

            // Generate the token and send it
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest();
            }

            var message = "Your security code is: " + code;
            if (model.SelectedProvider == "Email")
            {
                // send confirmed email
            }
            else if (model.SelectedProvider == "Phone")
            {
                // send sms
            }

            return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        [HttpGet("VerifyCode")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return BadRequest();
            }
            var result = new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe };
            return Ok(result);
        }


        [HttpPost("VerifyCode")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
            if (result.Succeeded)
            {
    
                return Ok();
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning(LoggingEvents.Fail, "User account locked out.");
                return BadRequest();
            }

            ModelState.AddModelError(string.Empty, "Invalid code.");
            return BadRequest();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(LoggingEvents.Success, "User logged out.");
            return NoContent();
        }


    }
}

