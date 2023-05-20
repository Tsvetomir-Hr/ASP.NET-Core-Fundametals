using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebShopDemo.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public string UserFirstName 
        {
            get
            {
                string firtsName = string.Empty;

                if (User != null && User.HasClaim(c=>c.Type=="first_name"))
                {
                    firtsName = User.Claims
                        .FirstOrDefault(c=>c.Type=="first_name")
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
            ViewBag.UserFirstName = UserFirstName;

            base.OnActionExecuted(context);

        }
    }
}

