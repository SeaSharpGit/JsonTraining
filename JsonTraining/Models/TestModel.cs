using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace JsonTraining.Models
{
    public class TestModel
    {
        public TestModel()
        {
            //var dt = new DataTable();
            //dt.Columns.Add("Column0");
            //dt.Columns.Add("Column1");
            //dt.Rows.Add(1, 2);
            //DataTableTest = dt;

            //var dt2 = new DataTable("MyDataTable");
            //dt2.Columns.Add("Column0");
            //dt2.Columns.Add("Column1");
            //dt2.Rows.Add(0, 1);
            //DataSetTest.Tables.Add(dt2);
        }

        #region 变量
        public int FiledTest = 1;
        public Action action = () =>
        {
            Debug.WriteLine(123);
        };
        #endregion
        

        //#region DataTable/DataSet
        //public DataTable DataTableTest { get; set; }
        //public DataSet DataSetTest { get; set; } = new DataSet();
        //#endregion




        //#region 引用类型
        //public string StringTest { get; set; } = "字符串";
        //public object ObjectTest { get; set; } = new { Name = "对象" };

        //public List<Struct> ListTest { get; set; } = new List<Struct>() { new Struct(), new Struct() };
        //public ArrayList ArrayListTest { get; set; } = new ArrayList() { 1, "2", 3 };
        //public int[] ArrayTest { get; set; } = new int[] { 1, 2, 3 }; 
        //#endregion

        //#region 基础值类型
        //public short ShortTest { get; set; } = 123;
        //public ushort UShortTest { get; set; } = 123;

        //public int IntTest { get; set; } = 123;
        //public uint UIntTest { get; set; } = 123U;

        //public long LongTest { get; set; } = 123L;
        //public ulong ULongTest { get; set; } = 123UL;

        //public double DoubleTest { get; set; } = 1.23;
        //public float FloatTest { get; set; } = 1.23F;
        //public decimal DecimalTest { get; set; } = 1.23M;

        //public DateTime DateTimeTest { get; set; } = new DateTime(2000, 1, 1);
        //public bool BoolTest { get; set; } = true;
        //public char CharTest { get; set; } = '1';
        //public byte ByteTest { get; set; } = 5;
        //public Enum EnumTest { get; set; } = Enum.Second;
        //public Struct StructTest { get; set; }
        //#endregion

        //#region 可空类型Nullable
        //public int? IntNullableTest { get; set; } = null;
        //public int? IntNullableTest2 { get; set; } = 123;
        //#endregion
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