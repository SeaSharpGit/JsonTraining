﻿using JsonTraining.Models;
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
                case DataType.DataTable:
                    return JsonToDataTable(str);
                case DataType.DataSet:
                    return JsonToDataSet(str);




                case DataType.Object:
                    return JsonToObject(type, str);
                case DataType.Empty:
                    return JsonToOther(type, str);
                default:
                    return null;
            }

            //    object model = Activator.CreateInstance(type);
            //    var fullName = type.FullName;
            //    switch (fullName)
            //    {
            //        case "System.Data.DataTable":
            //            //DataTableToJson(obj as DataTable, ref sb);
            //            break;
            //        case "System.Data.DataSet":
            //            //DataSetToJson(obj as DataSet, ref sb);
            //            break;
            //        case "System.Action":
            //            //委托暂不支持
            //            break;
            //        default:
            //            if (type.IsEnum)
            //            {
            //                model = Enum.Parse(type, str);
            //            }
            //            //else if (type.GetInterfaces().Count(i => i.Name == "IEnumerable") > 0)
            //            //{
            //            //    IEnumerableToJson(obj as IEnumerable, ref sb);
            //            //}
            //            else if (type.Name == "Func`1")
            //            {
            //                break;
            //            }
            //            else
            //            {
            //                if (str == "null")
            //                {
            //                    model = null;
            //                }
            //                else
            //                {
            //                    var list = new Dictionary<string, string>();
            //                    str = str.Substring(1, str.Length - 2) + ",";
            //                    while (!String.IsNullOrEmpty(str))
            //                    {
            //                        var pIndex = str.IndexOf("\"", 1);
            //                        var pKey = str.Substring(1, pIndex - 1);
            //                        var value = str.Substring(pIndex + 2);
            //                        var isString = false;//引号
            //                        var dCount = 0;//大括号{}
            //                        var jCount = 0;//尖括号[]
            //                        var isAdd = false;
            //                        for (int i = 0; i < value.Length; i++)
            //                        {
            //                            if (isAdd)
            //                            {
            //                                break;
            //                            }
            //                            switch (value[i])
            //                            {
            //                                case '"':
            //                                    if (isString)
            //                                    {
            //                                        if (value[i - 1] != '\\')
            //                                        {
            //                                            isString = false;
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        isString = true;
            //                                    }
            //                                    break;
            //                                case '{':
            //                                    if (!isString)
            //                                    {
            //                                        dCount++;
            //                                    }
            //                                    break;
            //                                case '}':
            //                                    if (!isString)
            //                                    {
            //                                        dCount--;
            //                                    }
            //                                    break;
            //                                case '[':
            //                                    if (!isString)
            //                                    {
            //                                        jCount++;
            //                                    }
            //                                    break;
            //                                case ']':
            //                                    if (!isString)
            //                                    {
            //                                        jCount--;
            //                                    }
            //                                    break;
            //                                case ',':
            //                                    if (!isString && dCount == 0 && jCount == 0)
            //                                    {
            //                                        var pValue = value.Substring(0, i);
            //                                        list.Add(pKey, pValue);
            //                                        str = value.Substring(i + 1);
            //                                        Debug.WriteLine("key:" + pKey + "===value:" + pValue);
            //                                        isAdd = true;
            //                                    }
            //                                    break;
            //                            }
            //                        }
            //                    }

            //                    var fields = type.GetFields();
            //                    var propertys = type.GetProperties();

            //                    foreach (var item in list)
            //                    {
            //                        var field = fields.Where(f => f.IsPublic && !f.IsStatic && f.Name == item.Key).FirstOrDefault();
            //                        if (field != null)
            //                        {
            //                            var value = CreateObject(field.FieldType, item.Value);
            //                            field.SetValue(model, value);
            //                        }
            //                    }
            //                }
            //            }
            //            break;
            //    }
            //    return model;
            //}
        }
        
        private static object JsonToDataSet(string str)
        {
            throw new NotImplementedException();
        }

        private static object JsonToDataTable(string str)
        {
            throw new NotImplementedException();
        }



        #region JsonToString
        private static object JsonToString(string str)
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
        private static object JsonToChar(string str)
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
        private static object JsonToTimeSpan(string str)
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
        private static object JsonToDateTimeOffset(string str)
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
        private static object JsonToDateTime(string str)
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

        #region JsonToObject
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
                                Debug.WriteLine("key:" + pKey + "===value:" + pValue);
                                isAdd = true;
                            }
                            break;
                    }
                }
            }

            //TODO:创建对象
            //var fields = type.GetFields();
            //var properties = type.GetProperties();
            //foreach (var item in list)
            //{
            //    var field = fields.Where(f => f.IsPublic && !f.IsStatic && f.Name == item.Key).FirstOrDefault();
            //    if (field != null)
            //    {
            //        var value = CreateObject(field.FieldType, item.Value);
            //        field.SetValue(model, value);
            //    }
            //    else
            //    {
            //        var property = properties.Where(p => p.Name == item.Key).FirstOrDefault();
            //        if (property != null)
            //        {
            //            var value = CreateObject(property.PropertyType, item.Value);
            //            property.SetValue(model, value);
            //        }
            //    }
            //}

            return model;
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
                                Debug.WriteLine("key:" + pKey + "===value:" + pValue);
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

    }
}