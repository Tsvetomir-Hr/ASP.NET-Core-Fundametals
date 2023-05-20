using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebShopDemo.Core.Constants;

namespace WebShopDemo.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
        /// <summary>
        /// property that return user firstName from the claims
        /// </summary>
        public string UserFirstName
        {
            get
            {
                string firtsName = string.Empty;

                if (User?.Identity?.IsAuthenticated ?? false && User.HasClaim(c => c.Type == ClaimTypeConstants.FirstName))
                {
                    firtsName = User.Claims
                        .FirstOrDefault(c => c.Type == ClaimTypeConstants.FirstName)
                        ?.Value ?? firtsName;
                }
                return firtsName;
            }
        }
        /// <summary>
        /// This method will be executed after every action and will fill the view bag with user first name.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {

                ViewBag.UserFirstName = UserFirstName;
            }

            base.OnActionExecuted(context);

        }
    }
}

