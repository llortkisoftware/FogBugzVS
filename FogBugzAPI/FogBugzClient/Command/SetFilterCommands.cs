using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FogBugzAPI.Model;

namespace FogBugzAPI.FogBugzClient.Command
{
    public class SetFilterCommand : IFogBugzCommand<FogBugzReturn>
    {

        public string Command => "setFilter";

        public List<KeyValuePair<string, string>> Parameters { get; } = new List<KeyValuePair<string, string>>();

        public SetFilterCommand(Filter filter)
        {
            Parameters.Add(new KeyValuePair<string, string>("sFilter", filter.sFilter));
        }

        public FogBugzReturn CreateResponse(FogBugzReturn fogBugzReturn)
        {
            return fogBugzReturn;
        }
    }
}
