using CsQuery;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CsQueryDemo
{
    public class Qyer
    {



        /// <summary>
        /// 抓取首页Links
        /// </summary>
        /// <returns></returns>
        public HomeData CrawlHome()
        {
            var result = new HomeData();
            try
            {
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
                        result.DestinationLinks.Add(new Link { Address = href, Title = System.Web.HttpUtility.HtmlDecode(e.InnerHTML.ToTrim()) });
                });
                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return result;
            }
        }

        /// <summary>
        /// 活动专题
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public List<Link> CrawlActivity(Link link)
        {
            List<Link> links = new List<Link>();
            try
            {
                var dom = CQ.CreateFromUrl(link.Address);
                dom[".block-lm-slides li>a"].ExtEach((i, e) =>
                {
                    var href = e["href"];
                    if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                        links.Add(new Link { Address = href, Title = string.Empty });
                });
                dom[".content-list>li>a"].ExtEach((i, e) =>
                {
                    var href = e["href"];
                    if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                        links.Add(new Link { Address = href, Title = string.Empty });
                });
                return links;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return links;
            }

        }

        /// <summary>
        /// 抓取列表页Links
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public List<Link> CrawlList(Link link)
        {
            List<Link> links = new List<Link>();
            try
            {
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
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return links;
            }
        }

        /// <summary>
        /// 获取详情页信息
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public Deal CrawlDeal(Link link)
        {
            try
            {
                var deal = new Deal();
                var dom = CQ.CreateFromUrl(link.Address);
                deal.ProductId = dom[".product-id"].ExtText().Replace("产品编号", string.Empty);
                deal.ProductName  =HttpUtility.HtmlDecode(dom[".fontYaHei"].ExtHtml());
                deal.StartingPrice = dom[".after-price em"].ExtText().ToInt(0);
                deal.Supplier = dom[".sj-top-wrap .sj-top .text-box p"].ExtText();
                deal.PV = dom[".gallery-bottom p:first span"].ExtText().ToInt(0);
                deal.SoldCount = dom[".gallery-bottom p:first span"].ExtText().ToInt(0);
                deal.Air = dom[".triffc-company p:first"].ExtText();

                var contents = dom[".sub-content .p-cont"];
                deal.Departure = dom[".sub-content .p-cont:first"].ExtText();
                var arrival = string.Empty;
                dom[".sub-content .p-cont"].Eq(1).ExtFind("a").ExtEach((i, e) =>
                 {
                     var local = e.InnerHTML.ToTrim();
                     arrival += System.Web.HttpUtility.HtmlDecode(local) + ",";
                 });
                if (arrival.IndexOf(",") > 0)
                {
                    arrival = arrival.Substring(0, arrival.Length - 1);
                    deal.ArrivalCity = arrival.Substring(0, arrival.Length - 1);
                    deal.ArrivalCountry = arrival.Substring(arrival.LastIndexOf(",") + 1);
                }
                else
                {
                    deal.ArrivalCity = string.Empty;
                    deal.ArrivalCountry = string.Empty;
                }
                return deal;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// 活动专题
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public List<Link> CrawlH5Activity(Link link)
        {
            List<Link> links = new List<Link>();
            try
            {
                var dom = CQ.CreateFromUrl(link.Address);
                //秒杀
                dom[".block-lm-slides li>a"].ExtEach((i, e) =>
                {
                    var href = e["href"];
                    if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                        links.Add(new Link { Address = href, Title = string.Empty });
                });
                dom[".content-list>li>a"].ExtEach((i, e) =>
                {
                    var href = e["href"];
                    if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                        links.Add(new Link { Address = href, Title = string.Empty });
                });
                return links;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return links;
            }
        }

        /// <summary>
        /// 获取详情页信息
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public Deal CrawlH5Deal(Link link)
        {
            try
            {
                var deal = new Deal();
                var dom = CQ.CreateFromUrl(link.Address);
                deal.ProductId = dom["#lid"].ExtText();
                deal.ProductName = dom[".topImgInfo"].ExtFind("h1").ExtText();
                deal.StartingPrice = dom[".topImgPrice>em"].ExtText().ToInt(0);
                deal.Supplier = dom[".businessInfoM1"].ExtText();
                deal.PV = dom[".topImgBrowse>span"].ExtText().ToInt(0);
                deal.SoldCount = dom[".topImgSold>span"].ExtText().ToInt(0);
                deal.Air = dom[".trafficB2"].Eq(0).ExtText();

                var contents = dom[".productInfoTable"];
                deal.Departure = contents.Find("td").Eq(1).ExtText();
                var arrival = contents.Find("td").Eq(3).ExtFind("a").ExtText();
                if (arrival.IndexOf(",") > 0)
                {
                    deal.ArrivalCity = arrival.Substring(0, arrival.Length - 1);
                    deal.ArrivalCountry = arrival.Substring(arrival.LastIndexOf(",") + 1);
                }
                else
                {
                    deal.ArrivalCity = string.Empty;
                    deal.ArrivalCountry = string.Empty;
                }
                return deal;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return null;
            }
        }

    }
}
