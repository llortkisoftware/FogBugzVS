using System;
using System.Security.Cryptography.X509Certificates;
using FogBugzAPI.Model.Cases.Fields;

namespace FogBugzAPI.Model.Fields {
    
    public abstract class Field<TEnum, TCreatable> : ICreatableField<TCreatable>
        where TEnum : struct, IConvertible, IComparable, IFormattable
        where TCreatable : ICreatableField<TCreatable> {

        protected Field(TEnum caseFieldName, FieldType fieldType, string fogBugzName)
        {
            FieldName = caseFieldName;
            FieldType = fieldType;
            FogBugzName = fogBugzName;
            switch (fieldType)
            {
                case FieldType.Boolean:
                    Value = new FieldValueBool();
                    break;
                case FieldType.StringList:
                    Value = new FieldValueStringList();
                    break;
                case FieldType.String:
                    Value = new FieldValueString();
                    break;
                case FieldType.IntegerList:
                    Value = new FieldValueIntegerList();
                    break;
                case FieldType.Integer:
                    Value = new FieldValueInt();
                    break;
                case FieldType.DateTime:
                    Value = new FieldValueDateTime();
                    break;
                case FieldType.Double:
                    Value = new FieldValueDouble();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fieldType), fieldType, null);
            }
        }

        public FieldType FieldType { get; private set; }
        public string FogBugzName { get; private set; }
        public FieldValue Value { get; private set; }
        public TEnum FieldName { get; private set; }

        public abstract TCreatable CreateNew();

        public abstract TCreatable CreateNew(string value);
    }
}