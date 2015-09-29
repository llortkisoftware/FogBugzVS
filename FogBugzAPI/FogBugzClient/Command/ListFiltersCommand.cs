using System.Collections.Generic;
using FogBugzAPI.Model;

namespace FogBugzAPI.FogBugzClient.Command {
    public class ListFiltersCommand : IFogBugzCommand<FilterList>
    {
        public ListFiltersCommand() { }
        public string Command => "listFilters";

        public List<KeyValuePair<string, string>> Parameters { get; } = new List<KeyValuePair<string, string>>();

        public FilterList CreateResponse(FogBugzReturn fogBugzReturn)
        {
            return new FilterList(fogBugzReturn);
        }
    }
}