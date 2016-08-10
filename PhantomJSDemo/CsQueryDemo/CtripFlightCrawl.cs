using CsQuery;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsQueryDemo
{
    public class CtripFlightCrawl
    {
        /// <summary>
        /// 抓取国内航班 :01   出发地列表页
        /// </summary>
        public List<Link> CrawlInternalOne()
        {
            var result = new List<Link>();
            var url = "http://flights.ctrip.com/schedule/";
            try
            {
                var dom = CQ.CreateFromUrl(url);
                dom[".natinal_m .m>a"].ExtEach((index, item) =>
                {
                    var link = new Link
                    {
                        Address = string.Format("http://flights.ctrip.com{0}", item["href"].ToTrim()),
                        Title = System.Web.HttpUtility.HtmlDecode(item.InnerText.ToTrim())
                    };
                    result.Add(link);
                });
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 抓取国内航班:02  出发地-目的地列表页
        /// </summary>
        /// <returns></returns>
        public List<Link> CrawlInternalTwo(Link link)
        {
            var result = new List<Link>();
            try
            {
                var dom = CQ.CreateFromUrl(link.Address);
                dom["#ulD_Domestic .m>a"].ExtEach((index, item) =>
                {
                    var lk = new Link
                    {
                        Address = item["href"].ToTrim(),
                        Title = System.Web.HttpUtility.HtmlDecode(item.InnerText.ToTrim())
                    };
                    result.Add(lk);
                });
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 抓取国内航班:03 航班详情数据
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public List<FlightInfo> CrawlInternalThree(Link link)
        {
            List<FlightInfo> result = new List<FlightInfo>();
            try
            {
                Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(link));
                var dom = CQ.CreateFromUrl(link.Address);
                dom[".result_m tr[data-defer=form]"].ExtEach((index, item) =>
                {
                    FlightInfo d = new FlightInfo();
                    var self = item.ExtCq();
                    d.FlightNo = self["strong[data-defer=jmpPlugin]"].ExtHtml();//航班号
                    d.Company = self["strong[data-defer=jmpPlugin]"].ExtAttrData("description");//航司
                    var companyCode = self["strong[data-defer=jmpPlugin]"].ExtAttr("class");//航司二字码
                    var companyCodeTempArray = companyCode.Split('_');
                    if (companyCodeTempArray.Length == 2)
                        d.CompanyCode = companyCodeTempArray[1].ToTrim().ToUpper();
                    else
                        d.CompanyCode = string.Empty;
                    d.AirplaneType = self["td[class=flight_logo] .gray"].ExtAttr("code");//机型
                    d.DepartureAirport = self[".depart .airport"].ExtAttr("title");//起飞机场
                    d.DepartureCityCode = self["input.input_date"].ExtAttrData("depart-city");//起飞城市Code
                    d.DepartureTime = self[".depart .time"].ExtHtml();//起飞时间
                    d.ArriveAirport = self[".arrive .airport"].ExtAttr("title");//到达机场
                    d.ArriveCityCode = self["input.input_date"].ExtAttrData("arrive-city");//到达城市Code
                    d.ArriveTime = self[".arrive .time"].ExtHtml();//到达时间
                    d.Price = self[".price_col .price"].ExtHtml().HtmlDecode().Replace("<dfn>¥</dfn>", string.Empty).Replace("<em>起</em>", string.Empty).ToDecimal(-1);//价格
                    d.OnTimePer = self[".punctuality"].ExtHtml().Replace("%", string.Empty).ToDecimal(-1);//准点率,百分比
                    d.StopOverCity = self["td[class=stop] .gray"].ExtHtml().HtmlDecode();//经停城市
                    d.IsStopOver = !d.StopOverCity.IsNullOrEmpty();//是否经停
                    self[".eat span"].ExtEach((idx, eat) =>
                    {
                        var hasEat = eat.ClassName.ToTrim().ToLower().Contains("blue");
                        if (hasEat)
                            d.Meals.Add((idx + 1).ToString());
                    });
                    //班期
                    self[".week .blue"].ExtEach(blue =>
                    {
                        var week = blue.ExtInnerHTML().HtmlDecode();
                        switch (week)
                        {
                            case "一":
                                d.Weeks.Add("1");
                                break;
                            case "二":
                                d.Weeks.Add("2");
                                break;
                            case "三":
                                d.Weeks.Add("3");
                                break;
                            case "四":
                                d.Weeks.Add("4");
                                break;
                            case "五":
                                d.Weeks.Add("5");
                                break;
                            case "六":
                                d.Weeks.Add("6");
                                break;
                            case "日":
                                d.Weeks.Add("7");
                                break;
                            default:
                                break;
                        }
                    });
                    if (d.FlightNo.IsNullOrEmpty())
                        return;
                    d.IsInternal = true;
                    result.Add(d);
                });
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        
        /// <summary>
        /// 抓取国际航班
        /// </summary>
        /// <returns></returns>
        public List<FlightInfo> CrawInternational(Link link)
        {
            List<FlightInfo> result = new List<FlightInfo>();
            try
            {
                Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(link));
                var dom = CQ.CreateFromUrl(link.Address);
                dom[".schedule_table"].First().ExtFind("tr").ExtEach((index, item) =>
                {
                    if (index == 0) return;
                    FlightInfo d = new FlightInfo();
                    var self = item.ExtCq();
                    self.Find("td").ExtEach((tdIdx, eTd) =>
                    {
                        var td = eTd.ExtCq();
                        switch (tdIdx)
                        {
                            case 0: //起飞/到达时间
                                var t0 = eTd.ExtInnerHTML().Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                d.DepartureTime = t0[0].ToTrim();//起飞时间
                                d.ArriveTime = t0[1].ToTrim();//到达时间
                                break;
                            case 1://航空公司/航班号
                                var t1 = eTd.ExtInnerHTML().HtmlDecode().Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                d.Company = t1[0].ToTrim();//航空公司
                                d.FlightNo = t1[1].ToTrim();//航班号
                                break;
                            case 2://出发城市/机场
                                var t2 = eTd.ExtInnerHTML().HtmlDecode().Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                d.DepartureCityCode = t2[0].ToTrim();//出发城市
                                d.DepartureAirport = t2[1].ToTrim();//出发机场
                                break;
                            case 3://到达城市/机场
                                var t3 = eTd.ExtInnerHTML().HtmlDecode().Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                d.ArriveCityCode = t3[0].ToTrim();//到达城市
                                d.ArriveAirport = t3[1].ToTrim();//到达机场
                                break;
                            case 4://机型
                                d.AirplaneType = td.Find("span").ExtHtml().HtmlDecode();
                                break;
                            case 5://经停

                                break;

                            case 6://班期
                                d.Weeks = eTd.ExtInnerHTML().HtmlDecode().ToCharArray().ToList().Select(week => week.ToString()).ToList();
                                break;
                        }
                    });
                    d.CompanyCode = string.Empty;
                    d.StopOverCity = string.Empty;
                    if (d.FlightNo.IsNullOrEmpty())
                        return;
                    d.IsInternal = false;
                    result.Add(d);
                });
            }
            catch (Exception ex)
            {
            }
            return result;
        }
    }



    public class FlightInfo
    {
        /// <summary>
        /// 是否为国内
        /// </summary>
        public bool IsInternal { set; get; }

        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo { set; get; }

        /// <summary>
        /// 航司
        /// </summary>
        public string Company { set; get; }

        /// <summary>
        /// 航司二字码
        /// </summary>
        public string CompanyCode { set; get; }

        /// <summary>
        /// 机型
        /// </summary>
        public string AirplaneType { set; get; }

        /// <summary>
        /// 起飞机场
        /// </summary>
        public string DepartureAirport { set; get; }

        /// <summary>
        /// 起飞城市Code
        /// </summary>
        public string DepartureCityCode { set; get; }

        /// <summary>
        /// 起飞时间
        /// </summary>
        public string DepartureTime { set; get; }

        /// <summary>
        /// 到达机场
        /// </summary>
        public string ArriveAirport { set; get; }

        /// <summary>
        /// 到达城市Code
        /// </summary>
        public string ArriveCityCode { set; get; }

        /// <summary>
        /// 到达时间
        /// </summary>
        public string ArriveTime { set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { set; get; }

        /// <summary>
        /// 准点率,百分比
        /// </summary>
        public decimal OnTimePer { set; get; }

        /// <summary>
        /// 是否经停
        /// </summary>
        public bool IsStopOver { set; get; }

        /// <summary>
        /// 经停城市
        /// </summary>
        public string StopOverCity { set; get; }

        /// <summary>
        /// 班期
        /// </summary>
        public List<string> Weeks { set; get; }

        /// <summary>
        /// 餐食
        /// </summary>
        public List<string> Meals { set; get; }

        public FlightInfo()
        {
            Weeks = new List<string>();
            Meals = new List<string>();
        }
    }

}
