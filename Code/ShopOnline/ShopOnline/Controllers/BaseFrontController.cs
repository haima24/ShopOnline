using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Constants;

namespace ShopOnline.Controllers
{
    public class BaseFrontController : Controller
    {
        protected bool IsUserLoggedIn { get; set; }
        protected int? UserId { get; set; }
        protected string UserName { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userObj = Session[Common.UserIdKey];
            if(userObj!=null)
            {
                var uId = 0;
                int.TryParse(userObj.ToString(), out uId);
                if(uId>0)
                {
                    UserId = uId;
                    IsUserLoggedIn = true;
                    var userName = Session[Common.UserNameKey];
                    if(userName!=null)
                    {
                        UserName = userName.ToString();
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
