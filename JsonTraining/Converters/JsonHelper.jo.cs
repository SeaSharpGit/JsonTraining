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
        public static T ConvertToJsonObject<T>(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException();
            }
            var type = typeof(T);
            return (T)CreateObject(type, json);
        }

        private static object CreateObject(Type type, string json)
        {
            if (json == "null" || json == null)
            {
                return null;
            }
            var dataType = DataTypeMappings.GetDataType(type);
            switch (dataType)
            {
                case DataType.SByte:
                case DataType.SByteNullable:
                    return sbyte.Parse(json);
                case DataType.Byte:
                case DataType.ByteNullable:
                    return byte.Parse(json);
                case DataType.Int16:
                case DataType.Int16Nullable:
                    return short.Parse(json);
                case DataType.UInt16:
                case DataType.UInt16Nullable:
                    return ushort.Parse(json);
                case DataType.Int32:
                case DataType.Int32Nullable:
                    return int.Parse(json);
                case DataType.UInt32:
                case DataType.UInt32Nullable:
                    return uint.Parse(json);
                case DataType.Int64:
                case DataType.Int64Nullable:
                    return long.Parse(json);
                case DataType.UInt64:
                case DataType.UInt64Nullable:
                    return ulong.Parse(json);
                case DataType.Double:
                case DataType.DoubleNullable:
                    return double.Parse(json);
                case DataType.Single:
                case DataType.SingleNullable:
                    return float.Parse(json);
                case DataType.Decimal:
                case DataType.DecimalNullable:
                    return decimal.Parse(json);
                case DataType.DateTime:
                case DataType.DateTimeNullable:
                    return JsonToDateTime(json);
                case DataType.DateTimeOffset:
                case DataType.DateTimeOffsetNullable:
                    return JsonToDateTimeOffset(json);
                case DataType.TimeSpan:
                case DataType.TimeSpanNullable:
                    return JsonToTimeSpan(json);
                case DataType.String:
                    return JsonToString(json);
                case DataType.Boolean:
                case DataType.BooleanNullable:
                    return bool.Parse(json);
                case DataType.Char:
                case DataType.CharNullable:
                    return JsonToChar(json);
                case DataType.Guid:
                case DataType.GuidNullable:
                    return JsonToGuid(json);
                case DataType.BigInteger:
                case DataType.BigIntegerNullable:
                    return BigInteger.Parse(json);
                case DataType.DataTable:
                    return JsonToDataTable(json);
                case DataType.DataSet:
                    return JsonToDataSet(json);
                case DataType.IEnumerable:
                    return JsonToIEnumerable(type, json);
                case DataType.Uri:
                    return JsonToUri(json);
                case DataType.Enum:
                    return JsonToEnum(type, json);
                case DataType.Object:
                    return JsonToObject(type, json);
                case DataType.Empty:
                    return JsonToOther(type, json);
                default:
                    return null;
            }
        }

        #region JsonToEnum
        private static object JsonToEnum(Type type, string json)
        {
            return Enum.Parse(type, json);
        }
        #endregion

        #region JsonToUri
        private static object JsonToUri(string json)
        {
            if (json.StartsWith("\"") && json.EndsWith("\""))
            {
                json = json.Substring(1, json.Length - 2);
            }
            return new Uri(json);
        }
        #endregion

        #region JsonToGuid
        private static Guid JsonToGuid(string json)
        {
            if (json.StartsWith("\"") && json.EndsWith("\""))
            {
                json = json.Substring(1, json.Length - 2);
            }
            if (Guid.TryParse(json, out Guid guid))
            {
                return guid;
            }
            else
            {
                throw new Exception("string转Guid错误：" + json);
            }
        }
        #endregion

        #region JsonToDataSet
        private static DataSet JsonToDataSet(string json)
        {
            var ds = new DataSet();
            if (json.Length <= 2)
            {
                return ds;
            }
            json = json.Substring(1, json.Length - 2) + ",";
            var dts = new Dictionary<string, string>();
            while (!String.IsNullOrEmpty(json))
            {
                var pIndex = json.IndexOf("\"", 1);
                var pKey = json.Substring(1, pIndex - 1);
                var value = json.Substring(pIndex + 2);


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
                                if (i == 0 || json[i - 1] != '\\')
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
                                json = value.Substring(i + 1);
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
        private static object JsonToOther(Type type, string json)
        {
            var model = Activator.CreateInstance(type);
            if (json.Length <= 2)
            {
                return model;
            }

            var list = new Dictionary<string, string>();
            json = json.Substring(1, json.Length - 2) + ",";
            while (!String.IsNullOrEmpty(json))
            {
                var pIndex = json.IndexOf("\"", 1);
                var pKey = json.Substring(1, pIndex - 1);
                var value = json.Substring(pIndex + 2);
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
                                json = value.Substring(i + 1);
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
        private static string JsonToString(string json)
        {
            if (json.StartsWith("\"") && json.EndsWith("\""))
            {
                json = json.Substring(1, json.Length - 2);
            }
            return json.Replace("\\\"", "\"");
        }
        #endregion

        #region JsonToChar
        private static char JsonToChar(string json)
        {
            if (json.StartsWith("\"") && json.EndsWith("\""))
            {
                json = json.Substring(1, json.Length - 2);
            }
            if (char.TryParse(json, out char c))
            {
                return c;
            }
            else
            {
                throw new Exception("string转char错误：" + json);
            }
        }
        #endregion

        #region JsonToTimeSpan
        private static TimeSpan JsonToTimeSpan(string json)
        {
            if (json.StartsWith("\"") && json.EndsWith("\""))
            {
                json = json.Substring(1, json.Length - 2);
            }
            if (TimeSpan.TryParse(json, out TimeSpan ts))
            {
                return ts;
            }
            else
            {
                throw new Exception("string转TimeSpan错误：" + json);
            }
        }
        #endregion

        #region JsonToDateTimeOffset
        private static DateTimeOffset JsonToDateTimeOffset(string json)
        {
            if (json.StartsWith("\"") && json.EndsWith("\""))
            {
                json = json.Substring(1, json.Length - 2);
            }
            if (DateTimeOffset.TryParse(json, out DateTimeOffset off))
            {
                return off;
            }
            else
            {
                throw new Exception("string转DateTimeOffset错误：" + json);
            }
        }
        #endregion

        #region JsonToDateTime
        private static DateTime JsonToDateTime(string json)
        {
            if (json.StartsWith("\"") && json.EndsWith("\""))
            {
                json = json.Substring(1, json.Length - 2);
            }
            if (DateTime.TryParse(json, out DateTime time))
            {
                return time;
            }
            else
            {
                throw new Exception("string转DateTime错误：" + json);
            }
        }
        #endregion

        #region JsonToObject
        private static object JsonToObject(Type type, string json)
        {
            var list = new Dictionary<string, string>();
            json = json.Substring(1, json.Length - 2) + ",";
            while (!String.IsNullOrEmpty(json))
            {
                var pIndex = json.IndexOf("\"", 1);
                var pKey = json.Substring(1, pIndex - 1);
                var value = json.Substring(pIndex + 2);
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
                                json = value.Substring(i + 1);
                                isAdd = true;
                            }
                            break;
                    }
                }
            }
            var sb = new StringBuilder();
            sb.Append("return new {");
            var start = 1;
            foreach (var item in list)
            {

                sb.Append(item.Key);
                sb.Append("=");
                sb.Append(item.Value);
                if (start != list.Count)
                {
                    sb.Append(",");
                    sb.Append(Environment.NewLine);
                }
                start++;
            }
            sb.Append("};");
            var code = sb.ToString();
            return CodeHelper.CodeToObject(code);
        }
        #endregion

        #region JsonToDataTable
        private static DataTable JsonToDataTable(string json)
        {
            var dt = new DataTable();
            if (json.Length <= 2)
            {
                return dt;
            }

            var list = new List<string>();
            json = json.Substring(1, json.Length - 2) + ",";
            while (!String.IsNullOrEmpty(json))
            {
                var isString = false;//引号
                var dCount = 0;//大括号{}
                var isAdd = false;
                for (int i = 0; i < json.Length; i++)
                {
                    if (isAdd)
                    {
                        break;
                    }
                    switch (json[i])
                    {
                        case '"':
                            if (isString)
                            {
                                if (i == 0 || json[i - 1] != '\\')
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
                                var pValue = json.Substring(0, i);
                                list.Add(pValue);
                                json = json.Substring(i + 1);
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

                var row = dt.NewRow();
                foreach (var kv in dic)
                {
                    var value = kv.Value.ToString();
                    if (first)
                    {
                        if (value.StartsWith("\"") && value.EndsWith("\""))
                        {
                            dt.Columns.Add(kv.Key, typeof(string));
                            row[kv.Key] = value.Substring(1, value.Length - 2);
                        }
                        else if (value.ToLower() == "true" || value.ToLower() == "false")
                        {
                            dt.Columns.Add(kv.Key, typeof(bool));
                            row[kv.Key] = kv.Value;
                        }
                        else
                        {
                            dt.Columns.Add(kv.Key, typeof(int));
                            row[kv.Key] = kv.Value;
                        }
                    }
                    else
                    {
                        if (value.StartsWith("\"") && value.EndsWith("\""))
                        {
                            row[kv.Key] = value.Substring(1, value.Length - 2);
                        }
                        else if (value.ToLower() == "true" || value.ToLower() == "false")
                        {
                            row[kv.Key] = kv.Value;
                        }
                        else
                        {
                            row[kv.Key] = kv.Value;
                        }
                    }
                }
                if (first)
                {
                    first = false;
                }
                dt.Rows.Add(row);
            }

            return dt;
        }
        #endregion

        #region JsonToIEnumerable
        private static object JsonToIEnumerable(Type type, string json)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                return JsonToDictionary(type, json);
            }
            if (string.IsNullOrEmpty(json) || !json.StartsWith("[") || !json.EndsWith("]"))
            {
                return null;
            }
            if (json == "[]")
            {
                return type.IsArray ? Array.CreateInstance(type.GetElementType(), 0) : Activator.CreateInstance(type);
            }

            var list = new List<string>();
            json = json.Substring(1, json.Length - 2) + ",";
            while (!String.IsNullOrEmpty(json))
            {
                var isString = false;//引号
                var dCount = 0;//大括号{}
                var jCount = 0;//尖括号[]
                var isAdd = false;
                for (int i = 0; i < json.Length; i++)
                {
                    if (isAdd)
                    {
                        break;
                    }
                    switch (json[i])
                    {
                        case '"':
                            if (isString)
                            {
                                if (i == 0 || json[i - 1] != '\\')
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
                                list.Add(json.Substring(0, i));
                                json = json.Substring(i + 1);
                                isAdd = true;
                            }
                            break;
                    }
                }
            }
            if (type.IsArray)
            {
                return JsonToArray(type, list);
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                return JsonToList(type, list);
            }
            else if (type == typeof(ArrayList))
            {
                return JsonToArrayList(type, list);
            }

            return null;
        }
        #endregion

        #region JsonToArraryList
        private static object JsonToArrayList(Type type, List<string> list)
        {
            var al = new ArrayList();
            foreach (var item in list)
            {
                if (item.StartsWith("\"") && item.EndsWith("\""))
                {
                    al.Add(item.Substring(1, item.Length - 2));
                }
                else if (bool.TryParse(item, out bool b))
                {
                    al.Add(b);
                }
                else if (int.TryParse(item, out int i))
                {
                    al.Add(i);
                }
            }
            return al;
        }
        #endregion

        #region JsonToDictionary
        private static object JsonToDictionary(Type type, string json)
        {
            var model = Activator.CreateInstance(type);
            if (!json.StartsWith("{") || !json.EndsWith("}"))
            {
                return model;
            }

            json = json.Substring(1, json.Length - 2) + ",";
            var list = new List<string>();
            while (!String.IsNullOrEmpty(json))
            {
                var isString = false;//引号
                var dCount = 0;//大括号{}
                var jCount = 0;//尖括号[]
                var isAdd = false;
                for (int i = 0; i < json.Length; i++)
                {
                    if (isAdd)
                    {
                        break;
                    }
                    switch (json[i])
                    {
                        case '"':
                            if (isString)
                            {
                                if (i == 0 || json[i - 1] != '\\')
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
                                list.Add(json.Substring(0, i));
                                json = json.Substring(i + 1);
                                isAdd = true;
                            }
                            break;
                    }
                }
            }

            Assembly assembly = Assembly.Load("mscorlib.dll");
            Type typeClass = assembly.GetType("System.Collections.IDictionary");
            foreach (var item in list)
            {
                var typeTs = type.GenericTypeArguments;
                var index = item.IndexOf(":");
                var key = item.Substring(1, index - 2);
                var value = item.Substring(index + 2, item.Length - index - 3);
                var objk = CreateObject(typeTs[0], key);
                var objv = CreateObject(typeTs[1], value);

                var types = new Type[2] { typeof(object), typeof(object) };
                var objs = new Object[2] { objk, objv };
                typeClass.GetMethod("Add", types).Invoke(model, objs);
            }
            return model;
        }
        #endregion

        #region JsonToArray
        private static object JsonToArray(Type type, List<string> list)
        {
            var typeT = type.GetElementType();
            var model = Array.CreateInstance(typeT, list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (item.StartsWith("\"") && item.EndsWith("\""))
                {
                    item = item.Substring(1, item.Length - 2);
                }
                var obj = CreateObject(type, item);
                model.SetValue(obj, i);
            }
            return model;
        }
        #endregion

        #region JsonToList
        private static object JsonToList(Type type, List<string> list)
        {
            var model = Activator.CreateInstance(type);
            var typeT = type.GenericTypeArguments[0];
            Assembly assembly = Assembly.Load("mscorlib.dll");
            Type typeClass = assembly.GetType("System.Collections.IList");
            foreach (var item in list)
            {
                var value = CreateObject(typeT, item);
                var types = new Type[1] { typeof(object) };
                var objs = new Object[1] { value };
                typeClass.GetMethod("Add", types).Invoke(model, objs);
            }
            return model;
        }
        #endregion


    }
}