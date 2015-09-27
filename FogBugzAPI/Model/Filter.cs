using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FogBugzAPI.XMLAPI;

namespace FogBugzAPI.Model
{
    public enum FilterType
    {
        BuiltIn,
        Saved,
        Shared
    }
    public class Filter
    {
        public static readonly string LIST_FILTERS_COMMAND = "listFilters";
        public static readonly string SET_FILTER_COMMAND = "setCurrentFilter";
        FilterType Type { get; set; }
        public string sFilter { get; set; }
        public string Display { get; set; }

        private ApiParameter _sFilterParameter;

        public ApiParameter SFilterParameter
        {
            get
            {
                if (_sFilterParameter == null)
                {
                    _sFilterParameter = new ApiParameter("sFilter", sFilter);
                }

                return _sFilterParameter;
            }
        }


        public static async Task<List<Filter>> GetFiltersAsync(FogBugzUrl url)
        {
            List<Filter> filters = new List<Filter>();
            
            FogBugzRequest request = new FogBugzRequest(url, Utilities.AssembleCommand(LIST_FILTERS_COMMAND, url.TokenParameter));

            XmlDocument document = await request.Execute();

            XmlNode filtersNode = document.DocumentElement.SelectSingleNode("filters");
            if (filtersNode == null)
            {
                return new List<Filter>();
            }

            XmlReader reader = new XmlNodeReader(filtersNode);

            XmlNodeList filterNodes = filtersNode.SelectNodes("filter");

            foreach (XmlNode filterNode in filterNodes)
            {

                Filter filter = new Filter();

                switch (filterNode.Attributes["type"].InnerText)
                {
                    case "builtin":
                        filter.Type = FilterType.BuiltIn;
                        break;
                    case "saved":
                        filter.Type = FilterType.Saved;
                        break;
                    case "shared":
                        filter.Type = FilterType.Shared;
                        break;
                }

                filter.sFilter = filterNode.Attributes["sFilter"].InnerText;
                filter.Display = filterNode.InnerText;
                filters.Add(filter);
            }

            return filters;
        }

        public static async Task SetFilterAsync(FogBugzUrl url, Filter filter)
        {
            FogBugzRequest request = new FogBugzRequest(url, Utilities.AssembleCommand(SET_FILTER_COMMAND, filter.SFilterParameter));
            await request.Execute();
            
        }
    }
}
