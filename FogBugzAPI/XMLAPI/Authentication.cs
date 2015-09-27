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
    public class Authentication
    {

        internal Authentication()
        {
            AmbiguousUserList = new List<string>();
        }

        public string Token { get; internal set; }

        public bool IsError => string.IsNullOrEmpty(Token);

        public string ErrorMessage { get; internal set; }
        public List<string> AmbiguousUserList { get; private set; }


        private static readonly string LOGON_COMMAND = "logon";
        private static readonly string LOGOFF_COMMAND = "logoff";

        public static async Task<Authentication> Logon(FogBugzUrl url)
        {
            return await (Logon(url, url.DefaultUsername, url.DefaultPassword));
        }

        public static async Task<Authentication> Logon(FogBugzUrl url, string password)
        {
            return await Logon(url, url.DefaultUsername, password);
        }


        public static async Task<Authentication> Logon(FogBugzUrl url, string username, string password)
        {

            string location = Utilities.AssembleCommand(LOGON_COMMAND, new ApiParameter("email", username),
                new ApiParameter("password", password));

            FogBugzRequest request = new FogBugzRequest(url, location);

            XmlDocument responseDocument = await request.Execute();
            XmlNode rootNode = responseDocument.DocumentElement;
            if (rootNode == null)
            {
                return new Authentication() { ErrorMessage = "Invalid API Response" };
            }

            using (XmlNodeReader reader = new XmlNodeReader(rootNode))
            {
                reader.Read(); //skil response element
                reader.Read();
                if (reader.Name.Equals("token"))
                {
                    reader.Read();
                    return new Authentication() { Token = reader.Value };
                }
                else
                {
                    //error
                    
                    Authentication response = new Authentication();
                    
                    string errorCode = reader.GetAttribute("code");
                    reader.Read();
                    response.ErrorMessage = reader.Value;
                    if (errorCode.Equals("2"))
                    {
                        reader.Read();
                        XmlNodeList people = rootNode.SelectNodes("//person");
                        foreach(XmlNode person in people)
                        {
                            response.AmbiguousUserList.Add(person.InnerText);
                        }
                    }

                    return response;
                }
            }
            
        }

        public static async Task LogOff(FogBugzUrl url)
        {
            string location = Utilities.AssembleCommand(LOGOFF_COMMAND, url.TokenParameter);
            FogBugzRequest request = new FogBugzRequest(url, location);

            XmlDocument responseDocument = await request.Execute();

        }

        public static async Task<bool> IsValidToken(FogBugzUrl url)
        {
            string location = Utilities.AssembleCommand(LOGON_COMMAND, url.TokenParameter);

            FogBugzRequest request = new FogBugzRequest(url, location);

            XmlDocument responseDocument = await request.Execute();

            XmlNode tokenNode = responseDocument.SelectSingleNode("response/token");
            if (tokenNode != null && tokenNode.InnerText.Equals(url.Token))
            {
                return true;
            }

            return false;
        }
    }
}
