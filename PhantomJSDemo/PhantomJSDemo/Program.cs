using Newtonsoft.Json;
using NReco.PhantomJS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhantomJSDemo
{
    class Program
    {
         
        static void Main(string[] args)
        {
            KillProcess();
            Console.WriteLine("Phantomjs killed, press any key and  go on....");
            Console.ReadLine();
            Action a = () =>
            { 
                Qyer qyer = new Qyer(); 
                var homeLinks = qyer.CrawlHome();
                Console.WriteLine("qyer.home.links.count:" + homeLinks.Count);  
                var groupHomeLinks = homeLinks.AvgGroup(2).ToList();
                Action<int> homeDealAct = (index) =>
                {
                    groupHomeLinks[index].ToList().ForEach((link) =>
                    {
                        Stopwatch dealStopWatch = new Stopwatch();
                        dealStopWatch.Start();
                        var res = qyer.CrawlDeal(link);
                        Console.WriteLine("qyer.deal.link:" + link);
                        Console.WriteLine(JsonConvert.SerializeObject(res));
                        Console.WriteLine("qyer.deal.time:" + dealStopWatch.ElapsedMilliseconds);
                        Console.WriteLine();
                    });
                };
                for (var index = 0; index < 2; index++)
                    homeDealAct.BeginInvoke(index, null, null);
               
                var activityLinks = qyer.CrawlActivity("http://z.qyer.com/zt/yqdrb/");
                var activityLinksAvgGroup = activityLinks.AvgGroup(2).ToList();
                Action<int> activityDealAct = (index) =>
                {
                    activityLinksAvgGroup[index].ToList().ForEach((link) =>
                    {
                        Stopwatch dealStopWatch = new Stopwatch();
                        dealStopWatch.Start();
                        Console.WriteLine("qyer.deal.link:" + link);
                        var res = qyer.CrawlDeal(link);
                        Console.WriteLine(JsonConvert.SerializeObject(res));
                        Console.WriteLine("qyer.deal.time:" + dealStopWatch.ElapsedMilliseconds);
                    });
                };
                for (var index = 0; index < 2; index++)
                    activityDealAct.BeginInvoke(index, null, null);
            };

            Action b = () =>
            {
                Mafengwo mafengwo = new Mafengwo();
                var homeLinks = mafengwo.CrawlHome();
                Console.WriteLine("mafengwo.home.links.count:" + homeLinks.Count);
                var homeLinksAvgGroup = homeLinks.AvgGroup(2).ToList();
                Action<int> homeDealAct = (index) =>
                {
                    homeLinksAvgGroup[index].ToList().ForEach((link) =>
                    {
                        Stopwatch dealStopWatch = new Stopwatch();
                        dealStopWatch.Start();
                        Console.WriteLine("mafengwo.deal.link:" + link);
                        var res = mafengwo.CrawlDeal(link);
                        Console.WriteLine(JsonConvert.SerializeObject(res));
                        Console.WriteLine("mafengwo.deal.time:" + dealStopWatch.ElapsedMilliseconds);
                    });
                };
                for (var index = 0; index < 2; index++)
                    homeDealAct.BeginInvoke(index, null, null);
                return;

                var activityLinks = mafengwo.CrawlActivity("http://www.mafengwo.cn/sales/activity/1000181.html");
                var activityLinksAvgGroup = activityLinks.AvgGroup(2).ToList();
                Action<int> activityDealAct = (index) =>
                {
                    activityLinksAvgGroup[index].ToList().ForEach((link) =>
                    {
                        Stopwatch dealStopWatch = new Stopwatch();
                        dealStopWatch.Start();
                        Console.WriteLine("mafengwo.deal.link:" + link);
                        var res = mafengwo.CrawlDeal(link);
                        Console.WriteLine(JsonConvert.SerializeObject(res));
                        Console.WriteLine("mafengwo.deal.time:" + dealStopWatch.ElapsedMilliseconds);
                    });
                };
                for (var index = 0; index < 2; index++)
                    activityDealAct.BeginInvoke(index, null, null);
            };


            a.BeginInvoke(null, null);
           // b.BeginInvoke(null, null);


            //act.BeginInvoke(null, null);
            //act.BeginInvoke(null, null);
            //act.BeginInvoke(null, null);
            //  for (var index = 0; index < 2; index++)
            //    act.BeginInvoke(null, null);             
            Console.ReadLine();
        }

        public static Action act = () =>
         {
             var phantomJS = new PhantomJS();
             phantomJS.OutputReceived += (sender, e) =>
             {
                 Console.WriteLine("PhantomJS output: {0}", e.Data);
             };
             var js = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "JavaScript1.js");
             var pageParams = new
             {
                 rootDir = AppDomain.CurrentDomain.BaseDirectory,
                 env = "pro",
                 jquery = "http://static.taolx.com/common/js/jquery-1.9.0.min.js"
             };
             var inputStr = JsonConvert.SerializeObject(pageParams);
             var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputStr + "\n"));
             using (var ops = new MemoryStream())
             {
                 phantomJS.RunScript(js, null, inputStream, ops);
                 var str = Encoding.UTF8.GetString(ops.ToArray());
                 Console.WriteLine(str);
                 Console.WriteLine(GetValue(str, "<`R>", "</~R>"));
                 Console.WriteLine();
                 Console.WriteLine();
             }
         };

        /// <summary>
        /// 获得字符串中开始和结束字符串中间得值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="s">开始</param>
        /// <param name="e">结束</param>
        /// <returns></returns> 
        public static string GetValue(string str, string s, string e)
        {
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }

        private static void KillProcess()
        {

            Process[] ps = Process.GetProcesses();
            foreach (Process item in ps)
            {
                Debug.WriteLine(item.ProcessName);
                if (item.ProcessName.ToLower()== "phantomjs")
                {
                    try
                    {
                        item.Kill();
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
