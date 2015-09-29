using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using FogBugzAPI.FogBugzClient;

namespace FogBugzAPI.Model
{
    public enum FilterType
    {
        BuiltIn,
        Saved,
        Shared
    }

    public class Filter : IFogBugzType
    {

        FilterType Type { get; set; }
        public string sFilter { get; set; }
        public string Display { get; set; }
        public bool IsCurrent { get; set; } 

        public Filter(XElement filterElement)
        {
            switch (filterElement.Attribute("type").Value)
            {
                case "builtin":
                    Type = FilterType.BuiltIn;
                    break;
                case "saved":
                    Type = FilterType.Saved;
                    break;
                case "shared":
                    Type = FilterType.Shared;
                    break;
            }

            sFilter = filterElement.Attribute("sFilter").Value;
            Display = filterElement.Value;

            var status = filterElement.Attribute("statue");
            if (status != null)
            {
                IsCurrent = true;
            }
        }
    }
}
