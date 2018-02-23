using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Numerics;

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
            //SByte = 123;
            //SByteNullable = 123;
            //Byte = 123;
            //ByteNullable = 123;
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
            //DateTime = new DateTime(2000, 1, 1);
            //DateTimeNullable = new DateTime(2000, 1, 1);
            //DateTimeOffset = new DateTimeOffset(new DateTime(2000, 1, 1));
            //DateTimeOffset = new DateTimeOffset(new DateTime(2000, 1, 1));
            //DateTimeOffsetNullable = new DateTimeOffset(new DateTime(2000, 1, 1));
            //TimeSpan = new TimeSpan(1, 1, 1, 1);
            //TimeSpanNullable = new TimeSpan(1, 1, 1, 1);
            #endregion

            #region 变量
            //Filed = 1;
            #endregion

            #region 集合类型
            //List = new List<int>() { 1, 2, 3 };
            //List2 = new List<string>() { "1", "2", "3" };
            //Array = new int[] { 1, 2, 3 };
            //Array2 = new string[] { "1", "2", "3" };
            //ArrayList = new ArrayList() { 1, "2", true };
            Dictionary = new Dictionary<int, string>() { { 1, "2" }, { 3, "4" } };

            //var dt = new DataTable();
            //dt.Columns.Add("Column0",typeof(bool));
            //dt.Columns.Add("Column1");
            //dt.Rows.Add(true, 2);
            //dt.Rows.Add(false, 4);
            //DataTable = dt;

            //DataSet = new DataSet();
            //var dt2 = new DataTable("MyDataTable");
            //dt2.Columns.Add("Column0");
            //dt2.Columns.Add("Column1");
            //dt2.Rows.Add(1, 2);
            //dt2.Rows.Add(3, 4);
            //DataSet.Tables.Add(dt2);
            //var dt3 = new DataTable("MyDataTable2");
            //dt3.Columns.Add("Column0");
            //dt3.Columns.Add("Column1");
            //dt3.Rows.Add(5, 6);
            //dt3.Rows.Add(7, 8);
            //DataSet.Tables.Add(dt3);
            #endregion

            #region 其他类型
            //String = "字符串{[\"";
            //Object = new { Name = "对象", Age = 20 };
            //Bool = true;
            //BoolNullable = true;
            //Char = '1';
            //CharNullable = '1';
            //Guid = new Guid();
            //GuidNullable = new Guid();
            //BigInteger = new BigInteger(1);
            //BigIntegerNullable = new BigInteger(1);
            //Uri = new Uri("https://www.baidu.com");
            //Enum = EnumModel.Second;
            //Struct = new StructModel() { Age = 1, Name = "2" };
            #endregion

        }

        #region 整形
        //public sbyte SByte { get; set; }
        //public sbyte SByteNullable { get; set; }
        //public byte Byte { get; set; }
        //public byte ByteNullable { get; set; }
        //public short Int16 { get; set; }
        //public short? Int16Nullable { get; set; }
        //public ushort UInt16 { get; set; }
        //public ushort? UInt16Nullable { get; set; }
        //public int Int32 { get; set; }
        //public int? Int32Nullable { get; set; }
        //public uint UInt32 { get; set; }
        //public uint? UInt32Nullable { get; set; }
        //public long Int64 { get; set; }
        //public long? Int64Nullable { get; set; }
        //public ulong UInt64 { get; set; }
        //public ulong? UInt64Nullable { get; set; }
        #endregion

        #region 浮点型
        //public double Double { get; set; }
        //public double? DoubleNullable { get; set; }
        //public float Float { get; set; }
        //public float? FloatNullable { get; set; }
        //public decimal Decimal { get; set; }
        //public decimal? DecimalNullable { get; set; }
        #endregion

        #region 时间类型
        //public DateTime DateTime { get; set; }
        //public DateTime? DateTimeNullable { get; set; }
        //public DateTimeOffset DateTimeOffset { get; set; }
        //public DateTimeOffset? DateTimeOffsetNullable { get; set; }
        //public TimeSpan TimeSpan { get; set; }
        //public TimeSpan? TimeSpanNullable { get; set; }
        #endregion

        #region 变量
        //public int Filed;
        #endregion

        #region 集合类型类型
        //public List<int> List { get; set; }
        //public List<string> List2 { get; set; }
        //public int[] Array { get; set; }
        //public string[] Array2 { get; set; }
        //public ArrayList ArrayList { get; set; } = new ArrayList();
        public Dictionary<int, string> Dictionary { get; set; }

        //public DataTable DataTable { get; set; }
        //public DataSet DataSet { get; set; }
        #endregion

        #region 其他类型
        //public string String { get; set; }
        //public object Object { get; set; }
        //public bool Bool { get; set; }
        //public bool? BoolNullable { get; set; }
        //public char Char { get; set; }
        //public char? CharNullable { get; set; }
        //public Guid Guid { get; set; }
        //public Guid? GuidNullable { get; set; }
        //public BigInteger BigInteger { get; set; }
        //public BigInteger? BigIntegerNullable { get; set; }
        //public Uri Uri { get; set; }
        //public EnumModel Enum { get; set; } = EnumModel.Second;
        //public StructModel Struct { get; set; }
        #endregion

    }

    public enum EnumModel
    {
        First,
        Second
    }
}