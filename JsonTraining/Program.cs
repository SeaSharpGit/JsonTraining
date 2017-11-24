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
            var model = new TestModel();
            Console.WriteLine("*********************************");
            Console.WriteLine("JsonTraining测试结果");
            var a = JsonHelper.ToJson(model);
            Console.WriteLine(a);
            Console.WriteLine("*********************************");
            Console.WriteLine("Json.NET测试结果");
            var b = JsonConvert.SerializeObject(model);
            Console.WriteLine(b);
            Console.WriteLine("*********************************");
            Console.WriteLine(a == b ? "相同" : "不相同");
            Console.WriteLine("*********************************");


            //var test = JsonConvert.DeserializeObject<int>(null);
            //var str = JsonHelper.ToJson(new TestModel());
            //Console.WriteLine(str);
            //Console.WriteLine("*********************************");
            ////var c = str.ToObject<TestModel>(str);
            //var d = JsonConvert.DeserializeObject<TestModel>(str);
            ////Console.WriteLine(c == d ? "相同" : "不相同");
            
            //Console.WriteLine(test);
            Console.ReadKey();
        }
    }
}
