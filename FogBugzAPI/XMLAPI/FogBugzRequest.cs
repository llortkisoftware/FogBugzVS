using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FogBugzAPI.XMLAPI
{
    public class FogBugzRequest
    {

        //public Uri baseUri { get; private set; }


        private Uri baseUri;
        private string requestPath;

        public FogBugzRequest(FogBugzUrl fogBugzFogBugzUrl, string requestPath, bool appendApiLocation = true)
        {
            baseUri = fogBugzFogBugzUrl.GetBaseUri();
            this.requestPath = (appendApiLocation ? fogBugzFogBugzUrl.ApiLocation : "") + requestPath;
            //baseUri = new Uri("https://" + fogBugzFogBugzUrl.BaseUrl + "/" + requestPath);
        }

        public async Task<XmlDocument> Execute()
        {
            HttpClient client = new HttpClient();
            
            client.BaseAddress = baseUri;
            var response = await client.GetAsync(requestPath);

            XmlDocument document = new XmlDocument();
            document.LoadXml(await response.Content.ReadAsStringAsync());

            return document;
        } 
        
    }
}
