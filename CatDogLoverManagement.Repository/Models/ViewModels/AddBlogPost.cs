using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Range(0, 5000), DataType(DataType.Currency)]
        //trước dấu "," lấy 18 số, sau dấu "," lấy 2 số
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Required]
        public string Image { get; set; } = null!;
    }
}

