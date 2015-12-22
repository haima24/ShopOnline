using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Extension
{
    public static class HtmlHelperExtension
    {
        public static string ToSkypeCall(this string skype)
        {
            return string.Format("skype:{0}?call", skype);
        }
        public static string ToSystemFormat(this decimal? value)
        {
            return string.Format("{0:#,##0} đ", value ?? 0);
        }
        public static string ToFullDateFormat(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy HH:mm:ss");
        }
        public static IHtmlString ToFriendlyUrl(this HtmlHelper helper, string urlToEncode)
        {
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = urlToEncode.Normalize(System.Text.NormalizationForm.FormD);
            urlToEncode= regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');

            var url = new StringBuilder();

            foreach (char ch in urlToEncode)
            {
                switch (ch)
                {
                    case ' ':
                        url.Append('-');
                        break;
                    case '&':
                        url.Append("and");
                        break;
                    case '\'':
                        break;
                    default:
                        if ((ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'))
                        {
                            url.Append(ch);
                        }
                        else
                        {
                            url.Append('-');
                        }
                        break;
                }
            }

            return new MvcHtmlString(url.ToString());
        }
    }
}