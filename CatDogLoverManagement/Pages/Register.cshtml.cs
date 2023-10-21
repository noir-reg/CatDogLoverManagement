using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.Enums;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatDogLoverManagement.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public RegisterModel(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
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
                RoleId = await roleRepository.GetRoleId(Role.user.ToString())

            };

            var result = await userRepository.AddAsync(user);
            if (result != null)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Record updated successfully!",
                    type = Repository.Models.Enums.NotificationType.Success
                };
                return RedirectToPage("Login");
            }
            return Page();
        }
    }
}
