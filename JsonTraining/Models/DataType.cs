using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTraining.Models
{
    public enum DataType
    {
        Empty = 0,
        Object = 1,

        //整形
        SByte = 2,
        SByteNullable = 3,
        Byte = 4,
        ByteNullable = 5,
        Int16 = 6,
        Int16Nullable = 7,
        UInt16 = 8,
        UInt16Nullable = 9,
        Int32 = 10,
        Int32Nullable = 11,
        UInt32 = 12,
        UInt32Nullable = 13,
        Int64 = 14,
        Int64Nullable = 15,
        UInt64 = 16,
        UInt64Nullable = 17,

        //浮点型
        Single = 18,
        SingleNullable = 19,
        Double = 20,
        DoubleNullable = 21,
        Decimal = 22,
        DecimalNullable = 23,

        //时间
        DateTime = 24,
        DateTimeNullable = 25,
        DateTimeOffset = 26,
        DateTimeOffsetNullable = 27,
        TimeSpan = 28,
        TimeSpanNullable = 29,

        //集合类型
        DataTable = 30,
        DataSet = 31,
        IEnumerable = 32,

        //其他
        String = 33,
        Boolean = 34,
        BooleanNullable = 35,
        Char = 36,
        CharNullable = 37,



        

        Guid = 38,
        GuidNullable = 39,
        BigInteger = 40,
        BigIntegerNullable = 41,
        Uri = 42,
        DBNull = 43,
        
        Action=44,
        Func=45,

        //待定
        Struct = 46,
        Enum = 47,
    }
}
