using JsonTraining.Helpers;
using JsonTraining.Models;
using Newtonsoft.Json;
using System;

namespace JsonTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new TestModel();
            Console.WriteLine("*********************************");
            Console.WriteLine("JsonTraining测试结果");
            Console.WriteLine(JsonHelper.ToJson(model));
            Console.WriteLine("*********************************");
            Console.WriteLine("Json.NET测试结果");
            Console.WriteLine(JsonConvert.SerializeObject(model));
            Console.WriteLine("*********************************");
            Console.ReadKey();
        }
    }
}
