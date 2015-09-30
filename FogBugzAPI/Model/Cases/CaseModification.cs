using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogBugzAPI.Model.Cases
{
    public class CaseModification
    {


        /*
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

    

cmd=resolve

Same as cmd=edit, with the addition of the ixStatus CaseFieldName.  Note: the UI does not let you change the project, area, assigned to, and category on resolve.  The API does.

cmd=close

Same as cmd=edit.  Note: the UI does not let you change any fieldsName on close.  The API does.  However, ixPersonAssignedTo will always be set to 1 (the CLOSED user).

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
    */

        public enum ModificationType {
            New,
            Edit,
            Assign,
            Reactivate,
            Reopen,
            Resolve,
            Close
        }
    }
}
