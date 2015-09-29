using System.Collections.Generic;
using System.Xml.Linq;

namespace FogBugzAPI.FogBugzClient {

    public interface IFogBugzType {
        
    }

    public interface IFogBugzCommand<T> where  T : IFogBugzType
    {

        string Command { get; }

        List<KeyValuePair<string,string>> Parameters { get; }

        T CreateResponse(FogBugzReturn fogBugzReturn);

    }

}