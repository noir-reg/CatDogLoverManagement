using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CatDogLoverManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using Microsoft.VisualBasic;

namespace CatDogLoverManagement.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository userRepository;
        public string Msg { get; set; }

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
            var result = await userRepository.LoginAsync(LoginViewModel.Username.Trim(), LoginViewModel.Password.Trim());
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            }


            HttpContext.Session.SetString("username", LoginViewModel.Username);

            HttpContext.Session.SetString("userId", result.UserId.ToString());
            HttpContext.Session.SetString("Role", result.Role.RoleName);

            return RedirectToPage("Home");


            // var result = await userRepository.GetAccount(LoginViewModel.Username.Trim(), LoginViewModel.Password.Trim());
            // if(result == null)
            // {

            //     Msg = "Account is not found";
            //     return Page();
            //}

            // if (result.UserId == 00000000-0000-0000-0000-000000000000)
            //    {
            //         string jsonString = JsonSerializer.Serialize(result);
            //         HttpContext.Session.SetString("account", jsonString);
            //          return RedirectToPage("/Admin/AccessPostList");
            //    }
            //  else
            //     {
            //     string jsonString = JsonSerializer.Serialize(result);
            //     HttpContext.Session.SetString("account", jsonString);

            //     return RedirectToPage("/Post/servicePosts");
            //      }





            return Page();


        }

    }
}
