using CsQuery;
using CsQuery.Promises;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsQueryDemo
{
    public class Program
    {


        public static void Main(string[] args)
        {
            Qyer qyer = new Qyer();
            qyer.Start();
            Console.ReadLine();
        }



        public static HomeData Home()
        {
            var result = new HomeData();
            var url = "http://z.qyer.com/";
            var dom = CQ.CreateFromUrl(url);
            //限时特卖
            dom[".zw-home-todaysale-list li>a"].Each((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                    result.HomeLinks.Add(new Link { Address = href, Title = "限时特卖" });
            });
            //机酒自由行
            dom[".zw-home-ziyouxing-list li>a"].Each((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                    result.HomeLinks.Add(new Link { Address = href, Title = "限时特卖" });
            });
            //城市玩乐
            dom[".zw-home-wanle-list li>a"].Each((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                    result.HomeLinks.Add(new Link { Address = href, Title = "城市玩乐" });
            });

            //主题
            dom[".zw-home-sliders-list li>a"].Each((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                    result.ActivityLinks.Add(new Link { Address = href, Title = href.Substring(href.LastIndexOf("=") + 1) });
            });

            //主题
            dom[".zw-home-category-place li>a"].Each((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("all_"))
                    result.DestinationLinks.Add(new Link { Address = href, Title = e.InnerHTML });
            });
            return result;
        }

        public static List<Link> CrawlList(Link link)
        {
            List<Link> links = new List<Link>();
            var dom = CQ.CreateFromUrl(link.Address);
            dom[".zw-module-productlist-unit .zw-module-bigcard-item>a"].Each((i, e) =>
            {
                var href = e["href"];
                if (!string.IsNullOrEmpty(href) && href.Contains("http"))
                    links.Add(new Link { Address = href });
            });
            return links;
        }

        public static void M1()
        {
            CQ dom = "<div>Hello world! <b>I am feeling bold!</b> What about <b>you?</b></div>";
            CQ bold = dom["b"];
            foreach (var b in bold.ToList())
            {

            }

            string boldText = bold.Text();
            Console.WriteLine(boldText);

            string html = dom.Render();
            Console.WriteLine(html);

            string bold2 = dom["div > b:first-child"].Text();
            Console.WriteLine(bold2);

            string bold3 = dom["b:first"].Text();
            Console.WriteLine(bold3);

            string bold4 = dom.Select("b")[0].InnerText;
            Console.WriteLine(bold4);

            string bold5 = dom["b"].Contents()[0].NodeValue;
            Console.WriteLine(bold5);

            var dom1 = CQ.Create(html);

            var dom2 = CQ.CreateFromUrl("http://www.jquery.com");

            string selectionHtml = dom2[".just-this-class"].RenderSelection();

            dom2.Select("div > span").Eq(1).Text("Change the text content of the 2nd span child of each div");

            var childSpans = dom2["body"].Find(":first-child");

            IDomObject element = dom2[0];
            string id = element.Id;
            string classes = element.ClassName;

            string href = dom2[0]["href"];

            string html2 = dom2["#my-link"].Html();

            string html3 = dom2.Document.GetElementById("my-link").InnerHTML;

            dom.Each((i, e) =>
            {
                if (e.Id == "remove-this-id")
                {
                    //   e.Parent().RemoveChild(e);
                }
            });
        }
    }

    public class HomeData
    {
        public List<Link> HomeLinks { set; get; }

        public List<Link> ActivityLinks { set; get; }

        public List<Link> DestinationLinks { set; get; }

        public HomeData()
        {
            HomeLinks = new List<Link>();
            ActivityLinks = new List<Link>();
            DestinationLinks = new List<Link>();
        }
    }

    public class Link
    {
        public string Title { set; get; }

        public string Address { set; get; }
    }

    public class Deal
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [JsonProperty("productId")]
        public string ProductId { set; get; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [JsonProperty("productName")]
        public string ProductName { set; get; }

        /// <summary>
        /// 起价
        /// </summary>
        [JsonProperty("startingPrice")]
        public int StartingPrice { set; get; }

        /// <summary>
        /// 出发城市
        /// </summary>

        [JsonProperty("departure")]
        public string Departure { set; get; }

        /// <summary>
        /// 目的地城市
        /// </summary>
        [JsonProperty("arrivalCity")]
        public string ArrivalCity { set; get; }

        /// <summary>
        /// 目的地国家
        /// </summary>
        [JsonProperty("arrivalCountry")]
        public string ArrivalCountry { set; get; }

        /// <summary>
        /// 供应商
        /// </summary>
        [JsonProperty("supplier")]
        public string Supplier { set; get; }

        /// <summary>
        /// 访问量
        /// </summary>
        [JsonProperty("pv")]
        public int PV { set; get; }

        /// <summary>
        /// 已售数量
        /// </summary>
        [JsonProperty("soldCount")]
        public int SoldCount { set; get; }

        /// <summary>
        /// 航司
        /// </summary>
        [JsonProperty("air")]
        public string Air { set; get; }

        /// <summary>
        /// 收藏次数
        /// </summary>
        [JsonProperty("collect")]
        public int Collect { set; get; }

    }
}
