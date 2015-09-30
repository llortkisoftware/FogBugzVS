using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using FogBugzAPI.FogBugzClient;
using FogBugzAPI.Model.Cases.Fields;

namespace FogBugzAPI.Model.Cases
{

    /*
  List fieldsName:

<tags> -- tags
  <tag><![CDATA[first]]></tag>
  <tag><![CDATA[second]]></tag>
  <tag><![CDATA[third]]></tag>
</tags>

*/


    public enum CaseOperation
    {
        Edit,
        Assign,
        Resolve,
        Reactivate,
        Close,
        Reopen,
        Reply,
        Forward,
        Email,
        Remind
    }

    //Cases list = "<cases>"
    //Attribut "count" = #ofcases
    public class Case : IFogBugzType
    {
        //Element "case"

        //attribute = "operations"

        public List<CaseOperation> AvailableCaseOperations { get; } = new List<CaseOperation>();
        //attribute = "ixBug"
        public int CaseId { get; private set; }

        //End "case"

        public List<Field> Fields { get; } = new List<Field>();

        public Field GetFogBugzCaseField(FieldName fieldName)
        {
            return Fields.FirstOrDefault(c => c.FieldName == fieldName);
        }

        public bool FogBugzCaseFieldExists(FieldName fieldName)
        {
            return Fields.Count(c => c.FieldName == fieldName) > 0;
        }

        public Case(XElement caseElement)
        {
            CaseId = Convert.ToInt32(caseElement.Attribute("ixBug").Value);

            String[] operationStrings = caseElement.Attribute("operations").Value.Split(',');

            foreach (var operationString in operationStrings)
            {
                var capitalized = operationString.Substring(0, 1).ToUpper() +
                                  operationString.Substring(1, operationString.Length - 1);
                AvailableCaseOperations.Add((CaseOperation)Enum.Parse(typeof(CaseOperation), capitalized));
            }

            FieldName[] fieldsName = (FieldName[])Enum.GetValues(typeof(FieldName));

            foreach (var fieldName in fieldsName)
            {
                Field field = Field.GetFogBugzCaseField(fieldName);
                XElement fieldXElement = caseElement.XPathSelectElement(field.FogBugzName);
                if (fieldXElement != null)
                {
                    Fields.Add(field.CreateNew(fieldXElement.Value));
                }
            }
        }
    }
}

