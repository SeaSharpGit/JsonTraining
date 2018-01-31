using JsonTraining.Helpers;
using JsonTraining.Models;
using Newtonsoft.Json;
using System;
using System.Data;

namespace JsonTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 序列化
            var model = new TestModel(1);
            Console.WriteLine("*********************************");
            Console.WriteLine("JsonTraining测试结果");
            var a = JsonHelper.ConvertToJson(model);
            Console.WriteLine(a);
            Console.WriteLine("*********************************");
            Console.WriteLine("Json.NET测试结果");
            var b = JsonConvert.SerializeObject(model);
            Console.WriteLine(b);
            Console.WriteLine("*********************************");
            Console.WriteLine(a == b ? "相同" : "不相同");
            Console.WriteLine("*********************************");
            #endregion


            #region 反序列化

            var model2 = new TestModel(1);
            var str = JsonHelper.ConvertToJson(model2);
            Console.WriteLine("字符串是：" + str);
            var c = JsonHelper.ConvertToJsonObject<TestModel>(str);
            var d = JsonConvert.DeserializeObject<TestModel>(str);
            #endregion


            Console.ReadKey();
        }
    }
}
