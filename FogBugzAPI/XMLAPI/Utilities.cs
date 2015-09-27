using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FogBugzAPI.XMLAPI
{
    public class ApiParameter {
        public string Name { get; set; }
        public string Value { get; set; }

        public ApiParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    public class Utilities
    {
        public static string AssembleCommand(string command, params ApiParameter[] parameters)
        {
            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.Append("cmd=").Append(command);
            foreach (var parameter in parameters)
            {
                commandBuilder.Append("&").Append(parameter.Name).Append("=").Append(WebUtility.UrlEncode(parameter.Value));
            }

            return commandBuilder.ToString();
        }
    }
}
