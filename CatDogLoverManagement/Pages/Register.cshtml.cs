using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatDogLoverManagement.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public RegisterModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [BindProperty]
        public Register RegisterViewModel { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = new User()
            {
                Username = RegisterViewModel.Username,
                Email = RegisterViewModel.Email,
                Password = RegisterViewModel.Password,
                Phonenumber = RegisterViewModel.Phonenumber,
                Action = RegisterViewModel.Action,
                RoleId = RegisterViewModel.RoleId,
            };

            await userRepository.AddAsync(user);
            ViewData["Notification"] = new Notification
            {
                Message = "Record updated successfully!",
                type = Repository.Models.Enums.NotificationType.Success
            };

            return Page();
        }
    }
}
