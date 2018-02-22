using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace JsonTraining.Models
{
    public class DataTypeMappings
    {
        private static Dictionary<Type, DataType> _Mappings = new Dictionary<Type, DataType>()
        {
            {typeof(object),DataType.Object },
            {typeof(sbyte),DataType.SByte },
            {typeof(sbyte?),DataType.SByteNullable },
            {typeof(byte),DataType.Byte },
            {typeof(byte?),DataType.ByteNullable },
            {typeof(short),DataType.Int16 },
            {typeof(short?),DataType.Int16Nullable },
            {typeof(ushort),DataType.UInt16 },
            {typeof(ushort?),DataType.UInt16Nullable },
            {typeof(int),DataType.Int32 },
            {typeof(int?),DataType.Int32Nullable },
            {typeof(uint),DataType.UInt32 },
            {typeof(uint?),DataType.UInt32Nullable },
            {typeof(long),DataType.Int64 },
            {typeof(long?),DataType.Int64Nullable },
            {typeof(ulong),DataType.UInt64 },
            {typeof(ulong?),DataType.UInt64Nullable },
            {typeof(double),DataType.Double },
            {typeof(double?),DataType.DoubleNullable },
            {typeof(float),DataType.Single },
            {typeof(float?),DataType.SingleNullable },
            {typeof(decimal),DataType.Decimal },
            {typeof(decimal?),DataType.DecimalNullable },
            {typeof(DateTime),DataType.DateTime },
            {typeof(DateTime?),DataType.DateTimeNullable },
            {typeof(DateTimeOffset),DataType.DateTimeOffset },
            {typeof(DateTimeOffset?),DataType.DateTimeOffsetNullable },
            {typeof(TimeSpan),DataType.TimeSpan },
            {typeof(TimeSpan?),DataType.TimeSpanNullable },
            {typeof(string),DataType.String },
            {typeof(bool),DataType.Boolean },
            {typeof(bool?),DataType.BooleanNullable },
            {typeof(char),DataType.Char },
            {typeof(char?),DataType.CharNullable },
            {typeof(DataTable),DataType.DataTable },
            {typeof(DataSet),DataType.DataSet },
            {typeof(Guid),DataType.Guid },
            {typeof(Guid?),DataType.GuidNullable },
            {typeof(BigInteger),DataType.BigInteger },
            {typeof(BigInteger?),DataType.BigIntegerNullable },
            {typeof(Uri),DataType.Uri }
        };

        public static DataType GetDataType(Type type)
        {
            if (_Mappings.TryGetValue(type, out DataType dataType))
            {
                return dataType;
            }
            else if (type.IsEnum)
            {
                return DataType.Enum;
            }
            else if (type.GetInterfaces().Count(i => i.Name == "IEnumerable") > 0)
            {
                return DataType.IEnumerable;
            }
            else
            {
                return DataType.Empty;
            }

        }
    }
}
