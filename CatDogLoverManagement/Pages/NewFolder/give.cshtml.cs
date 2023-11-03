using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatDogLoverManagement.Repository.Models;

namespace CatDogLoverManagement.Pages.NewFolder
{
    public class giveModel : PageModel
    {
        private readonly CatDogLoverManagement.Repository.Models.CatDogLoveManagementContext _context;

        public giveModel(CatDogLoverManagement.Repository.Models.CatDogLoveManagementContext context)
        {
            _context = context;
        }

        public IList<BlogPost> BlogPost { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BlogPosts != null)
            {
                BlogPost = await _context.BlogPosts
                .Include(b => b.Animal)
                .Include(b => b.Service)
                .Include(b => b.User).ToListAsync();
            }
        }
    }
}
