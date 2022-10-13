using System.ComponentModel.DataAnnotations;

namespace CreativeBlogs.Models;

public class CreateBlogPostModel
{
    [Required]
    [MinLength(1)]
    [MaxLength(150)]
    public string Title { get; set; }

    [Required]
    [MinLength(1)]
    [Display(Name = "Tag")]
    public string TagId { get; set; }

    [MaxLength(32000)]
    public string Description { get; set; }
}
