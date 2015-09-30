using FogBugzAPI.Model.Fields;

namespace FogBugzAPI.Model.Cases.Fields {
    public class CaseFieldCreator : FieldCreator<CaseFieldName, CaseField> {
        static CaseFieldCreator()
        {
            AddToLookupTable(new CaseField(CaseFieldName.Open, FieldType.Boolean, "fOpen"));
            AddToLookupTable(new CaseField(CaseFieldName.BugId, FieldType.Integer, "ixBug"));
            AddToLookupTable(new CaseField(CaseFieldName.ParentCase, FieldType.Integer, "ixBugParent"));
            AddToLookupTable(new CaseField(CaseFieldName.ChildCases, FieldType.IntegerList, "ixBugChildren"));
            AddToLookupTable(new CaseField(CaseFieldName.Tags, FieldType.StringList, "tags"));
            AddToLookupTable(new CaseField(CaseFieldName.Title, FieldType.String, "sTitle"));
            AddToLookupTable(new CaseField(CaseFieldName.OriginalTitle, FieldType.String, "sOriginalTitle"));
            AddToLookupTable(new CaseField(CaseFieldName.LatestTextSummary, FieldType.String, "sLatestTextSummary"));
            AddToLookupTable(new CaseField(CaseFieldName.LatestBugEventWithTextId, FieldType.Integer, "ixBugEventLatestText"));
            AddToLookupTable(new CaseField(CaseFieldName.ProjectId, FieldType.Integer, "ixProject"));
            AddToLookupTable(new CaseField(CaseFieldName.ProjectName, FieldType.String, "sProject"));
            AddToLookupTable(new CaseField(CaseFieldName.AreaId, FieldType.Integer, "ixArea"));
            AddToLookupTable(new CaseField(CaseFieldName.AreaName, FieldType.String, "sArea"));
            AddToLookupTable(new CaseField(CaseFieldName.PersonAssignedToId, FieldType.Integer, "ixPersonAssignedTo"));
            AddToLookupTable(new CaseField(CaseFieldName.PersonAssignedToName, FieldType.String, "sPersonAssignedTo"));
            AddToLookupTable(new CaseField(CaseFieldName.PersonAssignedToEmail, FieldType.String, "sEmailAssignedTo"));
            AddToLookupTable(new CaseField(CaseFieldName.PersonOpenedById, FieldType.Integer, "ixPersonOpenedBy"));
            AddToLookupTable(new CaseField(CaseFieldName.PersonResolvedById, FieldType.Integer, "ixPersonResolvedBy"));
            AddToLookupTable(new CaseField(CaseFieldName.PersonClosedById, FieldType.Integer, "ixPersonClosedBy"));
            AddToLookupTable(new CaseField(CaseFieldName.PersonLastEditedById, FieldType.Integer, "ixPersonLastEditedBy"));
            AddToLookupTable(new CaseField(CaseFieldName.StatusId, FieldType.Integer, "ixStatus"));
            AddToLookupTable(new CaseField(CaseFieldName.StatusName, FieldType.String, "sStatus"));
            AddToLookupTable(new CaseField(CaseFieldName.DuplicateBugIds, FieldType.IntegerList, "ixBugDuplicates"));
            AddToLookupTable(new CaseField(CaseFieldName.OriginalBugId, FieldType.Integer, "ixBugOriginal"));
            AddToLookupTable(new CaseField(CaseFieldName.PriorityId, FieldType.Integer, "ixPriority"));
            AddToLookupTable(new CaseField(CaseFieldName.PriorityName, FieldType.String, "sPriority"));
            AddToLookupTable(new CaseField(CaseFieldName.FixForId, FieldType.Integer, "ixFixFor"));
            AddToLookupTable(new CaseField(CaseFieldName.FixForName, FieldType.String, "sFixFor"));
            AddToLookupTable(new CaseField(CaseFieldName.FixForDate, FieldType.DateTime, "dtFixFor"));
            AddToLookupTable(new CaseField(CaseFieldName.Version, FieldType.String, "sVersion"));
            AddToLookupTable(new CaseField(CaseFieldName.Computer, FieldType.String, "sComputer"));
            AddToLookupTable(new CaseField(CaseFieldName.EstimateOriginalHours, FieldType.Double, "hrsOrigEst"));
            AddToLookupTable(new CaseField(CaseFieldName.EstimateCurrentHours, FieldType.Double, "hrsCurrEst"));
            AddToLookupTable(new CaseField(CaseFieldName.EstimateElapsedHours, FieldType.Double, "hrsElapsed"));
            AddToLookupTable(new CaseField(CaseFieldName.NumberOfBugOccurrences, FieldType.Integer, "c"));
            AddToLookupTable(new CaseField(CaseFieldName.CustomerContactEmail, FieldType.String, "sCustomerEmail"));
            AddToLookupTable(new CaseField(CaseFieldName.MailboxId, FieldType.Integer, "ixMailbox"));
            AddToLookupTable(new CaseField(CaseFieldName.CategoryId, FieldType.Integer, "ixCategory"));
            AddToLookupTable(new CaseField(CaseFieldName.CategoryName, FieldType.String, "sCategory"));
            AddToLookupTable(new CaseField(CaseFieldName.DateOpened, FieldType.DateTime, "dtOpened"));
            AddToLookupTable(new CaseField(CaseFieldName.DateResolved, FieldType.DateTime, "dtResolved"));
            AddToLookupTable(new CaseField(CaseFieldName.DateClosed, FieldType.DateTime, "dtClosed"));
            AddToLookupTable(new CaseField(CaseFieldName.LatestBugEventId, FieldType.Integer, "ixBugEventLatest"));
            AddToLookupTable(new CaseField(CaseFieldName.DateLastUpdated, FieldType.DateTime, "dtLastUpdated"));
            AddToLookupTable(new CaseField(CaseFieldName.HasBeenRepliedTo, FieldType.Boolean, "fReplied"));
            AddToLookupTable(new CaseField(CaseFieldName.HasBeenForwarded, FieldType.Boolean, "fForwarded"));
            AddToLookupTable(new CaseField(CaseFieldName.TicketNumber, FieldType.String, "sTicket"));
            AddToLookupTable(new CaseField(CaseFieldName.DiscussionTopicId, FieldType.Integer, "ixDiscussTopic"));
            AddToLookupTable(new CaseField(CaseFieldName.DateDue, FieldType.DateTime, "dtDue"));
            AddToLookupTable(new CaseField(CaseFieldName.ReleaseNotes, FieldType.String, "sReleaseNotes"));
            AddToLookupTable(new CaseField(CaseFieldName.LastBugEventLastTimeViewed, FieldType.Integer, "ixBugEventLastView"));
            AddToLookupTable(new CaseField(CaseFieldName.DateLastViewed, FieldType.DateTime, "dtLastView"));
            AddToLookupTable(new CaseField(CaseFieldName.RelatedBugsIds, FieldType.IntegerList, "ixRelatedBugs"));
            AddToLookupTable(new CaseField(CaseFieldName.ScoutDescription, FieldType.String, "sScoutDescription"));
            AddToLookupTable(new CaseField(CaseFieldName.ScoutMessage, FieldType.String, "sScoutMessage"));
            AddToLookupTable(new CaseField(CaseFieldName.StopScoutReporting, FieldType.Boolean, "fScoutStopReporting"));
            AddToLookupTable(new CaseField(CaseFieldName.DateLastScoutOccurence, FieldType.DateTime, "dtLastOccurrence"));
            AddToLookupTable(new CaseField(CaseFieldName.SubscribedToCase, FieldType.Boolean, "fSubscribed"));
            AddToLookupTable(new CaseField(CaseFieldName.StoryPoints, FieldType.Double, "dblStoryPts"));
        }

        

    }
}