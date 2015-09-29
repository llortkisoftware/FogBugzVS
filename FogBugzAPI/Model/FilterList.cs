using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using FogBugzAPI.FogBugzClient;

namespace FogBugzAPI.Model {

    public class FilterList : List<Filter>, IFogBugzType {

        public FilterList(FogBugzReturn fogBugzReturn) : base()
        {
            AddRange(fogBugzReturn.XmlDocument.XPathSelectElements("/response/filters/filter").Select(e => new Filter(e)).ToList());
        }

        public bool HasCurrentFilter()
        {
            return GetCurrentFilter() != null;
        }

        public Filter GetCurrentFilter()
        {
            return this.FirstOrDefault(filter => filter.IsCurrent);
        }

        public void SetCurrentFilter(Filter filter)
        {
            Filter currentFilter = GetCurrentFilter();
            if (currentFilter != null)
            {
                currentFilter.IsCurrent = false;
            }
            filter.IsCurrent = true;
        }
    }
}