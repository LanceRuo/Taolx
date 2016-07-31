using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsQueryDemo
{
   public class QyerCrawlBll
    {
        private readonly Qyer craw1 = new Qyer();
         

        #region 目的地城市列表
        /// <summary>
        /// 目的地城市列表
        /// </summary>
        private readonly static IDictionary<string, string> ArrivesDic = new Dictionary<string, string> {
                {"三亚","三亚"}
                ,{"丽江","丽江"}
                ,{"厦门","厦门"}
                ,{"桂林","桂林"}
                ,{"张家界","张家界"}
                ,{"西安","西安"}
                ,{"北京","北京"}
                ,{"上海","上海"}
                ,{"杭州","杭州"}
                ,{"成都","成都"}
                ,{"苏州","苏州"}
                ,{"呼伦贝尔","呼伦贝尔"}
                ,{"广州","广州"}
                ,{"青岛","青岛"}
                ,{"重庆","重庆"}
                ,{"武汉","武汉"}
                ,{"南京","南京"}
                ,{"哈尔滨","哈尔滨"}
                ,{"深圳","深圳"}
                ,{"珠海","珠海"}
                ,{"北海","北海"}
                ,{"大连","大连"}
                ,{"拉萨","拉萨"}
                ,{"昆明","昆明"}
                ,{"西宁","西宁"}
                ,{"西双版纳","西双版纳"}
                ,{"敦煌","敦煌"}
                ,{"惠州","惠州"}
                ,{"长白山","长白山"}
                ,{"沈阳","沈阳"}
                ,{"香港","香港"}
                ,{"澳门","澳门"}
                ,{"台湾","台湾"}
                ,{"台北","台北"}
                ,{"普吉岛","普吉岛"}
                ,{"清迈","清迈"}
                ,{"曼谷","曼谷"}
                ,{"苏梅岛","苏梅岛"}
                ,{"甲米","甲米"}
                ,{"芭提雅","芭提雅"}
                ,{"清莱","清莱"}
                ,{"皮皮岛","皮皮岛"}
                ,{"华欣","华欣"}
                ,{"马尔代夫","马尔代夫"}
                ,{"巴厘岛","巴厘岛"}
                ,{"首尔","首尔"}
                ,{"济州岛","济州岛"}
                ,{"江原道","江原道"}
                ,{"东京","东京"}
                ,{"大阪","大阪"}
                ,{"冲绳","冲绳"}
                ,{"福冈","福冈"}
                ,{"京都","京都"}
                ,{"名古屋","名古屋"}
                ,{"北海道","北海道"}
                ,{"札幌","札幌"}
                ,{"奈良","奈良"}
                ,{"熊本","熊本"}
                ,{"鹿儿岛","鹿儿岛"}
                ,{"静冈","静冈"}
                ,{"沙巴","沙巴"}
                ,{"亚庇","亚庇"}
                ,{"吉隆坡","吉隆坡"}
                ,{"兰卡威","兰卡威"}
                ,{"槟城","槟城"}
                ,{"新加坡","新加坡"}
                ,{"芽庄","芽庄"}
                ,{"岘港","岘港"}
                ,{"河内","河内"}
                ,{"长滩岛","长滩岛"}
                ,{"宿务","宿务"}
                ,{"薄荷岛","薄荷岛"}
                ,{"迪拜","迪拜"}
                ,{"阿布扎比","阿布扎比"}
                ,{"吴哥窟","吴哥窟"}
                ,{"暹粒","暹粒"}
                ,{"斯里兰卡","斯里兰卡"}
                ,{"尼泊尔","尼泊尔"}
                ,{"老挝","老挝"}
                ,{"缅甸","缅甸"}
                ,{"美国","美国"}
                ,{"洛杉矶","洛杉矶"}
                ,{"纽约","纽约"}
                ,{"拉斯维加斯","拉斯维加斯"}
                ,{"旧金山","旧金山"}
                ,{"塞班岛","塞班岛"}
                ,{"夏威夷","夏威夷"}
                ,{"华盛顿","华盛顿"}
                ,{"波士顿","波士顿"}
                ,{"关岛","关岛"}
                ,{"西雅图","西雅图"}
                ,{"芝加哥","芝加哥"}
                ,{"阿拉斯加","阿拉斯加"}
                ,{"曼哈顿","曼哈顿"}
                ,{"加拿大","加拿大"}
                ,{"巴西","巴西"}
                ,{"法国","法国"}
                ,{"英国","英国"}
                ,{"西班牙","西班牙"}
                ,{"意大利","意大利"}
                ,{"德国","德国"}
                ,{"希腊","希腊"}
                ,{"瑞士","瑞士"}
                ,{"荷兰","荷兰"}
                ,{"捷克","捷克"}
                ,{"奥地利","奥地利"}
                ,{"土耳其","土耳其"}
                ,{"俄罗斯","俄罗斯"}
                ,{"葡萄牙","葡萄牙"}
                ,{"比利时","比利时"}
                ,{"丹麦","丹麦"}
                ,{"匈牙利","匈牙利"}
                ,{"冰岛","冰岛"}
                ,{"摩纳哥","摩纳哥"}
                ,{"瑞典","瑞典"}
                ,{"澳大利亚","澳大利亚"}
                ,{"新西兰","新西兰"}
                ,{"塞舌尔","塞舌尔"}
                ,{"南非","南非"}
                ,{"埃及","埃及"}
                ,{"摩洛哥","摩洛哥"}
                ,{"毛里求斯","毛里求斯"}
                ,{"斐济","斐济"}
                ,{"大溪地","大溪地"}
        };
        #endregion

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            var homeData = craw1.CrawlHome();
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(homeData));
            //首页
            homeData.HomeLinks.ForEach((link) =>
            {
                var deal = craw1.CrawlDeal(link);
                if (deal == null) return;
                InsertData(deal, CrawDataType.Home, "首页");
            });

            //活动专题
            homeData.ActivityLinks.ForEach((activityLink) =>
            {
                var links = craw1.CrawlActivity(activityLink);
                if (links == null) return;
                links.ForEach(link =>
                {
                    var deal = craw1.CrawlDeal(link);
                    if (deal == null) return;
                    InsertData(deal, CrawDataType.Activity, activityLink.Title);
                });
            });

            //目的地:     出发城市 : 上海/杭州  不限制
            homeData.DestinationLinks.ForEach((link) =>
            {
                if (!ArrivesDic.ContainsKey(link.Title))
                    return;
                //不限制
                var listLinks = craw1.CrawlList(link);
                listLinks.ForEach(dealLink =>
                {
                    var deal = craw1.CrawlDeal(dealLink);
                    if (deal != null)
                        InsertData(deal, CrawDataType.Destination, string.Format("{0}-{1}", "不限", dealLink.Title));
                });
                //上海/杭州 
                link.Address = link.Address.Replace("all_0_", "all_2_");
                listLinks.ForEach(dealLink =>
                {
                    var deal = craw1.CrawlDeal(dealLink);
                    if (deal != null)
                        InsertData(deal, CrawDataType.Destination, string.Format("{0}-{1}", "上海/杭州", dealLink.Title));
                });
            });

            //h5活动 
            var h5Activities = new List<Link> {
                new Link { Title="初秋第一趴", Address="http://m.qyer.com/z/zt/cqdyp/"}
            };

            h5Activities.ForEach(alink =>
            {
                var links = craw1.CrawlH5Activity(alink);
                if (links == null) return;
                links.ForEach(link =>
                {
                    var deal = craw1.CrawlH5Deal(link);
                    if (deal == null) return;
                    InsertData(deal, CrawDataType.Activity, alink.Title);
                });
            });
        }

        private void InsertData(Deal deal, CrawDataType crawDataType, string activityName = "")
        {
            
        }

    }


    public enum CrawDataType
    {
        /// <summary>
        /// 首页
        /// </summary>
        Home = 1,

        /// <summary>
        /// 活动专题
        /// </summary>
        Activity = 2,

        /// <summary>
        /// 目的地
        /// </summary>
        Destination = 3,
    }
}
