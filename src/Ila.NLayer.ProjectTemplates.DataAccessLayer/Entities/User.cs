
using Microsoft.AspNetCore.Identity;
namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; }
        public string? Phone { get; set; }
    }
}

