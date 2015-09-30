using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;
using FogBugzAPI.Model.Fields;

namespace FogBugzAPI.Model {
    public abstract class FogBugzObject<TEnum, TFieldCreator, TCreatableField> : IFogBugzType
        where TEnum : struct, IConvertible, IComparable, IFormattable
        where TFieldCreator : IFieldCreator<TEnum, TCreatableField>
        where TCreatableField : Field<TEnum, TCreatableField>
    {
        public List<TCreatableField> Fields { get; } = new List<TCreatableField>();

        protected FogBugzObject(XElement element, TFieldCreator fieldCreator)
        {
            TEnum[] caseFieldsName = (TEnum[])Enum.GetValues(typeof(TEnum));

            foreach (var fieldName in caseFieldsName)
            {
                
                XElement fieldXElement = element.XPathSelectElement(fieldCreator.GetFogBugzName(fieldName));
                if (fieldXElement != null)
                {
                    TCreatableField caseField = fieldCreator.CreateField(fieldName);
                    caseField.Value.FromString(fieldXElement.Value);
                    Fields.Add(caseField);
                }
            }
        }
    }
}