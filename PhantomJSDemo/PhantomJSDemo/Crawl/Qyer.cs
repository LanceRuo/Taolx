using Newtonsoft.Json;
using NReco.PhantomJS;
using PhantomJSDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhantomJSDemo
{
    public class Qyer
    {
        /// <summary>
        /// js文件
        /// </summary>

        private Dictionary<JSFileType, string> _jsFilsDic = new Dictionary<JSFileType, string>();

        public Qyer()
        {

            foreach (JSFileType item in Enum.GetValues(typeof(JSFileType)))
            {
                var jqPath = Common.rootDir + item.GetDescription();
                _jsFilsDic[item] = File.ReadAllText(jqPath);
            }
        }

        /// <summary>
        /// Home首页
        /// </summary> 
        /// <returns></returns>
        public List<string> CrawlHome(int timeout = 30000)
        {
            var url = "http://z.qyer.com/";
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
        /// 活动专题
        /// </summary>
        /// <param name="url">http://z.qyer.com/zt/yqdrb/</param>
        /// <returns></returns>
        public List<string> CrawlActivity(string url, int timeout = 30000)
        {
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
                        phantomJSActivity.RunScript(_jsFilsDic[JSFileType.Activity], null, inputStream, ops);
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
        /// <param name="url">http://z.qyer.com/deal/72705/</param>
        /// <returns></returns>
        public Deal CrawlDeal(string url, int timeout = 30000)
        {
            try
            {
                var phantomJS = new PhantomJS();
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
        }

        private enum JSFileType
        {
            /// <summary>
            /// 首页推荐
            /// </summary>
            [Description("JavaScripts\\Crawl\\qyer\\Home.js")]
            Home,

            /// <summary>
            ///  活动页面
            /// </summary>
            [Description("JavaScripts\\Crawl\\qyer\\Activity.js")]
            Activity,

            /// <summary>
            /// 详情页
            /// </summary>
            [Description("JavaScripts\\Crawl\\qyer\\Deal.js")]
            Deal,


        }
    }
}
