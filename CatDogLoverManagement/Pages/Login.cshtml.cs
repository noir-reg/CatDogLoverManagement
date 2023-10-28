using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatDogLoverManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CatDogLoverManagement.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository userRepository;

        [BindProperty]
        public Login LoginViewModel { get; set; }


        public LoginModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public void OnGet()
        {

        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("Home");
        }
        public async Task<IActionResult> OnPost(string ReturnUrl)
        {
            var result = await userRepository.LoginAsync(LoginViewModel.Username, LoginViewModel.Password);
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            }

            HttpContext.Session.SetString("username", LoginViewModel.Username);

            HttpContext.Session.SetString("userId", result.UserId.ToString());
            HttpContext.Session.SetString("Role", result.Role.RoleName);

            return RedirectToPage("Home");

            //var signInResult = await signInManager.PasswordSignInAsync(
            //  LoginViewModel.Username, LoginViewModel.Password, false, false);

            //if (signInResult.Succeeded)
            //{
            //    if (!string.IsNullOrWhiteSpace(ReturnUrl))
            //    {
            //        return RedirectToPage(ReturnUrl);
            //    }

            //    return RedirectToPage("Index");
            //}
            //else
            //{
            //    ViewData["Notification"] = new Notification
            //    {
            //        type = Repository.Models.Enums.NotificationType.Error,
            //        Message = "Unable to Login"
            //    };

            //    return Page();
            //}
        }

    }
}
