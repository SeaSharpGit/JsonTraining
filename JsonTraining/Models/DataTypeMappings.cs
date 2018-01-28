using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTraining.Models
{
    public class DataTypeMappings
    {
        private static Dictionary<Type, DataType> _Mappings = new Dictionary<Type, DataType>()
        {
            {typeof(object),DataType.Object },
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


        };

        public static DataType GetDataType(Type type)
        {
            if (_Mappings.TryGetValue(type, out DataType dataType))
            {
                return dataType;
            }
            else
            {
                return DataType.Empty;
            }

        }
    }
}
