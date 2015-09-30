using System;
using System.Collections;

namespace FogBugzAPI.Model.Cases.Fields
{
    public class Field
    {
        private static readonly Hashtable LookupTable = new Hashtable();

        static Field()
        {
            AddToLookupTable(new Field(FieldName.Open, FieldType.Boolean, "fOpen"));
            AddToLookupTable(new Field(FieldName.ParentCase, FieldType.Integer, "ixBugParent"));
            AddToLookupTable(new Field(FieldName.ChildCases, FieldType.IntegerList, "ixBugChildren"));
            AddToLookupTable(new Field(FieldName.Tags, FieldType.StringList, "tags"));
            AddToLookupTable(new Field(FieldName.Title, FieldType.String, "sTitle"));
            AddToLookupTable(new Field(FieldName.OriginalTitle, FieldType.String, "sOriginalTitle"));
            AddToLookupTable(new Field(FieldName.LatestTextSummary, FieldType.String, "sLatestTextSummary"));
            AddToLookupTable(new Field(FieldName.LatestBugEventWithTextId, FieldType.Integer, "ixBugEventLatestText"));
            AddToLookupTable(new Field(FieldName.ProjectId, FieldType.Integer, "ixProject"));
            AddToLookupTable(new Field(FieldName.ProjectName, FieldType.String, "sProject"));
            AddToLookupTable(new Field(FieldName.AreaId, FieldType.Integer, "ixArea"));
            AddToLookupTable(new Field(FieldName.AreaName, FieldType.String, "sArea"));
            AddToLookupTable(new Field(FieldName.PersonAssignedToId, FieldType.Integer, "ixPersonAssignedTo"));
            AddToLookupTable(new Field(FieldName.PersonAssignedToName, FieldType.String, "sPersonAssignedTo"));
            AddToLookupTable(new Field(FieldName.PersonAssignedToEmail, FieldType.String, "sEmailAssignedTo"));
            AddToLookupTable(new Field(FieldName.PersonOpenedById, FieldType.Integer, "ixPersonOpenedBy"));
            AddToLookupTable(new Field(FieldName.PersonResolvedById, FieldType.Integer, "ixPersonResolvedBy"));
            AddToLookupTable(new Field(FieldName.PersonClosedById, FieldType.Integer, "ixPersonClosedBy"));
            AddToLookupTable(new Field(FieldName.PersonLastEditedById, FieldType.Integer, "ixPersonLastEditedBy"));
            AddToLookupTable(new Field(FieldName.StatusId, FieldType.Integer, "ixStatus"));
            AddToLookupTable(new Field(FieldName.StatusName, FieldType.String, "sStatus"));
            AddToLookupTable(new Field(FieldName.DuplicateBugIds, FieldType.IntegerList, "ixBugDuplicates"));
            AddToLookupTable(new Field(FieldName.OriginalBugId, FieldType.Integer, "ixBugOriginal"));
            AddToLookupTable(new Field(FieldName.PriorityId, FieldType.Integer, "ixPriority"));
            AddToLookupTable(new Field(FieldName.PriorityName, FieldType.String, "sPriority"));
            AddToLookupTable(new Field(FieldName.FixForId, FieldType.Integer, "ixFixFor"));
            AddToLookupTable(new Field(FieldName.FixForName, FieldType.String, "sFixFor"));
            AddToLookupTable(new Field(FieldName.FixForDate, FieldType.DateTime, "dtFixFor"));
            AddToLookupTable(new Field(FieldName.Version, FieldType.String, "sVersion"));
            AddToLookupTable(new Field(FieldName.Computer, FieldType.String, "sComputer"));
            AddToLookupTable(new Field(FieldName.EstimateOriginalHours, FieldType.Double, "hrsOrigEst"));
            AddToLookupTable(new Field(FieldName.EstimateCurrentHours, FieldType.Double, "hrsCurrEst"));
            AddToLookupTable(new Field(FieldName.EstimateElapsedHours, FieldType.Double, "hrsElapsed"));
            AddToLookupTable(new Field(FieldName.NumberOfBugOccurrences, FieldType.Integer, "c"));
            AddToLookupTable(new Field(FieldName.CustomerContactEmail, FieldType.String, "sCustomerEmail"));
            AddToLookupTable(new Field(FieldName.MailboxId, FieldType.Integer, "ixMailbox"));
            AddToLookupTable(new Field(FieldName.CategoryId, FieldType.Integer, "ixCategory"));
            AddToLookupTable(new Field(FieldName.CategoryName, FieldType.String, "sCategory"));
            AddToLookupTable(new Field(FieldName.DateOpened, FieldType.DateTime, "dtOpened"));
            AddToLookupTable(new Field(FieldName.DateResolved, FieldType.DateTime, "dtResolved"));
            AddToLookupTable(new Field(FieldName.DateClosed, FieldType.DateTime, "dtClosed"));
            AddToLookupTable(new Field(FieldName.LatestBugEventId, FieldType.Integer, "ixBugEventLatest"));
            AddToLookupTable(new Field(FieldName.DateLastUpdated, FieldType.DateTime, "dtLastUpdated"));
            AddToLookupTable(new Field(FieldName.HasBeenRepliedTo, FieldType.Boolean, "fReplied"));
            AddToLookupTable(new Field(FieldName.HasBeenForwarded, FieldType.Boolean, "fForwarded"));
            AddToLookupTable(new Field(FieldName.TicketNumber, FieldType.String, "sTicket"));
            AddToLookupTable(new Field(FieldName.DiscussionTopicId, FieldType.Integer, "ixDiscussTopic"));
            AddToLookupTable(new Field(FieldName.DateDue, FieldType.DateTime, "dtDue"));
            AddToLookupTable(new Field(FieldName.ReleaseNotes, FieldType.String, "sReleaseNotes"));
            AddToLookupTable(new Field(FieldName.LastBugEventLastTimeViewed, FieldType.Integer, "ixBugEventLastView"));
            AddToLookupTable(new Field(FieldName.DateLastViewed, FieldType.DateTime, "dtLastView"));
            AddToLookupTable(new Field(FieldName.RelatedBugsIds, FieldType.IntegerList, "ixRelatedBugs"));
            AddToLookupTable(new Field(FieldName.ScoutDescription, FieldType.String, "sScoutDescription"));
            AddToLookupTable(new Field(FieldName.ScoutMessage, FieldType.String, "sScoutMessage"));
            AddToLookupTable(new Field(FieldName.StopScoutReporting, FieldType.Boolean, "fScoutStopReporting"));
            AddToLookupTable(new Field(FieldName.DateLastScoutOccurence, FieldType.DateTime, "dtLastOccurrence"));
            AddToLookupTable(new Field(FieldName.SubscribedToCase, FieldType.Boolean, "fSubscribed"));
            AddToLookupTable(new Field(FieldName.StoryPoints, FieldType.Double, "dblStoryPts"));
        }


        private Field(FieldName fieldName, FieldType fieldType, string fogBugzName)
        {
            FieldName = fieldName;
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

        public FieldName FieldName { get; private set; }
        public FieldType FieldType { get; private set; }
        public string FogBugzName { get; private set; }
        public FieldValue Value { get; private set; }

        public Field CreateNew()
        {
            return new Field(FieldName, FieldType, FogBugzName);
        }

        public Field CreateNew(string value)
        {
            var newField = new Field(FieldName, FieldType, FogBugzName);
            newField.Value.FromString(value);
            return newField;
        }

        private static void AddToLookupTable(Field field)
        {
            LookupTable.Add(field.FieldName, field);
        }

        public static Field GetFogBugzCaseField(FieldName fieldName)
        {
            return ((Field)LookupTable[fieldName]).CreateNew();
        }
    }
}