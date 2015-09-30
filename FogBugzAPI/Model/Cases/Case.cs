using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FogBugzAPI.FogBugzClient;
using FogBugzAPI.Model.Cases.Fields;

namespace FogBugzAPI.Model.Cases
{

    /*
  List caseFieldsName:

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


    public class Case : FogBugzObject<CaseFieldName, CaseField, CaseField>
    {
        public List<CaseOperation> AvailableCaseOperations { get; } = new List<CaseOperation>();

        public CaseField GetFogBugzCaseField(CaseFieldName caseFieldName)
        {
            return Fields.FirstOrDefault(c => c.FieldName == caseFieldName);
        }

        public bool FogBugzCaseFieldExists(CaseFieldName caseFieldName)
        {
            return Fields.Count(c => c.FieldName == caseFieldName) > 0;
        }

        public Case(XElement caseElement) : base(caseElement, CaseField.GetInstance())
        {
            String[] operationStrings = caseElement.Attribute("operations").Value.Split(',');

            foreach (var operationString in operationStrings)
            {
                var capitalized = operationString.Substring(0, 1).ToUpper() +
                                  operationString.Substring(1, operationString.Length - 1);
                AvailableCaseOperations.Add((CaseOperation)Enum.Parse(typeof(CaseOperation), capitalized));
            }
            
        }
    }
}

