using System.ComponentModel.DataAnnotations;

namespace ASP.NET___fiorello_backend.Areas.Admin.ViewModels.Category
{
    public class CategoryEditVM
    {
        [Required]
        public string Name { get; set; }
    }
}
