using System.Collections.Generic;

namespace FogBugzAPI.FogBugzClient.Command {
    public class LogoffCommand : IFogBugzCommand<FogBugzReturn>, IFogBugzType
    {

        internal LogoffCommand()
        { }
        public string Command => "logoff";

        public List<KeyValuePair<string, string>> Parameters { get; } = new List<KeyValuePair<string, string>>();
        public FogBugzReturn CreateResponse(FogBugzReturn fogBugzReturn)
        {
            return fogBugzReturn;
        }
    }
}