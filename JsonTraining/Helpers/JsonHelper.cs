using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            var type = obj.GetType();
            var typeName = type.Name;
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
                    sb.Append(obj);
                    break;
                case "Boolean":
                    sb.Append(obj.ToString().ToLower());
                    break;
                case "Enum":
                    sb.Append((int)obj);
                    break;
                case "DateTime":
                    sb.Append("\"");
                    sb.Append(Convert.ToDateTime(obj).ToString("s"));
                    sb.Append("\"");
                    break;
                case "Char":
                    sb.Append("\"");
                    sb.Append(obj.ToString() == "\0" ? "\\u0000" : obj.ToString());
                    sb.Append("\"");
                    break;
                case "Nullable`1":
                    sb.Append(obj);
                    break;
                case "String":
                    sb.Append("\"");
                    sb.Append(obj);
                    sb.Append("\"");
                    break;
                default:
                    if (type.GetInterfaces().Count(i => i.Name == "IEnumerable") > 0)
                    {
                        sb.Append("[");
                        foreach (var i in obj as IEnumerable)
                        {
                            AppendString(i, ref sb);
                        }
                        sb.Append("]");
                    }
                    else
                    {
                        var propertys = type.GetProperties();
                        if (propertys.Count() > 0)
                        {
                            sb.Append("{");
                            foreach (var item in propertys)
                            {
                                sb.Append("\"");
                                sb.Append(item.Name);
                                sb.Append("\":");
                                var value = item.GetValue(obj);
                                AppendString(value, ref sb);
                                sb.Append(",");
                            }
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("}");
                        }

                    }
                    break;
            }
        }

    }
}