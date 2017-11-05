using System;
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
            var length = propertys.Length;
            for (int i = 0; i < length; i++)
            {
                var model = propertys[i];
                sb.Append("\"");
                sb.Append(model.Name);
                sb.Append("\":");
                var value = model.GetValue(obj);
                if (value == null)
                {
                    sb.Append("null,");
                    continue;
                }

                switch (value.GetType().Name)
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
                    case "String":
                        sb.Append("\"");
                        sb.Append(value);
                        sb.Append("\"");
                        break;
                    default:
                        AppendString(value, ref sb);
                        break;
                }
                sb.Append(",");
            }
            if (length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}");
        }

    }
}