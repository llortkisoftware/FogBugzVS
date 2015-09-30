using System.Collections.Generic;
using System.Text;

namespace FogBugzAPI.Model.Cases.Fields
{
    public class FieldValueStringList : FieldValue
    {
        public FieldValueStringList()
        {
            Value = new List<string>();
        }

        public override void FromString(string value)
        {
            if (!(Value is List<string>))
            {
                Value = new List<string>();
            }
            else
            {
                ((List<string>)Value).Clear();
            }
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            string[] list = value.Split(',');
            foreach (var val in list)
            {
                ((List<string>)Value).Add(val);
            }
        }

        public override string ToStringValue()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var val in (List<string>)Value)
            {
                builder.Append(val).Append(",");
            }
            if (builder.Length > 1)
            {
                builder.Remove(builder.Length - 1, 1);
            }
            return builder.ToString();
        }
    }
}