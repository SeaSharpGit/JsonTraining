using System;

namespace JsonTraining.Models
{
    public class TestModel
    {
        #region 值类型
        public short ShortTest { get; set; } = 123;
        public ushort UShortTest { get; set; } = 123;

        public int IntTest { get; set; } = 123;
        public uint UIntTest { get; set; } = 123U;

        public long LongTest { get; set; } = 123L;
        public ulong ULongTest { get; set; } = 123UL;

        public double DoubleTest { get; set; } = 1.23;
        public float FloatTest { get; set; } = 1.23F;
        public decimal DecimalTest { get; set; } = 1.23M;

        public DateTime DateTimeTest { get; set; } = new DateTime(2000, 1, 1);


        public bool BoolTest { get; set; } = true;
        public char CharTest { get; set; } = '1';
        public byte ByteTest { get; set; } = 5;
        public Enum EnumTest { get; set; } = Enum.Second;
        public Struct StructTest { get; set; } = new Struct() { Name = "名字", Age = 123 };
        #endregion

        #region 引用类型
        public string StringTest { get; set; } = "字符串";
        public object ObjectTest { get; set; } = new { Name = "对象" }; 
        #endregion
    }

    public struct Struct
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public enum Enum
    {
        First,
        Second
    }
}