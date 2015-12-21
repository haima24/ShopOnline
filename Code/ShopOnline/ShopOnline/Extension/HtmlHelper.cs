using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Extension
{
    public static class HtmlHelper
    {
        public static string ToSkypeCall(this string skype)
        {
            return string.Format("skype:{0}?call", skype);
        }
        public static string ToSystemFormat(this decimal? value)
        {
            return string.Format("{0:#,##0} đ", value??0);
        }
        public static string ToFullDateFormat(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}