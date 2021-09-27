using DemoAssessmentWeb.Helpers;
using DemoAssessmentWeb.Models;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DemoAssessmentWeb.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model,string ReturnUrl ="")
        {
            try
            {
                ModelState.Remove("RememberMe");
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await _userManager.FindByEmailAsync(model.Email);

                var result =
                     await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    // Add all the claims that are selected on the UI
                    var lstClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.GivenName, user.FirstName + " " + (user.LastName ?? "")),
                        new Claim("Role", role),
                        new Claim("UserContext", Newtonsoft.Json.JsonConvert.SerializeObject(user))
                    };


                    var existingClaims = await _userManager.GetClaimsAsync(user);
                    // remove all existing claims 
                    var removeresult = _userManager.RemoveClaimsAsync(user, existingClaims).GetAwaiter().GetResult();
                    // add new claims for user
                    var claimresult = await _userManager.AddClaimsAsync(user, lstClaims);

                    await _signInManager.RefreshSignInAsync(user);
                    //LanguageClass.UpdateDefaultLanguage(true);
                    SessionHelper.UserName = user.UserName;
                    SessionHelper.Email = user.Email;
                    SessionHelper.Password = model.Password;

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return RedirectToLocal(ReturnUrl);
                    }

                    //return RedirectToLocal(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
              
                else
                {
                   
                    ModelState.AddModelError(string.Empty,"Invalid Login Attempt");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
              
            }
            return View(model);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                HttpContext.Session.Clear();

            }
            catch (Exception ex)
            {
               
            }
            return RedirectToAction("Login");
        }
    }
}
