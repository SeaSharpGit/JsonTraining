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
        Int16 = 2,
        Int16Nullable = 3,
        UInt16 = 4,
        UInt16Nullable = 5,
        Int32 = 6,
        Int32Nullable = 7,
        UInt32 = 8,
        UInt32Nullable = 9,
        Int64 = 10,
        Int64Nullable = 11,
        UInt64 = 12,
        UInt64Nullable = 13,

        //浮点型
        Single = 14,
        SingleNullable = 15,
        Double = 16,
        DoubleNullable = 17,
        Decimal = 18,
        DecimalNullable = 19,

        //时间
        DateTime = 20,
        DateTimeNullable = 21,
        DateTimeOffset = 22,
        DateTimeOffsetNullable = 23,
        TimeSpan = 24,
        TimeSpanNullable = 25,

        //其他
        String = 26,
        Boolean = 27,
        BooleanNullable = 28,
        Char = 29,
        CharNullable = 30,
        SByte = 31,
        SByteNullable = 32,
        Byte = 33,
        ByteNullable = 34,
        Bytes = 35,
        Guid = 36,
        GuidNullable = 37,
        BigInteger = 38,
        BigIntegerNullable = 39,
        Uri = 40,
        DBNull = 41,
        DataTable=42,
        DataSet=43,
        Action=44,
        Func=45,

        //待定
        Struct = 46,
        Enum = 47,
    }
}
