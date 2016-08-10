using CsQuery;
using CsQuery.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CsQueryDemo
{
    public static class CQExtension
    {
        public static Tuple<bool, CQ> ExtCreateFromUrl(this CQ source, string url, ServerConfig options = null)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(0, 0, 30);
                var response = client.GetAsync(url).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var cq = CQ.CreateDocument(responseContent);
                return new Tuple<bool, CQ>(true, cq);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return new Tuple<bool, CQ>(false, null);
            }
        }

        public static string ExtText(this CQ source, string defaultVal = "")
        {
            if (source == null)
                return defaultVal;
            return source.Text().ToTrim();
        }

        public static string ExtHtml(this CQ source, string defaultVal = "")
        {
            if (source == null)
                return defaultVal;
            return source.Html().ToTrim();
        }

        public static CQ ExtFind(this CQ source, string selector)
        {
            if (source == null)
                return null;
            return source.Find(selector);
        }

        public static CQ ExtEach(this CQ source, Action<int, IDomObject> func, bool noThrowException = true)
        {
            if (func == null || source == null)
                return source;
            source.Each((i, e) =>
            {
                try
                {
                    func(i, e);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    if (!noThrowException)
                        throw ex;
                }
            });
            return source;
        }

        public static CQ ExtEach(this CQ source, Action<IDomObject> func, bool noThrowException = true)
        {
            if (func == null || source == null)
                return source;
            source.Each((e) =>
            {
                try
                {
                    func(e);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                    if (!noThrowException)
                        throw ex;
                }
            });
            return source;
        }

        /// <summary>
        /// next
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static CQ ExtNext(this CQ source)
        {
            if (source == null) return null;
            return source.Next();
        }

        public static string ExtAttr(this CQ source, string attName)
        {
            if (source == null) return string.Empty;
            return source.Attr(attName).ToTrim();
        }

        public static string ExtAttrData(this CQ source, string name)
        {
            return source.ExtAttr(string.Format("data-{0}", name));
        }
    }

    public static class IDomObjectExtension
    {
        public static CQ ExtCq(this IDomObject idom)
        {
            if (idom == null)
                return CQ.Create(string.Empty);
            return CQ.Create(idom.OuterHTML);
        }

        public static string ExtInnerHTML(this IDomObject idom)
        {
            if (idom == null)
                return string.Empty;
            return idom.InnerHTML.ToTrim();
        }

        public static string ExtOuterHTML(this IDomObject idom)
        {
            if (idom == null)
                return string.Empty;
            return idom.OuterHTML.ToTrim();
        }
    }

}
