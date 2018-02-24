using JsonTraining.Helpers;
using JsonTraining.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace JsonTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            //var obj5 = new { Name = "123", Age = 456 };
            //var strrr= JsonHelper.ConvertToJson(obj5);
            //var objjj= JsonConvert.DeserializeObject<object>(strrr);

            #region 序列化
            var model = new TestModel(1);
            Console.WriteLine("*************JsonTraining测试结果**************");
            Console.WriteLine("");
            var a = JsonHelper.ConvertToJson(model);
            Console.WriteLine(a);
            Console.WriteLine("*************Json.NET测试结果***********");
            Console.WriteLine("");
            var b = JsonConvert.SerializeObject(model);
            Console.WriteLine(b);
            Console.WriteLine("*********************************");
            Console.WriteLine(a == b ? "相同" : "不相同");
            #endregion


            #region 反序列化
            var str = JsonHelper.ConvertToJson(model);
            var c = JsonHelper.ConvertToJsonObject<TestModel>(str);
            var d = JsonConvert.DeserializeObject<TestModel>(str);
            #endregion

            #region 性能测试
            Console.WriteLine("**************性能测试************");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 10000; i++)
            {
                var json = JsonHelper.ConvertToJson(model);
            }
            watch.Stop();
            Console.WriteLine("JsonTraining序列化10000个对象耗时：" + watch.ElapsedMilliseconds + "毫秒");


            watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 10000; i++)
            {
                var json = JsonConvert.SerializeObject(model);
            }
            watch.Stop();
            Console.WriteLine("Json.NET序列化10000个对象耗时：" + watch.ElapsedMilliseconds + "毫秒");


            watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 10000; i++)
            {
                var obj = JsonHelper.ConvertToJsonObject<TestModel>(str);
            }
            watch.Stop();
            Console.WriteLine("JsonTraining反序列化10000个对象耗时：" + watch.ElapsedMilliseconds + "毫秒");

            watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 10000; i++)
            {
                var obj = JsonConvert.DeserializeObject<TestModel>(str);
            }
            watch.Stop();
            Console.WriteLine("Json.NET反序列化10000个对象耗时：" + watch.ElapsedMilliseconds + "毫秒");
            #endregion


            Console.ReadKey();
        }
    }
}
