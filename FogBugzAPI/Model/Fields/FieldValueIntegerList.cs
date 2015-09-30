using System;
using System.Collections.Generic;
using System.Text;

namespace FogBugzAPI.Model.Cases.Fields
{
    public class FieldValueIntegerList : FieldValue
    {
        public FieldValueIntegerList()
        {
            Value = new List<int>();
        }

        public override void FromString(string value)
        {
            if (!(Value is List<int>))
            {
                Value = new List<int>();
            }
            else
            {
                ((List<int>)Value).Clear();
            }
            if (String.IsNullOrEmpty(value))
            {
                return;
            }
            string[] list = value.Split(',');
            foreach (var val in list)
            {
                ((List<int>)Value).Add(Convert.ToInt32(val));
            }
        }

        public override string ToStringValue()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var val in (List<int>)Value)
            {
                builder.Append(val.ToString()).Append(",");
            }
            if (builder.Length > 1)
            {
                builder.Remove(builder.Length - 1, 1);
            }
            return builder.ToString();
        }
    }
}