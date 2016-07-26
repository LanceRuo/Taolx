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
    }
}
