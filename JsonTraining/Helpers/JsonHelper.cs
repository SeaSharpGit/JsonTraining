using System;
using System.Text;

namespace JT.Helpers
{
    public static class JsonHelper
    {
        public static string ToJson(object obj)
        {
            var sb = new StringBuilder();
            AppendString(obj,ref sb);
            return sb.ToString();
        }

        private static void AppendString(object obj,ref StringBuilder sb)
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
                var type = value.GetType();
                if (type.IsValueType)
                {
                    if (type == typeof(char))
                    {
                        sb.Append("\"");
                        if (value.ToString() == "\0")
                        {
                            sb.Append("\\u0000");
                        }
                        else
                        {
                            sb.Append(value.ToString());
                        }
                        sb.Append("\"");
                    }
                    else if(type==typeof(DateTime))
                    {
                        sb.Append("\"");
                        sb.Append(Convert.ToDateTime(value).ToString("s"));
                        sb.Append("\"");
                    }
                    else
                    {
                        sb.Append(value);
                    }
                }
                else if (type == typeof(string))
                {
                    sb.Append("\"");
                    sb.Append(value);
                    sb.Append("\"");
                }
                else
                {
                    AppendString(value, ref sb);
                    //sb.Append(ToJson(value));
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