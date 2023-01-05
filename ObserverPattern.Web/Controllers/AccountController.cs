using ObserverPattern.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObserverPattern.Web.Observer;
using MediatR;
using ObserverPattern.Web.ObserverWithMediatR.Events;

namespace ObserverPattern.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserObserverSubject _userObserver;
        private readonly IMediator _mediator;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, UserObserverSubject userObserver, IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userObserver = userObserver;
            _mediator = mediator;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var hasUser = await _userManager.FindByEmailAsync(email);
            if (hasUser == null)
                return View();

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser,password,true,false);
            if(!signInResult.Succeeded)
            {
                return View();
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateViewModel user)
        {
            var appUser = new AppUser
            {
                UserName = user.UserName,
                Email = user.Email
            };

            var identityResult = await _userManager.CreateAsync(appUser, user.Password);

            if(identityResult.Succeeded)
            {

                //Observer without MediatR
                _userObserver.NotifyObserver(appUser);

                //Observer with MediatR
                await _mediator.Publish(new UserCreatedEvent
                { 
                    AppUser = appUser
                });

                ViewBag.öessage = "Üyelik işlemi başarıyla gerçekleşti";
            }
            else
            {
                ViewBag.öessage = identityResult.Errors.ToList().FirstOrDefault().Description;
            }
            return View();
        }
    }
}
