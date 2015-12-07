using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class HelperController : BaseFrontController
    {
        public ActionResult RenderLoginBar()
        {
            return PartialView(new LoginViewModel{IsUserLoggedIn = IsUserLoggedIn,UserId=UserId,UserName=UserName});
        }
    }
}
