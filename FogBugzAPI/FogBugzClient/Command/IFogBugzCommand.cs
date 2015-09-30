using System.Collections.Generic;

namespace FogBugzAPI.FogBugzClient.Command
{
    public interface IFogBugzCommand<T> where T : IFogBugzType
    {
        string Command { get; }

        List<KeyValuePair<string, string>> Parameters { get; }

        T CreateResponse(FogBugzReturn fogBugzReturn);
    }

}