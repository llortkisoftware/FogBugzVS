using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FogBugzAPI.XMLAPI;

namespace FogBugzAPI.Model
{
    public class ApiVersion
    {
        public int Version { get; private set; }
        public int MinimumCompatibleApi { get; private set; }
        public string ApiLocation { get; private set; }

        private ApiVersion()
        {
        }

        public static async Task<ApiVersion> GetApiVersion(FogBugzUrl fogBugzUrl)
        {
            FogBugzRequest request = new FogBugzRequest(fogBugzUrl, "api.xml", false);

            Task<XmlDocument> apiTask = request.Execute();
            ApiVersion apiVersion = new ApiVersion();

            XmlDocument responseDocument = await apiTask;

            XmlNode versionNode = responseDocument.DocumentElement.SelectSingleNode("version");
            XmlNode minimumCompatibleApiNode = responseDocument.DocumentElement.SelectSingleNode("minversion");
            XmlNode apiLocation = responseDocument.DocumentElement.SelectSingleNode("url");

            apiVersion.ApiLocation = apiLocation.InnerText;
            apiVersion.MinimumCompatibleApi = Int32.Parse(minimumCompatibleApiNode.InnerText);
            apiVersion.Version = Int32.Parse(versionNode.InnerText);

            return apiVersion;
        }
    }
}
