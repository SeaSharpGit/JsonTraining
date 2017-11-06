using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JsonTraining.Helpers
{
    public static class JsonHelper
    {
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

            sb.Append("{");
            var propertys = obj.GetType().GetProperties();
            foreach (PropertyInfo item in propertys)
            {
                var type = item.PropertyType;
                var typeName = type.Name;
                if (!type.IsSerializable)
                {
                    if (typeName == "Struct")
                    {
                        sb.Append("\"");
                        sb.Append(item.Name);
                        sb.Append("\":");
                        //TODO:序列化结构
                        var value = item.GetValue(obj);
                        AppendString(value, ref sb);
                        sb.Append(",");
                    }
                    continue;
                }

                sb.Append("\"");
                sb.Append(item.Name);
                sb.Append("\":");
                if (type.IsValueType)
                {
                    var value = item.GetValue(obj);
                    switch (typeName)
                    {
                        case "Int16":
                        case "UInt16":
                        case "Int32":
                        case "UInt32":
                        case "Int64":
                        case "UInt64":
                        case "Double":
                        case "Single":
                        case "Decimal":
                        case "Byte":
                        case "Boolean":
                            sb.Append(value);
                            break;
                        case "Enum":
                            sb.Append((int)value);
                            break;
                        case "DateTime":
                            sb.Append("\"");
                            sb.Append(Convert.ToDateTime(value).ToString("s"));
                            sb.Append("\"");
                            break;
                        case "Char":
                            sb.Append("\"");
                            sb.Append(value.ToString() == "\0" ? "\\u0000" : value.ToString());
                            sb.Append("\"");
                            break;
                        case "Nullable`1":
                            sb.Append(value ?? "null");
                            break;
                    }
                }
                else
                {
                    var value = item.GetValue(obj);
                    switch (typeName)
                    {
                        case "String":
                            if (value == null)
                            {
                                sb.Append("null");
                            }
                            else
                            {
                                sb.Append("\"");
                                sb.Append(value);
                                sb.Append("\"");
                            }
                            break;
                        default:
                            AppendString(value, ref sb);
                            break;
                    }
                }
                sb.Append(",");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}");
        }

    }
}