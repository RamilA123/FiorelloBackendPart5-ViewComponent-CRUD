using System.ComponentModel.DataAnnotations;

namespace ASP.NET___fiorello_backend.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
    }

  
}
