using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace JsonTraining.Models
{
    public class TestModel
    {
        public TestModel(int parameter = 0)
        {
            if (parameter == 1)
            {
                ResetProperties();
            }
        }

        public TestModel()
        {

        }

        private void ResetProperties()
        {
            #region 整形
            //Int16 = 123;
            //Int16Nullable = 123;
            //UInt16 = 123;
            //UInt16Nullable = 123;
            //Int32 = 123;
            //Int32Nullable = 123;
            //UInt32 = 123U;
            //UInt32Nullable = 123U;
            //Int64 = 123L;
            //Int64Nullable = 123L;
            //UInt64 = 123UL;
            //UInt64Nullable = 123UL; 
            #endregion

            #region 浮点型
            //Double = 1.23;
            //DoubleNullable = 1.23;
            //Float = 1.23F;
            //FloatNullable = 1.23F;
            //Decimal = 1.23M;
            //DecimalNullable = 1.23M; 
            #endregion

            #region 时间类型
            DateTime = new DateTime(2000, 1, 1);
            DateTimeNullable = new DateTime(2000, 1, 1);
            DateTimeOffset = new DateTimeOffset(new DateTime(2000, 1, 1));
            DateTimeOffset = new DateTimeOffset(new DateTime(2000, 1, 1));
            DateTimeOffsetNullable = new DateTimeOffset(new DateTime(2000, 1, 1));
            TimeSpan = new TimeSpan(1,1,1,1);
            TimeSpanNullable = new TimeSpan(1,1,1,1);
            #endregion

            //var dt = new DataTable();
            //dt.Columns.Add("Column0");
            //dt.Columns.Add("Column1");
            //dt.Rows.Add(1, 2);
            //dt.Rows.Add(3, 4);
            //DataTableTest = dt;

            //var dt2 = new DataTable("MyDataTable");
            //dt2.Columns.Add("Column0");
            //dt2.Columns.Add("Column1");
            //dt2.Rows.Add(0, 1);
            //DataSetTest.Tables.Add(dt2);
        }

        #region 整形
        //public short Int16 { get; set; }
        //public short? Int16Nullable { get; set; }
        //public short? Int16NullableNull { get; set; }
        //public ushort UInt16 { get; set; }
        //public ushort? UInt16Nullable { get; set; }
        //public ushort? UInt16NullableNull { get; set; }
        //public int Int32 { get; set; }
        //public int? Int32Nullable { get; set; }
        //public int? Int32NullableNull { get; set; }
        //public uint UInt32 { get; set; }
        //public uint? UInt32Nullable { get; set; }
        //public uint? UInt32NullableNull { get; set; }
        //public long Int64 { get; set; }
        //public long? Int64Nullable { get; set; }
        //public long? Int64NullableNull { get; set; }
        //public ulong UInt64 { get; set; }
        //public ulong? UInt64Nullable { get; set; }
        //public ulong? UInt64NullableNull { get; set; }
        #endregion

        #region 浮点型
        //public double Double { get; set; }
        //public double? DoubleNullable { get; set; }
        //public double? DoubleNullableNull { get; set; }
        //public float Float { get; set; }
        //public float? FloatNullable { get; set; }
        //public float? FloatNullableNull { get; set; }
        //public decimal Decimal { get; set; }
        //public decimal? DecimalNullable { get; set; }
        //public decimal? DecimalNullableNull { get; set; }
        #endregion

        #region 时间类型
        public DateTime DateTime { get; set; }
        public DateTime? DateTimeNullable { get; set; }
        public DateTime? DateTimeNullableNull { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
        public DateTimeOffset? DateTimeOffsetNullable { get; set; }
        public DateTimeOffset? DateTimeOffsetNullableNull { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public TimeSpan? TimeSpanNullable { get; set; }
        public TimeSpan? TimeSpanNullableNull { get; set; }
        #endregion

        #region 变量
        //public int FiledTest = 1;
        //public StructModel StructFiledTest;
        //public Action action = () =>
        //{

        //};
        //public Func<int> func = () =>
        //{
        //    return 1;
        //};
        #endregion


        //public DateTime DateTimeTest { get; set; } = new DateTime(2000, 1, 1);
        //public bool BoolTest { get; set; } = true;
        //public char CharTest { get; set; } = '5';
        //public byte ByteTest { get; set; } = 5;
        //public EnumModel EnumTest { get; set; } = EnumModel.Second;
        //public StructModel StructTest { get; set; }

        #region DataTable/DataSet
        //public DataTable DataTableTest { get; set; }
        //public DataSet DataSetTest { get; set; } = new DataSet();
        #endregion

        #region 基础引用类型
        //public string StringTest { get; set; } = "字符串{[\"";
        //public object ObjectTest { get; set; } = new { Name = "对象" };

        //public List<StructModel> ListTest { get; set; } = new List<StructModel>() { new StructModel(), new StructModel() };
        //public ArrayList ArrayListTest { get; set; } = new ArrayList() { 1, "2", 3 };
        //public int[] ArrayTest { get; set; } = new int[] { 1, 2, 3 };
        #endregion

    }

    public struct StructModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public enum EnumModel
    {
        First,
        Second
    }
}