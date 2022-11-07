using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UmbracoDemo.Dtos
{
    public class ArticleToAddDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string Brief { get; set; }
        [BindProperty]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:MM tt}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }
        public IFormFile? FeaturedImage { get; set; }

        public IFormFile? MediaImage { get; set; }
    }
}
