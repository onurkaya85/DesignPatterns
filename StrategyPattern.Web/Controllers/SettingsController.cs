using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StrategyPattern.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StrategyPattern.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SettingsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            Settings settings = new();
            if (User.Claims.Where(x => x.Type == Settings.claimDatabaseType).FirstOrDefault() != null)
            {
                settings.DatabaseType = (EDatabaseType)int.Parse(User.Claims.FirstOrDefault(x => x.Type == Settings.claimDatabaseType).Value);
            }
            else
                settings.DatabaseType = settings.GetDefaultDtabaseType;


            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDb(int databaseType)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var newClaim = new Claim(Settings.claimDatabaseType, databaseType.ToString());

            var claims = await _userManager.GetClaimsAsync(user);

            var hasClaim = claims.FirstOrDefault(v => v.Type == Settings.claimDatabaseType);

            if(hasClaim != null)
            {
                await _userManager.ReplaceClaimAsync(user, hasClaim, newClaim);
            }
            else
            {
                await _userManager.AddClaimAsync(user, newClaim);
            }

            await _signInManager.SignOutAsync();

            var result = await HttpContext.AuthenticateAsync();
            await _signInManager.SignInAsync(user, result.Properties);

            return RedirectToAction(nameof(Index));
        }
    }
}
