using JsonTraining.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsonTraining.Helpers
{
    public static partial class JsonHelper
    {
        public static string ConvertToJson(object obj)
        {
            var sb = new StringBuilder();
            AppendString(obj, ref sb);
            return sb.ToString();
        }

        private static void AppendString(object obj, ref StringBuilder sb)
        {
            if (obj == null)
            {
                sb.Append("null");
                return;
            }
            var type = obj.GetType();
            var dataType = DataTypeMappings.GetDataType(type);
            switch (dataType)
            {
                case DataType.SByte:
                case DataType.SByteNullable:
                case DataType.Byte:
                case DataType.ByteNullable:
                case DataType.Int16:
                case DataType.Int16Nullable:
                case DataType.UInt16:
                case DataType.UInt16Nullable:
                case DataType.Int32:
                case DataType.Int32Nullable:
                case DataType.UInt32:
                case DataType.UInt32Nullable:
                case DataType.Int64:
                case DataType.Int64Nullable:
                case DataType.UInt64:
                case DataType.UInt64Nullable:
                case DataType.Double:
                case DataType.DoubleNullable:
                case DataType.Single:
                case DataType.SingleNullable:
                case DataType.Decimal:
                case DataType.DecimalNullable:
                case DataType.BigInteger:
                case DataType.BigIntegerNullable:
                    sb.Append(obj);
                    break;
                case DataType.Boolean:
                case DataType.BooleanNullable:
                    BoolToJson(obj, ref sb);
                    break;
                case DataType.Char:
                case DataType.CharNullable:
                    CharToJson(obj, ref sb);
                    break;
                case DataType.DateTime:
                case DataType.DateTimeNullable:
                    DateTimeToJson(obj, ref sb);
                    break;
                case DataType.DateTimeOffset:
                case DataType.DateTimeOffsetNullable:
                    DateTimeOffsetToJson(obj, ref sb);
                    break;
                case DataType.TimeSpan:
                case DataType.TimeSpanNullable:
                    TimeSpanToJson(obj, ref sb);
                    break;
                case DataType.String:
                    StringToJson(obj, ref sb);
                    break;
                case DataType.Guid:
                case DataType.GuidNullable:
                    GuidToJson(obj, ref sb);
                    break;
                case DataType.DataTable:
                    DataTableToJson(obj as DataTable, ref sb);
                    break;
                case DataType.DataSet:
                    DataSetToJson(obj as DataSet, ref sb);
                    break;
                case DataType.IEnumerable:
                    IEnumerableToJson(obj, ref sb);
                    break;
                case DataType.Uri:
                    UriToJson(obj as Uri, ref sb);
                    break;
                case DataType.Enum:
                    EnumToJson(obj, ref sb);
                    break;
                case DataType.Object:
                case DataType.Empty:
                    ObjectToJson(obj, ref sb);
                    break;
            }
        }

        #region EnumToJson
        private static void EnumToJson(object obj, ref StringBuilder sb)
        {
            sb.Append((int)obj);
        }
        #endregion

        #region UriToJson
        private static void UriToJson(Uri uri, ref StringBuilder sb)
        {
            sb.Append("\"");
            sb.Append(uri.OriginalString);
            sb.Append("\"");
        }
        #endregion

        #region GuidToJson
        private static void GuidToJson(object obj, ref StringBuilder sb)
        {
            sb.Append("\"");
            sb.Append(obj);
            sb.Append("\"");
        }
        #endregion

        #region BoolToJson
        private static void BoolToJson(object obj, ref StringBuilder sb)
        {
            sb.Append(obj.ToString().ToLower());
        }
        #endregion

        #region CharToJson
        private static void CharToJson(object obj, ref StringBuilder sb)
        {
            sb.Append("\"");
            sb.Append(obj.ToString() == "\0" ? "\\u0000" : obj.ToString());
            sb.Append("\"");
        }
        #endregion

        #region StringToJson
        private static void StringToJson(object obj, ref StringBuilder sb)
        {
            sb.Append("\"");
            //做一个处理，作为反序列化时判断依据
            var str = obj.ToString().Replace("\"", "\\\"");
            sb.Append(str);
            sb.Append("\"");
        }
        #endregion

        #region TimeSpanToJson
        private static void TimeSpanToJson(object obj, ref StringBuilder sb)
        {
            if (obj is TimeSpan ts)
            {
                sb.Append("\"");
                sb.Append(ts.ToString());
                sb.Append("\"");
            }
        }
        #endregion

        #region DateTimeOffsetToJson
        private static void DateTimeOffsetToJson(object obj, ref StringBuilder sb)
        {
            if (obj is DateTimeOffset off)
            {
                sb.Append("\"");
                sb.Append(off.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                sb.Append("\"");
            }
        }
        #endregion

        #region DateTimeToJson
        private static void DateTimeToJson(object obj, ref StringBuilder sb)
        {
            if (obj is DateTime time)
            {
                sb.Append("\"");
                sb.Append(time.ToString("s"));
                sb.Append("\"");
            }
        }
        #endregion

        #region ObjectToJson
        private static void ObjectToJson(object obj, ref StringBuilder sb)
        {
            var type = obj.GetType();
            sb.Append("{");
            var fields = type.GetFields();
            var propertys = type.GetProperties();

            foreach (var item in fields)
            {
                if (item.IsPublic && !item.IsStatic)
                {
                    sb.Append("\"");
                    sb.Append(item.Name);
                    sb.Append("\":");
                    var value = item.GetValue(obj);
                    AppendString(value, ref sb);
                    sb.Append(",");
                }
            }

            foreach (var item in propertys)
            {
                sb.Append("\"");
                sb.Append(item.Name);
                sb.Append("\":");
                var value = item.GetValue(obj);
                AppendString(value, ref sb);
                sb.Append(",");
            }

            if (propertys.Count() > 0 || fields.Count() > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}");
        }
        #endregion

        #region DataTableToJson
        private static void DataTableToJson(DataTable dt, ref StringBuilder sb)
        {
            sb.Append("[");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    sb.Append("{");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sb.Append("\"");
                        sb.Append(dt.Columns[i].ColumnName);
                        sb.Append("\":");
                        AppendString(row[i], ref sb);
                        sb.Append(",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
        }
        #endregion

        #region DataSetToJson
        private static void DataSetToJson(DataSet ds, ref StringBuilder sb)
        {
            sb.Append("{");
            var tables = ds.Tables;
            foreach (DataTable item in tables)
            {
                sb.Append("\"");
                sb.Append(item.TableName);
                sb.Append("\":");
                DataTableToJson(item, ref sb);
                sb.Append(",");
            }
            if (tables.Count > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}");
        }
        #endregion

        #region IEnumerableToJson
        private static void IEnumerableToJson(object items, ref StringBuilder sb)
        {
            var type = items.GetType();
            //判断Dictionary
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                dynamic dic = items;
                sb.Append("{");
                foreach (var item in dic)
                {
                    sb.Append("\"");
                    AppendString(item.Key);
                    sb.Append("\"");
                }
                sb.Append("}");
            }
            else
            {
                sb.Append("[");
                var flag = false;
                foreach (var i in items as IEnumerable)
                {
                    AppendString(i, ref sb);
                    sb.Append(",");
                    flag = true;
                }
                if (flag)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
            }
        }
        #endregion
    }
}