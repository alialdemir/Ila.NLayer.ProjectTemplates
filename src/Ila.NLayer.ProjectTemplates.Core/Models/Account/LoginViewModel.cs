using System.ComponentModel.DataAnnotations;

namespace Ila.NLayer.ProjectTemplates.Core.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

