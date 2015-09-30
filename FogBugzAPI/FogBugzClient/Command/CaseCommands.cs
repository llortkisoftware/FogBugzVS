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

        //new
        //edit
        //assign
        //reactivate
        //reopen

        /*
        *
        *
        Editing Cases

        Note that setting ixPersonEditedBy when using the command cmd=new sets who opened the case.

    cmd=new and cmd=edit and cmd=assign and cmd=reactivate and cmd=reopen

Arguments:

    ixBugParent - Make this case a subcase of another case
    ixBugEvent – omitted for cmd=new – optional - If supplied, and this is not equal to the latest bug event for the case, you will receive error code 9 back to show that you were working with a “stale” view of the case.
    tags - When searching for the tags column via the API, returns a <tags> element with a <tag> for each tag in the case.
    sTags- When editing a case, use the sTags column and submit a comma-delimited list of the tags associated with the case. Existing tags omitted from this list will be removed.
    sTitle -
    ixProject (or sProject) - If the user does not have Modify on any projects, the return will be Error Code 13: Insufficient Permissions.
    ixArea (or sArea)
    ixFixFor (or sFixFor – searches project first, then global fixfors)
    ixCategory (or sCategory)
    ixPersonAssignedTo (or sPersonAssignedTo)
    ixPriority (or sPriority)
    dtDue
    hrsCurrEst 
    hrsElapsedExtra
    dblStoryPts Sets the Story Points for a case.
    sVersion
    sComputer
    sCustomerEmail
    ixMailbox
    sScoutDescription – used only with cmd=new
        If you set this, and FogBugz finds a case with this sScoutDescription, it will append to that case unless fScoutStopReporting is true for that case, and then it will do nothing.
    sScoutMessage
        The message you are supposed to display to users for this case
    fScoutStopReporting
        Set this to 1 if you don’t want FogBugz to record any more of these types of cases
    sEvent Text description of the bugevent
    cols The columns you want returned about this case
    File1, File2, File3, etc
        Upload files to the case, no limit to the number of files (constrained only by the max upload limit on the web server).  Use the enctype=”multipart/form-data” form type
    nFileCount – Required with File1, File2, etc
        Number of file parameters included in the request. If this is absent, only File1 will upload.

cmd=resolve

Same as cmd=edit, with the addition of the ixStatus FieldName.  Note: the UI does not let you change the project, area, assigned to, and category on resolve.  The API does.

cmd=close

Same as cmd=edit.  Note: the UI does not let you change any fieldsName on close.  The API does.  However, ixPersonAssignedTo will always be set to 1 (the CLOSED user).

cmd=email, cmd=reply, and cmd=forward

Same as cmd=edit, with additional arguments: sFrom (required), sTo (required), ixBug (required), sEvent (required, body of email), sSubject, sCC, sBCC and ixBugEventAttachment (this is the ixBugEvent if you want to include any attachments from a previous email, for example when you want to do a “forward“

It is not required, but we recommend making sure the case you email from has sCustomerEmail and ixMailbox set.

 Note: You can supply any address for the sFrom FieldName, although the UI restricts you to email addresses that FogBugz is actively checking (so that when a user replies to your email, it will actually go back into FogBugz).  The sFrom FieldName you supply here should be one of the values returned from the cmd=listMailboxes command, and best practices suggest using a matching ixMailbox value for the case.
    */

        public class GetCaseCommand : IFogBugzCommand<Case> {
            public string Command => "search";

            public List<KeyValuePair<string, string>> Parameters { get; } = new List<KeyValuePair<string, string>>();

            public GetCaseCommand(int caseId) : this(caseId, new List<FieldName>()) { }

            public GetCaseCommand(int caseId, List<FieldName> fieldNames)
            {
                Parameters.Add(new KeyValuePair<string, string>("q", caseId.ToString()));
                if (fieldNames.Count > 0)
                {
                    StringBuilder colsBuilder = new StringBuilder();
                    foreach (var fieldName in fieldNames)
                    {
                        colsBuilder.Append(Field.GetFogBugzCaseField(fieldName).FogBugzName).Append(",");
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
