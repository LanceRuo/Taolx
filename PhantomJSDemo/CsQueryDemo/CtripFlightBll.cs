using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsQueryDemo
{
    public class CtripFlightBll
    {
        private readonly CtripFlightCrawl _crawl = new CtripFlightCrawl();


        public void Run()
        {
            //国内
            var internalOne = _crawl.CrawlInternalOne();

            internalOne.ForEach(one =>
            {
                var internalTwo = _crawl.CrawlInternalTwo(one);
                internalTwo.ForEach(two =>
                {
                    var f = _crawl.CrawlInternalThree(two);
                });
            });
            var international = new List<Link> {
                new Link
                {
                     Address=string.Format("http://flights.ctrip.com/international/Schedule/{0}.html","SHA-OSA"),
                    Title="sss"
                }
            };
            international.ForEach(l =>
            {
                var f = _crawl.CrawInternational(l);

            });

        }



    }
}
