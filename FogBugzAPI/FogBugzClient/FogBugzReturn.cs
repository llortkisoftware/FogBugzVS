using System;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FogBugzAPI.FogBugzClient {

    //TODO: Pull out as an interface? 
    public class FogBugzReturn : IFogBugzType
    {
        public Boolean IsError { get; private set; }
        public XDocument XmlDocument { get; private set; }

        public FogBugzReturn(XDocument xmlDocument)
        {
            XmlDocument = xmlDocument;
            if (xmlDocument.XPathSelectElement("/response/error") != null)
            {
                //TODO: Have a GetErrorObject
                IsError = true;
            }
        }
    }
}