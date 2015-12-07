using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class LoginViewModel
    {
        public bool IsUserLoggedIn { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
}