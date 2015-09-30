using System;

namespace FogBugzAPI.Model.Cases.Fields
{
    public class FieldValueDateTime : FieldValue
    {
        //FORMAT:    2007-05-06T22:47:59Z

        public FieldValueDateTime()
        {
            Value = DateTime.Now;
        }

        public override void FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Value = DateTime.MinValue;
                return;
            }

            Value = Convert.ToDateTime(value).ToLocalTime();
        }

        public override string ToStringValue()
        {
            DateTime dtValue = (DateTime)Value;
            return dtValue.ToUniversalTime().ToString("o");
        }
    }
}