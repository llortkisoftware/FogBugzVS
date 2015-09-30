using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using FogBugzAPI.Model;
using FogBugzAPI.Model.Cases;
using FogBugzAPI.Model.Cases.Fields;

namespace FogBugzAPI.FogBugzClient.Command
{
    public class CaseCommands
    {
        //search
        //      q=query : case#,case#,case# | "#" | "string"
        //      cols= columns to return
        //      max = max records

        /*
cmd=email, cmd=reply, and cmd=forward

Same as cmd=edit, with additional arguments: sFrom (required), sTo (required), ixBug (required), sEvent (required, body of email), sSubject, sCC, sBCC and ixBugEventAttachment (this is the ixBugEvent if you want to include any attachments from a previous email, for example when you want to do a “forward“

It is not required, but we recommend making sure the case you email from has sCustomerEmail and ixMailbox set.

 Note: You can supply any address for the sFrom CaseFieldName, although the UI restricts you to email addresses that FogBugz is actively checking (so that when a user replies to your email, it will actually go back into FogBugz).  The sFrom CaseFieldName you supply here should be one of the values returned from the cmd=listMailboxes command, and best practices suggest using a matching ixMailbox value for the case.
    */

        public class GetCaseCommand : IFogBugzCommand<Case> {
            public string Command => "search";

            public List<KeyValuePair<string, string>> Parameters { get; } = new List<KeyValuePair<string, string>>();

            public GetCaseCommand(int caseId) : this(caseId, new List<CaseFieldName>()) { }

            public GetCaseCommand(int caseId, List<CaseFieldName> fieldNames)
            {
                Parameters.Add(new KeyValuePair<string, string>("q", caseId.ToString()));
                if (fieldNames.Count > 0)
                {
                    StringBuilder colsBuilder = new StringBuilder();
                    CaseFieldCreator fieldCreator = new CaseFieldCreator();
                    
                    foreach (var fieldName in fieldNames)
                    {
                        colsBuilder.Append(fieldCreator.GetFogBugzName(fieldName)).Append(",");
                    }
                    colsBuilder.Remove(colsBuilder.Length - 1, 1);
                    Parameters.Add(new KeyValuePair<string, string>("cols", colsBuilder.ToString()));
                }
            }
            public Case CreateResponse(FogBugzReturn fogBugzReturn)
            {
                CaseList list = new CaseList(fogBugzReturn);
                return list.Count > 0 ? list[0] : null;
            }
        }
    }
}
