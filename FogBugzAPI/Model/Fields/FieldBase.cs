using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using FogBugzAPI.Model.Cases.Fields;

namespace FogBugzAPI.Model.Fields {
    public abstract class FieldBase<TEnum, TCreatable> : ICreatableField<TCreatable>, IFieldCreator<TEnum, TCreatable> 
        where TEnum : struct, IConvertible, IComparable, IFormattable 
        where TCreatable : new() {
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")] private static readonly Hashtable LookupTable = new Hashtable();
        
        protected FieldBase() { } 
        protected FieldBase(TEnum caseFieldName, FieldType fieldType, string fogBugzName)
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


        protected static void AddToLookupTable(FieldBase<TEnum, TCreatable> field)
        {
            LookupTable.Add(field.FieldName, field);
        }

        public TCreatable CreateField(TEnum caseFieldName)
        {
            return ((FieldBase<TEnum, TCreatable>) LookupTable[caseFieldName]).CreateNew();
        }

        public static TCreatable GetInstance()
        {
            return new TCreatable();
        }

        public abstract TCreatable CreateNew();

        public abstract TCreatable CreateNew(string value);
        
    }
}