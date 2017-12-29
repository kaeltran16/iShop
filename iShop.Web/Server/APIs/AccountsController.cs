using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace iShop.Web.Server.APIs
{
    [Authorize]
    [Route("/api/[controller]")]
    public class AccountsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> identityOptions,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _identityOptions = identityOptions;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok(model.Email);
            }
            // send code for two factor validation
            // which we dont need right now
            //if (result.RequiresTwoFactor)
            //{
            //    return RedirectToAction(nameof(SendCode), new { RememberMe = model.RememberMe });
            //}
            //if (result.IsLockedOut)
            //{
            //    _logger.LogWarning(2, "User account locked out.");
            //    return BadRequest(new ApiError("Lockout"));
            //}

            return BadRequest("Invalid login attempt.");


        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model, string returnUrl = null)
        {
            var currentUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.Firstname,
                LastName = model.Lastname
            };

            var result = await _userManager.CreateAsync(currentUser, model.Password);
            if (result.Succeeded)
            {
                // Add to roles
                var roleAddResult = await _userManager.AddToRoleAsync(currentUser, "User");
                if (roleAddResult.Succeeded)
                {
                    // NOTE:
                    // call the email services for confirm the account or do whatever you wanna do here
                }
            }

            return BadRequest("Can not create your account.");
        }

        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return Ok();
            return BadRequest();
        }

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
        {
            var currentUser = await _userManager.FindByNameAsync(model.Email);
            if (currentUser == null || !(await _userManager.IsEmailConfirmedAsync(currentUser)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return NoContent();
            }
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            // Send an email with this link
            // Call the email service to generate token for reseting the password
            
            return NoContent(); // sends 204
        }

        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok("Reset confirmed");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok("Reset confirmed"); ;
            }
            
            return BadRequest();
        }

        [HttpGet("SendCode")]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return BadRequest();
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            var result = new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe };
            return Ok(result);
        }

        [HttpPost]
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        {
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

                return BadRequest();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid code.");
                return BadRequest();
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
           
            return NoContent();
        }


    }
}
