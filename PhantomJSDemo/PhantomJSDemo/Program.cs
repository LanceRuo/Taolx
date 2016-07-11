using Newtonsoft.Json;
using NReco.PhantomJS;
using System;
using System.Collections.Generic;
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

            Action act = () =>
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
            //act.BeginInvoke(null, null);
            //act.BeginInvoke(null, null);
            //act.BeginInvoke(null, null);
            for (var index = 0; index < 2; index++)
                act.BeginInvoke(null, null);
            Console.ReadLine();
        }



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
    }
}
