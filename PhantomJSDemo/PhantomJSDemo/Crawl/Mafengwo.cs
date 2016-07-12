using Newtonsoft.Json;
using NReco.PhantomJS;
using PhantomJSDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhantomJSDemo
{
    public class Mafengwo
    {
        /// <summary>
        /// js文件
        /// </summary>

        private Dictionary<JSFileType, string> _jsFilsDic = new Dictionary<JSFileType, string>();

        //   private readonly PhantomJS phantomJS = new PhantomJS();
        public Mafengwo()
        {
            foreach (JSFileType item in Enum.GetValues(typeof(JSFileType)))
            {
                var jqPath = Common.rootDir + item.GetDescription();
                _jsFilsDic[item] = File.ReadAllText(jqPath);
            }
        }

        #region Crawl

        /// <summary>
        /// Home首页 http://www.mafengwo.cn/sales/
        /// </summary> 
        /// <returns></returns>
        public List<string> CrawlHome(int timeout = 30000)
        {
            var url = "http://www.mafengwo.cn/sales/";
            PhantomJS phantomJSActivity = new PhantomJS();
            try
            {
                phantomJSActivity.OutputReceived += (sender, e) =>
                {
                    Console.WriteLine("PhantomJS output: {0}", e.Data);
                };
                var pageParams = new
                {
                    rootDir = Common.rootDir,
                    isDebug = Common.isDebug,
                    jquery = Common.jquery,
                    url = url
                };
                var inputStr = JsonConvert.SerializeObject(pageParams);
                var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputStr + "\n"));
                string resultJson = null;
                Action doAct = () =>
                {
                    using (var ops = new MemoryStream())
                    {
                        phantomJSActivity.RunScript(_jsFilsDic[JSFileType.Home], null, inputStream, ops);
                        var str = Encoding.UTF8.GetString(ops.ToArray());
                        resultJson = Common.GetValue(str, "<`R>", "</~R>");
                        if (Common.isDebug)
                            Console.WriteLine(str);
                    }
                };
                doAct.BeginInvoke(null, null);
                var time = 0;
                while (time < timeout && resultJson == null)
                {
                    Thread.Sleep(100);
                    time += 100;
                }
                if (string.IsNullOrEmpty(resultJson))
                    return new List<string>();
                return JsonConvert.DeserializeObject<List<string>>(resultJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>();
            }
            finally
            {
                phantomJSActivity.Abort();
            }
        }

        /// <summary>
        /// 获取详情页信息
        /// </summary>
        /// <param name="url">http://www.mafengwo.cn/sales/374185.html</param>
        public Deal CrawlDeal(string url, int timeout = 30000)
        {
            var phantomJS = new PhantomJS();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                phantomJS.OutputReceived += (sender, e) =>
                {
                    Console.WriteLine("PhantomJS output: {0}", e.Data);
                };
                var pageParams = new
                {
                    rootDir = Common.rootDir,
                    isDebug = Common.isDebug,
                    jquery = Common.jquery,
                    url = url
                };
                var inputStr = JsonConvert.SerializeObject(pageParams);
                var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputStr + "\n"));
                string resultJson = null;
                Action doAct = () =>
                {
                    using (var ops = new MemoryStream())
                    {
                        phantomJS.RunScript(_jsFilsDic[JSFileType.Deal], null, inputStream, ops);
                        var str = Encoding.UTF8.GetString(ops.ToArray());
                        resultJson = Common.GetValue(str, "<`R>", "</~R>");
                        if (Common.isDebug)
                            Console.WriteLine(str);
                    }
                };
                doAct.BeginInvoke(null, null);
                var time = 0;
                while (time < timeout && resultJson == null)
                {
                    Thread.Sleep(100);
                    time += 100;
                }
                if (string.IsNullOrEmpty(resultJson))
                    return null;
                return JsonConvert.DeserializeObject<Deal>(resultJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                phantomJS.Abort();
                stopWatch.Stop();
                Console.WriteLine("mafengwo.active.time:" + stopWatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// 活动专题
        /// </summary>
        /// <param name="url">http://www.mafengwo.cn/sales/activity/1000181.html</param>
        /// <returns></returns>
        public List<string> CrawlActivity(string url, int timeout = 30000)
        {
            var phantomJS = new PhantomJS();
            try
            {
                phantomJS.OutputReceived += (sender, e) =>
                {
                    Console.WriteLine("PhantomJS output: {0}", e.Data);
                };
                var pageParams = new
                {
                    rootDir = Common.rootDir,
                    isDebug = Common.isDebug,
                    jquery = Common.jquery,
                    url = url
                };
                var inputStr = JsonConvert.SerializeObject(pageParams);
                var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputStr + "\n"));
                string resultJson = null;
                Action doAct = () =>
                {
                    using (var ops = new MemoryStream())
                    {
                        phantomJS.RunScript(_jsFilsDic[JSFileType.Activity], null, inputStream, ops);
                        var str = Encoding.UTF8.GetString(ops.ToArray());
                        resultJson = Common.GetValue(str, "<`R>", "</~R>");
                        if (Common.isDebug)
                            Console.WriteLine(str);
                    }
                };
                doAct.BeginInvoke(null, null);
                var time = 0;
                while (time < timeout && resultJson == null)
                {
                    Thread.Sleep(100);
                    time += 100;
                }
                if (string.IsNullOrEmpty(resultJson))
                    return new List<string>();
                return JsonConvert.DeserializeObject<List<string>>(resultJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>();
            }
            finally
            {
                phantomJS.Abort();
            }
        }


        #endregion


        private enum JSFileType
        {
            /// <summary>
            /// 首页推荐
            /// </summary>
            [Description("JavaScripts\\Crawl\\mafengwo\\Home.js")]
            Home,


            /// <summary>
            /// 活动专题
            /// </summary>
            [Description("JavaScripts\\Crawl\\mafengwo\\Activity.js")]
            Activity,


            /// <summary>
            /// 详情
            /// </summary>
            [Description("JavaScripts\\Crawl\\mafengwo\\Deal.js")]
            Deal,


        }
    }
}
