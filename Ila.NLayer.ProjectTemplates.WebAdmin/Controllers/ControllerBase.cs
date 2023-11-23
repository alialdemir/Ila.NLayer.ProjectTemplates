using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ila.NLayer.ProjectTemplates.WebApi.Controllers
{
   // [Authorize]
    [Controller]
    [Produces("application/json")]
    public class ControllerBase : Controller
    {
        #region Properties

        private string _userId = string.Empty;

        // <summary>
        // Current user language
        // </summary>
        public string CurrentUserLanguage
        {
            get
            {
                var acceptLanguage = Request.Headers["Accept-Language"];

                if (acceptLanguage.Count == 0)
                    return "tr-TR";

                return acceptLanguage.ToString();
            }
        }

        /// <summary>
        /// Current user id
        /// </summary>
        public string UserId
        {
            get
            {
                if (string.IsNullOrEmpty(_userId))
                {
                    _userId = User.FindFirst("sub")?.Value;
                }

                return _userId;
            }
        }

        #endregion Properties
    }
}