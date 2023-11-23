using System.ComponentModel.DataAnnotations;

namespace Ila.NLayer.ProjectTemplates.Core.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}

