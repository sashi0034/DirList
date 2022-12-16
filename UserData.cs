using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DirList
{
    public class DirListDataInstance
    {
        public string InstanceName { get; set; } = "";
        public List<DirPath> DirPathList { get; set; } = new();

        public DirListDataInstance() { }

        public DirListDataInstance(string instanceName)
        {
            InstanceName = instanceName;
        }
    }

    public class TabData
    {
        public TabData() { }
        public List<string> TabNames { get; set; } = new();
        public int CurrentIndex = 0;
    }


    public class UserData
    {
        public UserData() { }
        public List<DirListDataInstance> DataInstanceList { get; set; } = new();
        public int DataInstanceSelectedIndex { get; set; } = 0;
        public Configs.DirListSortKind SortKind { get; set; }
        public TabData TabData { get; set; }


        private const string DataPath = @"UserData.xml";


        public void WriteSelf()
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(UserData));

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = ("\t");
            xmlSettings.Encoding = new System.Text.UTF8Encoding(false);

            using (var sw = new System.IO.StreamWriter(DataPath, false, new System.Text.UTF8Encoding(false)))
            using (var xmlWriter = XmlWriter.Create(sw, xmlSettings))
            {
                serializer.Serialize(xmlWriter, this);
            }
        }
        public static UserData? ReadFromFile()
        {
            UserData? result = null;

            try
            {
                using (StreamReader sr = new StreamReader(DataPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(UserData));
                    result = serializer.Deserialize(sr) as UserData;
                }

            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
