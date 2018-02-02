using JsonTraining.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace JsonTraining.Helpers
{
    public static partial class JsonHelper
    {
        public static T ConvertToJsonObject<T>(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }
            var type = typeof(T);
            return (T)CreateObject(type, str);
        }

        private static object CreateObject(Type type, string str)
        {
            if (str == "null")
            {
                return null;
            }
            var dataType = DataTypeMappings.GetDataType(type);
            switch (dataType)
            {
                case DataType.SByte:
                case DataType.SByteNullable:
                    return sbyte.Parse(str);
                case DataType.Byte:
                case DataType.ByteNullable:
                    return byte.Parse(str);
                case DataType.Int16:
                case DataType.Int16Nullable:
                    return short.Parse(str);
                case DataType.UInt16:
                case DataType.UInt16Nullable:
                    return ushort.Parse(str);
                case DataType.Int32:
                case DataType.Int32Nullable:
                    return int.Parse(str);
                case DataType.UInt32:
                case DataType.UInt32Nullable:
                    return uint.Parse(str);
                case DataType.Int64:
                case DataType.Int64Nullable:
                    return long.Parse(str);
                case DataType.UInt64:
                case DataType.UInt64Nullable:
                    return ulong.Parse(str);
                case DataType.Double:
                case DataType.DoubleNullable:
                    return double.Parse(str);
                case DataType.Single:
                case DataType.SingleNullable:
                    return float.Parse(str);
                case DataType.Decimal:
                case DataType.DecimalNullable:
                    return decimal.Parse(str);
                case DataType.DateTime:
                case DataType.DateTimeNullable:
                    return JsonToDateTime(str);
                case DataType.DateTimeOffset:
                case DataType.DateTimeOffsetNullable:
                    return JsonToDateTimeOffset(str);
                case DataType.TimeSpan:
                case DataType.TimeSpanNullable:
                    return JsonToTimeSpan(str);
                case DataType.String:
                    return JsonToString(str);
                case DataType.Boolean:
                case DataType.BooleanNullable:
                    return bool.Parse(str);
                case DataType.Char:
                case DataType.CharNullable:
                    return JsonToChar(str);
                case DataType.Guid:
                case DataType.GuidNullable:
                    return JsonToGuid(str);
                case DataType.BigInteger:
                case DataType.BigIntegerNullable:
                    return BigInteger.Parse(str);
                case DataType.DataTable:
                    return JsonToDataTable(str);
                case DataType.DataSet:
                    return JsonToDataSet(str);
                case DataType.IEnumerable:
                    return JsonToIEnumerable(type, str);
                case DataType.Uri:
                    return JsonToUri(str);
                case DataType.Enum:
                    return JsonToEnum(type, str);
                case DataType.Object:
                    return JsonToObject(type, str);
                case DataType.Empty:
                    return JsonToOther(type, str);
                default:
                    return null;
            }

            //    switch (fullName)
            //    {
            //        case "System.Action":
            //            //委托暂不支持
            //            break;
            //        default:
            //            if (type.IsEnum)
            //            {
            //                model = Enum.Parse(type, str);
            //            }
            //            else if (type.Name == "Func`1")
            //            {
            //                break;
            //            }
            //    }
            //    return model;
            //}
        }

        #region JsonToEnum
        private static object JsonToEnum(Type type, string str)
        {
            return Enum.Parse(type, str);
        } 
        #endregion

        #region JsonToUri
        private static object JsonToUri(string str)
        {
            if (str.Length >= 2)
            {
                str = str.Substring(1, str.Length - 2);
                return new Uri(str);
            }
            throw new Exception("string转Uri错误：" + str);
        }
        #endregion

        #region JsonToGuid
        private static Guid JsonToGuid(string str)
        {
            if (str.Length >= 2)
            {
                str = str.Substring(1, str.Length - 2);
                if (Guid.TryParse(str, out Guid guid))
                {
                    return guid;
                }
            }
            throw new Exception("string转Guid错误：" + str);
        }
        #endregion

        #region JsonToDataSet
        private static DataSet JsonToDataSet(string str)
        {
            var ds = new DataSet();
            if (str.Length <= 2)
            {
                return ds;
            }
            str = str.Substring(1, str.Length - 2) + ",";
            var dts = new Dictionary<string, string>();
            while (!String.IsNullOrEmpty(str))
            {
                var pIndex = str.IndexOf("\"", 1);
                var pKey = str.Substring(1, pIndex - 1);
                var value = str.Substring(pIndex + 2);


                var isString = false;//引号
                var jCount = 0;//尖括号[]
                var isAdd = false;
                for (int i = 0; i < value.Length; i++)
                {
                    if (isAdd)
                    {
                        break;
                    }
                    switch (value[i])
                    {
                        case '"':
                            if (isString)
                            {
                                if (str[i - 1] != '\\')
                                {
                                    isString = false;
                                }
                            }
                            else
                            {
                                isString = true;
                            }
                            break;
                        case '[':
                            if (!isString)
                            {
                                jCount++;
                            }
                            break;
                        case ']':
                            if (!isString)
                            {
                                jCount--;
                            }
                            break;
                        case ',':
                            if (!isString && jCount == 0)
                            {
                                var pValue = value.Substring(0, i);
                                dts.Add(pKey, pValue);
                                str = value.Substring(i + 1);
                                isAdd = true;
                            }
                            break;
                    }
                }
            }
            foreach (var kv in dts)
            {
                var dt = JsonToDataTable(kv.Value);
                dt.TableName = kv.Key;
                ds.Tables.Add(dt);
            }
            return ds;
        }
        #endregion

        #region JsonToOther
        private static object JsonToOther(Type type, string str)
        {
            var model = Activator.CreateInstance(type);
            var list = new Dictionary<string, string>();
            str = str.Substring(1, str.Length - 2) + ",";
            while (!String.IsNullOrEmpty(str))
            {
                var pIndex = str.IndexOf("\"", 1);
                var pKey = str.Substring(1, pIndex - 1);
                var value = str.Substring(pIndex + 2);
                var isString = false;//引号
                var dCount = 0;//大括号{}
                var jCount = 0;//尖括号[]
                var isAdd = false;
                for (int i = 0; i < value.Length; i++)
                {
                    if (isAdd)
                    {
                        break;
                    }
                    switch (value[i])
                    {
                        case '"':
                            if (isString)
                            {
                                if (value[i - 1] != '\\')
                                {
                                    isString = false;
                                }
                            }
                            else
                            {
                                isString = true;
                            }
                            break;
                        case '{':
                            if (!isString)
                            {
                                dCount++;
                            }
                            break;
                        case '}':
                            if (!isString)
                            {
                                dCount--;
                            }
                            break;
                        case '[':
                            if (!isString)
                            {
                                jCount++;
                            }
                            break;
                        case ']':
                            if (!isString)
                            {
                                jCount--;
                            }
                            break;
                        case ',':
                            if (!isString && dCount == 0 && jCount == 0)
                            {
                                var pValue = value.Substring(0, i);
                                list.Add(pKey, pValue);
                                str = value.Substring(i + 1);
                                isAdd = true;
                            }
                            break;
                    }
                }
            }

            var fields = type.GetFields();
            var properties = type.GetProperties();

            foreach (var item in list)
            {
                var field = fields.Where(f => f.IsPublic && !f.IsStatic && f.Name == item.Key).FirstOrDefault();
                if (field != null)
                {
                    var value = CreateObject(field.FieldType, item.Value);
                    field.SetValue(model, value);
                }
                else
                {
                    var property = properties.Where(p => p.Name == item.Key).FirstOrDefault();
                    if (property != null)
                    {
                        var value = CreateObject(property.PropertyType, item.Value);
                        property.SetValue(model, value);
                    }
                }
            }

            return model;
        }
        #endregion

        #region JsonToString
        private static string JsonToString(string str)
        {
            if (str.Length >= 2)
            {
                str = str.Substring(1, str.Length - 2).Replace("\\\"", "\"");
                return str;
            }
            throw new Exception("json转string错误：" + str);
        }
        #endregion

        #region JsonToChar
        private static char JsonToChar(string str)
        {
            if (str.Length >= 2)
            {
                str = str.Substring(1, str.Length - 2);
                if (char.TryParse(str, out char c))
                {
                    return c;
                }
            }
            throw new Exception("string转char错误：" + str);
        }
        #endregion

        #region JsonToTimeSpan
        private static TimeSpan JsonToTimeSpan(string str)
        {
            if (str.Length >= 2)
            {
                str = str.Substring(1, str.Length - 2);
                if (TimeSpan.TryParse(str, out TimeSpan ts))
                {
                    return ts;
                }
            }
            throw new Exception("string转TimeSpan错误：" + str);
        }
        #endregion

        #region JsonToDateTimeOffset
        private static DateTimeOffset JsonToDateTimeOffset(string str)
        {
            if (str.Length >= 2)
            {
                str = str.Substring(1, str.Length - 2);
                if (DateTimeOffset.TryParse(str, out DateTimeOffset off))
                {
                    return off;
                }
            }
            throw new Exception("string转DateTimeOffset错误：" + str);
        }
        #endregion

        #region JsonToDateTime
        private static DateTime JsonToDateTime(string str)
        {
            if (str.Length >= 2)
            {
                str = str.Substring(1, str.Length - 2);
                if (DateTime.TryParse(str, out DateTime time))
                {
                    return time;
                }
            }
            throw new Exception("string转DateTime错误：" + str);
        }
        #endregion

        private static DataTable JsonToDataTable(string str)
        {
            var dt = new DataTable();
            if (str.Length <= 2)
            {
                return dt;
            }

            var list = new List<string>();
            str = str.Substring(1, str.Length - 2) + ",";
            while (!String.IsNullOrEmpty(str))
            {
                var isString = false;//引号
                var dCount = 0;//大括号{}
                var isAdd = false;
                for (int i = 0; i < str.Length; i++)
                {
                    if (isAdd)
                    {
                        break;
                    }
                    switch (str[i])
                    {
                        case '"':
                            if (isString)
                            {
                                if (str[i - 1] != '\\')
                                {
                                    isString = false;
                                }
                            }
                            else
                            {
                                isString = true;
                            }
                            break;
                        case '{':
                            if (!isString)
                            {
                                dCount++;
                            }
                            break;
                        case '}':
                            if (!isString)
                            {
                                dCount--;
                            }
                            break;
                        case ',':
                            if (!isString && dCount == 0)
                            {
                                var pValue = str.Substring(0, i);
                                list.Add(pValue);
                                str = str.Substring(i + 1);
                                isAdd = true;
                            }
                            break;
                    }
                }
            }

            var first = true;
            foreach (var item in list)
            {
                var lastStr = item.Substring(1, item.Length - 2) + ",";
                var dic = new Dictionary<string, object>();
                while (!String.IsNullOrEmpty(lastStr))
                {
                    var pIndex = lastStr.IndexOf("\"", 1);
                    var pKey = lastStr.Substring(1, pIndex - 1);
                    var value2 = lastStr.Substring(pIndex + 2);
                    var isString = false;//引号
                    var isAdd = false;
                    for (int i = 0; i < value2.Length; i++)
                    {
                        if (isAdd)
                        {
                            break;
                        }
                        switch (value2[i])
                        {
                            case '"':
                                if (isString)
                                {
                                    if (value2[i - 1] != '\\')
                                    {
                                        isString = false;
                                    }
                                }
                                else
                                {
                                    isString = true;
                                }
                                break;
                            case ',':
                                if (!isString)
                                {
                                    var pValue = value2.Substring(0, i);
                                    dic.Add(pKey, pValue);
                                    lastStr = value2.Substring(i + 1);
                                    isAdd = true;
                                }
                                break;
                        }
                    }
                }

                //TODO:1.根据string分析类型，DataTable的列是有类型的,2.插入的值也要是有类型的
                if (first)
                {
                    foreach (var kv in dic)
                    {
                        dt.Columns.Add(kv.Key, typeof(string));
                    }
                    first = false;
                }

                dt.Rows.Add(dic.Values);
            }

            return dt;
        }

        private static object JsonToObject(Type type, string str)
        {
            var model = Activator.CreateInstance(type);
            var list = new Dictionary<string, string>();
            str = str.Substring(1, str.Length - 2) + ",";
            while (!String.IsNullOrEmpty(str))
            {
                var pIndex = str.IndexOf("\"", 1);
                var pKey = str.Substring(1, pIndex - 1);
                var value = str.Substring(pIndex + 2);
                var isString = false;//引号
                var dCount = 0;//大括号{}
                var jCount = 0;//尖括号[]
                var isAdd = false;
                for (int i = 0; i < value.Length; i++)
                {
                    if (isAdd)
                    {
                        break;
                    }
                    switch (value[i])
                    {
                        case '"':
                            if (isString)
                            {
                                if (value[i - 1] != '\\')
                                {
                                    isString = false;
                                }
                            }
                            else
                            {
                                isString = true;
                            }
                            break;
                        case '{':
                            if (!isString)
                            {
                                dCount++;
                            }
                            break;
                        case '}':
                            if (!isString)
                            {
                                dCount--;
                            }
                            break;
                        case '[':
                            if (!isString)
                            {
                                jCount++;
                            }
                            break;
                        case ']':
                            if (!isString)
                            {
                                jCount--;
                            }
                            break;
                        case ',':
                            if (!isString && dCount == 0 && jCount == 0)
                            {
                                var pValue = value.Substring(0, i);
                                list.Add(pKey, pValue);
                                str = value.Substring(i + 1);
                                isAdd = true;
                            }
                            break;
                    }
                }
            }

            //TODO:创建对象

            return model;
        }

        private static object JsonToIEnumerable(Type type, string str)
        {
            //TODO:迭代器的实例化
            //var model = Activator.CreateInstance(type);
            //return model;
            return null;
        }



    }
}