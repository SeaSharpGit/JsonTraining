﻿using JsonTraining.Helpers;
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
            var a = JsonHelper.ToJson(model);
            Console.WriteLine(a);
            Console.WriteLine("*********************************");
            Console.WriteLine("Json.NET测试结果");
            var b = JsonConvert.SerializeObject(model);
            Console.WriteLine(b);
            Console.WriteLine("*********************************");
            Console.WriteLine(a == b ? "相同" : "不相同");
            Console.ReadKey();
        }
    }
}
