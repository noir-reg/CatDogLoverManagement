﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Models.ViewModels
{
    public class AddBlogPost
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Image { get; set; } = null!;
    }
}

