using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FogBugzAPI
{
    [Serializable]
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class FogBugzUrl
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {

        public string BaseUrl { get; set; }
        public string DefaultUsername { get; set; }
        public string DefaultPassword { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is FogBugzUrl))
            {
                return false;
            }

            FogBugzUrl other = (FogBugzUrl)obj;

            return string.Equals(BaseUrl, other.BaseUrl, StringComparison.CurrentCulture) &&
                string.Equals(DefaultUsername, other.DefaultUsername, StringComparison.CurrentCulture) &&
                string.Equals(DisplayName, other.DisplayName, StringComparison.CurrentCulture);
        }

        public override string ToString()
        {
            return "{ BaseUrl=" + BaseUrl + ", DefaultUsername=" + DefaultUsername + ", DisplayName=" + DisplayName +
                   ", Token=" + Token + " }";
        }
    }

    [Serializable]
    public class Configuration
    {

        public List<FogBugzUrl> BaseUrlList { get; private set; }

        public static readonly string CONFIG_PATH =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FogBugzVS";
        public static readonly string CONFIG_FILENAME = "fbvsconfig.xml";

        public Configuration()
        {
            BaseUrlList = new List<FogBugzUrl>();
        }

        /// <summary>
        /// Saves the current settings to "APPDATA/FogBugzVS/fbvsconfig.xml"
        /// <para>
        /// This should be wrapped in a try/catch block since filesystem exceptions can be thrown.
        /// </para>
        /// </summary>
        public void Save()
        {
            Save(CONFIG_PATH + "/" + CONFIG_FILENAME);
        }

        /// <summary>
        /// Saves the current settings to the given file name.
        /// </summary>
        /// <param name="fileName">The FULL file name including path to use</param>
        public void Save(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
            Directory.CreateDirectory(CONFIG_PATH);
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(fs, this);
            }
        }

        /// <summary>
        /// Loads the current settings from "APPDATA/FogBugzVS/fbvsconfig.xml"
        /// <para>
        /// This should be wrapped in a try/catch block since filesystem exceptions can be thrown.
        /// </para>
        /// </summary>
        public static Configuration Load()
        {
            return Load(CONFIG_PATH + "/" + CONFIG_FILENAME);
        }

        public static Configuration Load(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
            Configuration config = null;
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                config = (Configuration)serializer.Deserialize(fs);
            }

            return config;

        }
    }
}
