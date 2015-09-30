using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;
using FogBugzAPI.Model.Fields;

namespace FogBugzAPI.Model {
    public abstract class FogBugzObject<TEnum, TFieldCreator, TCreatableField> : IFogBugzType
        where TEnum : struct, IConvertible, IComparable, IFormattable
        where TFieldCreator : IFieldCreator<TEnum, TCreatableField>
        where TCreatableField : FieldBase<TEnum, TCreatableField>, new()
    {
        public List<TCreatableField> Fields { get; } = new List<TCreatableField>();

        protected FogBugzObject(XElement element, TFieldCreator fieldCreator)
        {
            TEnum[] caseFieldsName = (TEnum[])Enum.GetValues(typeof(TEnum));

            foreach (var fieldName in caseFieldsName)
            {
                TCreatableField caseField = fieldCreator.CreateField(fieldName);
                XElement fieldXElement = element.XPathSelectElement(caseField.FogBugzName);
                if (fieldXElement != null)
                {
                    Fields.Add(caseField.CreateNew(fieldXElement.Value));
                }
            }
        }
    }
}