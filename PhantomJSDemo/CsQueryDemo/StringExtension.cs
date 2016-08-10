using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsQueryDemo
{
    public static class StringExtension
    {
        public static string ToTrim(this string source, string defaultVal = "")
        {

            if (source == null) return defaultVal;
            return source.Trim();
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static int ToInt(this string source, int defaultVal = 0)
        {
            if (source.IsNullOrEmpty()) return defaultVal;
            int.TryParse(source, out defaultVal);
            return defaultVal;
        }

        public static decimal ToDecimal(this string source, decimal defaultVal = 0)
        {
            if (source.IsNullOrEmpty()) return defaultVal;
            decimal.TryParse(source, out defaultVal);
            return defaultVal;
        }

        /// <summary>
        /// HtmlDecode
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string source)
        {
            if (source.IsNullOrEmpty())
                return string.Empty;
            return System.Web.HttpUtility.HtmlDecode(source);
        }
    }
}
