using CsQuery;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsQueryDemo
{
    public class Qyer
    {

        public void Start()
        {
            var homeData = CrawlHome();
            Console.WriteLine(JsonConvert.SerializeObject(homeData));
            homeData.DestinationLinks.ForEach((link) =>
            {
                //不限制
                var listLinks = CrawlList(link);
                listLinks.ForEach(dealLink =>
                {
                    var deal = CrawlDeal(dealLink);
                    Console.WriteLine(JsonConvert.SerializeObject(deal));
                });
                //上海/杭州 
                link.Address = link.Address.Replace("all_0_", "all_2_");
                listLinks.ForEach(dealLink =>
                {
                    var deal = CrawlDeal(dealLink);
                    Console.WriteLine(JsonConvert.SerializeObject(deal));
                });
            });
        }

        /// <summary>
        /// 抓取首页Links
        /// </summary>
        /// <returns></returns>
        public HomeData CrawlHome()
        {
            var result = new HomeData();
            var url = "http://z.qyer.com/";
            var dom = CQ.CreateFromUrl(url);
            //Console.WriteLine(dom.ExtText());
            //限时特卖
            dom[".zw-home-todaysale-list li>a"].ExtEach((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                    result.HomeLinks.Add(new Link { Address = href, Title = "限时特卖" });
            });
            //机酒自由行
            dom[".zw-home-ziyouxing-list li>a"].ExtEach((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                    result.HomeLinks.Add(new Link { Address = href, Title = "限时特卖" });
            });
            //城市玩乐
            dom[".zw-home-wanle-list li>a"].ExtEach((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                    result.HomeLinks.Add(new Link { Address = href, Title = "城市玩乐" });
            });

            //主题
            dom[".zw-home-sliders-list li>a"].ExtEach((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                    result.ActivityLinks.Add(new Link { Address = href, Title = href.Substring(href.LastIndexOf("=") + 1) });
            });

            //目的地
            dom[".zw-home-category-place li>a"].ExtEach((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("all_"))
                    result.DestinationLinks.Add(new Link { Address = href, Title = e.InnerHTML });
            });
            return result;
        }

        /// <summary>
        /// 抓取列表页Links
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public List<Link> CrawlList(Link link)
        {
            List<Link> links = new List<Link>();
            var dom = CQ.CreateFromUrl(link.Address);
            //Console.WriteLine(dom.ExtText());
            dom[".zw-module-productlist-unit .zw-module-bigcard-h2ul-wrap>h2>a"].ExtEach((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                    links.Add(new Link { Address = href, Title = e["title"] });
            });
            return links;
        }

        public Deal CrawlDeal(Link link)
        {
            var deal = new Deal();
            var dom = CQ.CreateFromUrl(link.Address);
            //Console.WriteLine(dom.ExtText());
            deal.ProductId = dom[".product-id"].ExtText().Replace("产品编号", string.Empty);
            deal.ProductName = dom[".fontYaHei"].ExtText();
            deal.StartingPrice = dom[".after-price em"].ExtText().ToInt(0);
            deal.Supplier = dom[".sj-top-wrap .sj-top .text-box p"].ExtText();
            deal.PV = dom[".gallery-bottom p:first span"].ExtText().ToInt(0);
            deal.SoldCount = dom[".gallery-bottom p:first span"].ExtText().ToInt(0);
            deal.Air = dom[".triffc-company p:first"].ExtText();

            var contents = dom[".sub-content .p-cont"];
            deal.Departure = dom[".sub-content .p-cont:first"].ExtText();
            var arrival = dom[".sub-content .p-cont"].Eq(1).ExtFind("a").ExtEach((i, e) =>
            {
                var local = e.InnerHTML.ToTrim();
                deal.ArrivalCity += System.Web.HttpUtility.HtmlDecode(local) + ",";
            });
            if (deal.ArrivalCity.IndexOf(",") > 0)
            {
                deal.ArrivalCity = deal.ArrivalCity.Substring(0, deal.ArrivalCity.Length - 1);
                deal.ArrivalCountry = deal.ArrivalCity.Substring(deal.ArrivalCity.LastIndexOf(",") + 1);
            }
            else
            {
                deal.ArrivalCity = string.Empty;
                deal.ArrivalCountry = string.Empty;
            }

            return deal;
        }
    }
}
