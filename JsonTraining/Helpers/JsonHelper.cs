using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsonTraining.Helpers
{
    public static class JsonHelper
    {
        #region JsonToObject
        public static T ToObject<T>(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }
            return CreateObject<T>(str);
        }

        private static T CreateObject<T>(string str)
        {
            var type = typeof(T);
            var fullName = type.FullName;
            object model = default(T);
            switch (fullName)
            {
                case "System.Int16":
                    model = Convert.ToInt16(str);
                    break;
                case "System.UInt16":
                    model = Convert.ToUInt16(str);
                    break;
                case "System.Int32":
                    model = Convert.ToInt32(str);
                    break;
                case "System.UInt32":
                    model = Convert.ToUInt32(str);
                    break;
                case "System.Int64":
                    model = Convert.ToInt64(str);
                    break;
                case "System.UInt64":
                    model = Convert.ToUInt64(str);
                    break;
                case "System.Double":
                    model = Convert.ToDouble(str);
                    break;
                case "System.Single":
                    model = Convert.ToSingle(str);
                    break;
                case "System.Decimal":
                    model = Convert.ToDecimal(str);
                    break;
                case "System.Byte":
                    model = Convert.ToByte(str);
                    break;
                case "System.Boolean":
                    model = Convert.ToBoolean(str);
                    break;
                case "System.DateTime":
                    model = Convert.ToDateTime(str.Substring(1, str.Length - 2));
                    break;
                case "System.Char":
                    model = Convert.ToChar(str.Substring(1, str.Length - 2));
                    break;
                case "System.String":
                    model = str.Substring(1, str.Length - 2);
                    break;
                case "System.Data.DataTable":
                    //DataTableToJson(obj as DataTable, ref sb);
                    break;
                case "System.Data.DataSet":
                    //DataSetToJson(obj as DataSet, ref sb);
                    break;
                case "System.Action":
                    //委托暂不支持
                    break;
                default:
                    if (type.IsEnum)
                    {
                        model = Enum.Parse(type, str);
                    }
                    //else if (type.GetInterfaces().Count(i => i.Name == "IEnumerable") > 0)
                    //{
                    //    IEnumerableToJson(obj as IEnumerable, ref sb);
                    //}
                    else if (type.Name == "Func`1")
                    {
                        break;
                    }
                    else
                    {
                        if (str == "null")
                        {
                            model = null;
                        }
                        else
                        {
                            str = str.Substring(1, str.Length - 2) + ",";
                            var list = new Dictionary<string, string>();
                            while (str.Length > 0)
                            {
                                var pIndex = str.IndexOf("\"", 1);
                                var pKey = str.Substring(1, pIndex - 1);
                                var value = str.Substring(pIndex + 2);
                                var isString = false;//引号
                                var dCount = 0;//大括号
                                var jCount = 0;//尖括号
                                for (int i = 0; i < value.Length; i++)
                                {
                                    var c = value[i];
                                    if (c == '"')
                                    {
                                        isString = !isString;
                                    }

                                    if (isString)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        switch (c)
                                        {
                                            case ',':
                                                break;

                                        }
                                        if (c == ',' && dCount == 0 && jCount == 0)
                                        {
                                            //Todo拿出来

                                            //重新改写字符串
                                        }
                                        else
                                        {
                                            if()
                                        }
                                    }
                                }

                            }

                            //var pIndex = text.IndexOf('"');
                            //var pName = text.Substring(0, pIndex);
                        }
                    }
                    break;
            }
            return (T)model;
        }
        #endregion


        #region ObjectToJson
        public static string ToJson(object obj)
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
            var fullName = type.FullName;
            switch (fullName)
            {
                case "System.Int16":
                case "System.UInt16":
                case "System.Int32":
                case "System.UInt32":
                case "System.Int64":
                case "System.UInt64":
                case "System.Double":
                case "System.Single":
                case "System.Decimal":
                case "System.Byte":
                    sb.Append(obj);
                    break;
                case "System.Boolean":
                    sb.Append(obj.ToString().ToLower());
                    break;
                case "System.DateTime":
                    sb.Append("\"");
                    sb.Append(Convert.ToDateTime(obj).ToString("s"));
                    sb.Append("\"");
                    break;
                case "System.Char":
                    sb.Append("\"");
                    sb.Append(obj.ToString() == "\0" ? "\\u0000" : obj.ToString());
                    sb.Append("\"");
                    break;
                case "System.String":
                    sb.Append("\"");
                    //做一个处理，作为反序列化时判断依据
                    var str = obj.ToString().Replace("\"", "\\\"");
                    sb.Append(str);
                    sb.Append("\"");
                    break;
                case "System.Data.DataTable":
                    DataTableToJson(obj as DataTable, ref sb);
                    break;
                case "System.Data.DataSet":
                    DataSetToJson(obj as DataSet, ref sb);
                    break;
                case "System.Action":
                    //委托暂不支持
                    break;
                default:
                    if (type.IsEnum)
                    {
                        sb.Append((int)obj);
                    }
                    else if (type.GetInterfaces().Count(i => i.Name == "IEnumerable") > 0)
                    {
                        IEnumerableToJson(obj as IEnumerable, ref sb);
                    }
                    else if (type.Name == "Func`1")
                    {
                        //委托暂不支持
                        break;
                    }
                    else
                    {
                        ObjectToJson(obj, ref sb);
                    }
                    break;
            }
        }

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
        private static void IEnumerableToJson(IEnumerable items, ref StringBuilder sb)
        {
            sb.Append("[");
            var flag = false;
            foreach (var i in items)
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
        #endregion

        #endregion
    }
}